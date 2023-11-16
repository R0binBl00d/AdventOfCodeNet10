using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2015.Day_23
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/23
--- Day 23: Opening the Turing Lock ---
Little Jane Marie just got her very first computer for Christmas from some unknown benefactor. 
It comes with instructions and an example program, but the computer itself seems to be malfunctioning. 
She's curious what the program does, and would like you to help her run it.

The manual explains that the computer supports two registers and six instructions 
(truly, it goes on to remind the reader, a state-of-the-art technology). 
The registers are named a and b, can hold any non-negative integer, and begin with a value of 0. 
The instructions are as follows:

hlf r sets register r to half its current value, then continues with the next instruction.
tpl r sets register r to triple its current value, then continues with the next instruction.
inc r increments register r, adding 1 to it, then continues with the next instruction.
jmp offset is a jump; it continues with the instruction offset away relative to itself.
jie r, offset is like jmp, but only jumps if register r is even ("jump if even").
jio r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).
All three jump instructions work with an offset relative to that instruction. 
The offset is always written with a prefix + or - to indicate the direction of the jump 
(forward or backward, respectively). 
    
For example, jmp +1 would simply continue with the next instruction, 
while jmp +0 would continuously jump back to itself forever.

The program exits when it tries to run an instruction beyond the ones defined.

For example, this program sets a to 2, because the jio instruction causes it to skip the tpl instruction:

inc a
jio a, +2
tpl a
inc a
What is the value in register b when the program in your puzzle input is finished executing?
    */
    /// </summary>
    /// <returns>
    /// 167 too low (Uint16)
    /// Your puzzle answer was 184. (UInt32)
    /// </returns>
    public override string Execute()
    {
      string result = "";

      List<KeyValuePair<command, int[]>> commands = new List<KeyValuePair<command, int[]>>();

      foreach (var line in Lines)
      {
        var chunks = line.Split(new char[] { ' ', ',', '+' }, StringSplitOptions.RemoveEmptyEntries);

        command c = (command)Enum.Parse(typeof(command), chunks[0]);

        int[] vals;

        switch (c)
        {
          case command.jmp:
            vals = new int[] { Int32.Parse(chunks[1]) };
            break;
          case command.jie:
          case command.jio:
            vals = new int[] { chunks[1] == "a" ? 0 : 1, Int32.Parse(chunks[2]) };
            break;
          default:
            vals = new int[] { chunks[1] == "a" ? 0 : 1 };
            break;
        }
        commands.Add(new KeyValuePair<command, int[]>(c, vals));
      }

      // execute commands
      UInt32[] Register = new UInt32[] { 0, 0 };

      int executionpointer = 0;
      do
      {
        switch (commands[executionpointer].Key)
        {
          case command.hlf:
            Register[commands[executionpointer].Value[0]] /= 2;
            executionpointer++;
            break;
          case command.tpl:
            Register[commands[executionpointer].Value[0]] *= 3;
            executionpointer++;
            break;
          case command.inc:
            Register[commands[executionpointer].Value[0]]++;
            executionpointer++;
            break;
          case command.jmp:
            executionpointer += commands[executionpointer].Value[0];
            break;
          case command.jie:
            executionpointer = Register[commands[executionpointer].Value[0]] % 2 == 0 ?
              executionpointer + commands[executionpointer].Value[1] :
              executionpointer + 1;
            break;
          case command.jio:
            executionpointer = Register[commands[executionpointer].Value[0]] == 1 ?
              executionpointer + commands[executionpointer].Value[1] :
              executionpointer + 1;
            break;
        }

      } while (executionpointer < commands.Count);

      result = Register[1].ToString();
      return result;
    }

    private enum command
    {
      hlf,//r sets register r to half its current value, then continues with the next instruction.
      tpl,//r sets register r to triple its current value, then continues with the next instruction.
      inc,//r increments register r, adding 1 to it, then continues with the next instruction.
      jmp,//offset is a jump; it continues with the instruction offset away relative to itself.
      jie,//r, offset is like jmp, but only jumps if register r is even ("jump if even").
      jio //r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).
    }
  }
}
