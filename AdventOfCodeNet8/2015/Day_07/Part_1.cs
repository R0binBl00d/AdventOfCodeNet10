using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Net.WebRequestMethods;

namespace AdventOfCodeNet8._2015.Day_07
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
     https://adventofcode.com/2015/day/7
--- Day 7: Some Assembly Required ---
This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is a little under the recommended age range, and he needs help assembling the circuit.

Each wire has an identifier (some lowercase letters) and can carry a 16-bit signal (a number from 0 to 65535). A signal is provided to each wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can provide its signal to multiple destinations. A gate provides no signal until all of its inputs have a signal.

The included instructions booklet describes how to connect the parts together: x AND y -> z means to connect wires x and y to an AND gate, and then connect its output to wire z.

For example:

123 -> x means that the signal 123 is provided to wire x.
x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
Other possible gates include OR (bitwise OR) and RSHIFT (right-shift). If, for some reason, you'd like to emulate the circuit instead, almost all programming languages (for example, C, JavaScript, or Python) provide operators for these gates.

For example, here is a simple circuit:

123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
After it is run, these are the signals on the wires:

d: 72
e: 507
f: 492
g: 114
h: 65412
i: 65079
x: 123
y: 456
In little Bobby's kit's instructions booklet (provided as your puzzle input), what signal is ultimately provided to wire a?
     */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 16076.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      // 0 - 65535
      //ushort max = ushort.MaxValue;

      Dictionary<string, Node> signals = new Dictionary<string, Node>();

      foreach (string line in Lines)
      {
        var chunks = line.Split(' ');

        #region if NOT

        if (chunks[0] == "NOT") // NOT ab -> cd
        {
          bool IsNumberN = ushort.TryParse(chunks[1], out var notNumber); // ab

          Node notNode = new Node(); // cd

          if (signals.ContainsKey(chunks[3]))
          {
            notNode = signals[chunks[3]];
          }
          else
          {
            notNode.Name = chunks[3]; // cd
          }
          notNode.Operation = "NOT";

          if (IsNumberN) // ab
          {
            notNode.HasResult = true;
            notNode.Result = (ushort)~notNumber;
          }
          else
          {
            if (signals.ContainsKey(chunks[1])) // ab
            {
              notNode.firstNumber = signals[chunks[1]];
              notNode.HasResult = false;
            }
            else
            {
              Node unknown = new Node(); // ab
              unknown.Name = chunks[1];
              unknown.Operation = "";
              unknown.HasResult = false;

              signals.Add(chunks[1], unknown);

              notNode.firstNumber = unknown;
            }
          }

          if (!signals.ContainsKey(chunks[3]))
          {
            signals.Add(chunks[3], notNode);
          }
        }

        #endregion NOT

        else if (chunks[1] == "->")
        {
          // lx -> a
          // 19138 -> z

          bool IsNumberA = ushort.TryParse(chunks[0], out var numberA);

          Node newNodeA = new Node();
          Node firstNodeA;


          if (signals.ContainsKey(chunks[2]))
          {
            newNodeA = signals[chunks[2]];
          }
          else
          {
            newNodeA.Name = chunks[2];
            newNodeA.HasResult = false;
          }
          newNodeA.Operation = "->";

          if (IsNumberA)
          {
            firstNodeA = new Node();
            firstNodeA.HasResult = true;
            firstNodeA.Result = numberA;

            newNodeA.firstNumber = firstNodeA;
          }
          else
          {
            if (signals.ContainsKey(chunks[0]))
            {
              newNodeA.firstNumber = signals[chunks[0]];
            }
            else
            {
              Node unknown = new Node(); // ab
              unknown.Name = chunks[0];
              unknown.Operation = "";
              unknown.HasResult = false;

              signals.Add(chunks[0], unknown);

              newNodeA.firstNumber = unknown;
            }
          }

          if (!signals.ContainsKey(chunks[2]))
          {
            signals.Add(chunks[2], newNodeA);
          }
        }
        else
        {
          // ab AND cd -> xy
          // ab OR cd -> xy
          // ab LSHIFT 1 -> xy
          // ab RSHIFT 2 -> xy

          bool IsNumber1 = ushort.TryParse(chunks[0], out var number1);
          bool IsNumber2 = ushort.TryParse(chunks[2], out var number2);

          Node firstNode;
          Node secondNode;

          Node newNodeXY = new Node();

          int targetIndex = 4; // part after the "->"

          if (signals.ContainsKey(chunks[targetIndex]))
          {
            newNodeXY = signals[chunks[targetIndex]];
          }
          else
          {
            newNodeXY.Name = chunks[targetIndex];
            newNodeXY.HasResult = false;
          }

          if (IsNumber1)
          {
            firstNode = new Node();
            firstNode.HasResult = true;
            firstNode.Result = number1;

            newNodeXY.firstNumber = firstNode;
          }
          else
          {
            if (signals.ContainsKey(chunks[0]))
            {
              newNodeXY.firstNumber = signals[chunks[0]];
            }
            else
            {
              Node unknown = new Node(); // ab
              unknown.Name = chunks[0];
              unknown.Operation = "";
              unknown.HasResult = false;

              signals.Add(chunks[0], unknown);

              newNodeXY.firstNumber = unknown;
            }
          }

          if (IsNumber2)
          {
            secondNode = new Node();
            secondNode.HasResult = true;
            secondNode.Result = number2;

            newNodeXY.secondNumber = secondNode;
          }
          else
          {
            if (signals.ContainsKey(chunks[2]))
            {
              newNodeXY.secondNumber = signals[chunks[2]];
            }
            else
            {
              Node unknown = new Node(); // cd
              unknown.Name = chunks[2];
              unknown.Operation = "";
              unknown.HasResult = false;

              signals.Add(chunks[2], unknown);

              newNodeXY.secondNumber = unknown;
            }
          }

          // first and second Value assigned !!

          newNodeXY.Operation = chunks[1];

          if (!signals.ContainsKey(chunks[4]))
          {
            signals.Add(chunks[4], newNodeXY);
          }
        }
      }

      Node a = null;
      if (signals.ContainsKey("a"))
      {
        a = signals["a"];
      }
      else
      {
        Debugger.Break();
      }

      if (a != null)
      {
        a.Calculate();

        if (a.HasResult)
        {
          result = a.Result.ToString();
        }
        else
        {
          Debugger.Break();
        }
      }

      return result;
    }

    private class Node
    {
      public Node firstNumber;
      public Node secondNumber;

      public string Name;
      public string Operation;

      public ushort Result;
      public bool HasResult;
      public bool Failed;
      public Node()
      {
        HasResult = false;
        Failed = false;
        Result = 0;

        firstNumber = null;
        secondNumber = null;

        Name = "";
        Operation = "";
      }

      public void Calculate()
      {
        int first = -1;
        int second = -1;

        if (firstNumber != null)
        {
          if (firstNumber.HasResult)
          {
            first = firstNumber.Result;
          }
          else
          {
            firstNumber.Calculate();
            if (firstNumber.HasResult)
            {
              first = firstNumber.Result;
            }
            else
            {
              Debugger.Break(); // This should not happen !!
            }
          }
        }
        else
        {
          Debugger.Break(); // This should not happen !!
        }

        switch (Operation)
        {
          case "NOT":
            if (first != -1)
            {
              HasResult = true;
              Result = (ushort)~first;
              return;
            }
            else
            {
              Debugger.Break(); // This should not happen !!
            }
            break;
          case "->":
            if (first != -1)
            {
              HasResult = true;
              Result = (ushort)first;
              return;
            }
            else
            {
              Debugger.Break(); // This should not happen !!
            }
            break;
          default:
            if (secondNumber != null)
            {
              if (secondNumber.HasResult)
              {
                second = secondNumber.Result;
              }
              else
              {
                secondNumber.Calculate();
                if (secondNumber.HasResult)
                {
                  second = secondNumber.Result;
                }
                else
                {
                  Debugger.Break(); // This should not happen !!
                }

              }
            }
            else
            {
              // not sure how to resolve this -> g AND i -> j

              //but there is calculation to calculate i
              // so what is the result?

              Debugger.Break(); // This should not happen !!
            }
            break;
        }

        if (first == -1 || second == -1)
        {
          // FAILED !!!!!!!!!!!!!!
          // No inputs, don't activate'

          Failed = true;
          return;
        }

        switch (Operation)
        {
          case "AND":
            HasResult = true;
            Result = (ushort)(first & second);
            break;
          case "OR":
            HasResult = true;
            Result = (ushort)(first | second);
            break;
          case "LSHIFT":
            HasResult = true;
            Result = (ushort)(first << second);
            break;
          case "RSHIFT":
            HasResult = true;
            Result = (ushort)(first >> second);
            break;
        }
      }

      public override string ToString()
      {
        return String.Format("{0} | {1} | {2} | {3} | ", Name, Operation, HasResult, Result);
      }
    }
  }
}
