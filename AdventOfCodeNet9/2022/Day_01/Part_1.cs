using System.Diagnostics;

namespace AdventOfCodeNet9._2022.Day_01
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2022/day/1
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";

      CRC10 crc10 = new CRC10();
      // Example messages to calculate CRC
      byte[] message1 = { 0x01, 0x00, 0x00, 0xFF, 0x03, 0x00 }; // CFGA IC 1 message
      byte[] message2 = { 0x00, 0xF8, 0x7F, 0x00, 0x00, 0x00 }; // CFGB IC 1 message

      ushort crc1 = crc10.calculate_crc10(message1, 0x0010);
      ushort crc2 = crc10.calculate_crc10(message2, 0x0010);

      // Print the CRC value
      Debug.WriteLine($"The CRC for CFGA IC 1 is: 0x{crc1:X3} (expected 0x005A)");
      Debug.WriteLine($"The CRC for CFGB IC 1 is: 0x{crc2:X3} (expected 0x033D)");
      
      return result;
    }


    public class CRC10
    {
      const ushort CRC10_POLY = 0x02EB;
      const int BITS_IN_BYTE = 8;
      const int CRC_BIT_LENGTH = 10;

      // Assuming the lookup table is stored in this array
      private ushort[] _crc10Table;

      public CRC10()
      {
        _crc10Table = GenerateCrc10Table();
      }

      /*
      private static ushort[] GenerateCrc10Table()
      {
        ushort[] crc10Table = new ushort[256];

        for (int i = 0; i < 256; i++)
        {
          ushort remainder = (ushort)(i << (CRC_BIT_LENGTH - BITS_IN_BYTE));

          for (int bit = 0; bit < BITS_IN_BYTE; bit++)
          {
            if ((remainder & (1 << (CRC_BIT_LENGTH - 1))) != 0)
            {
              remainder = (ushort)((remainder << 1) ^ CRC10_POLY);
            }
            else
            {
              remainder = (ushort)(remainder << 1);
            }
          }

          crc10Table[i] = (ushort)(remainder & ((1 << CRC_BIT_LENGTH) - 1));
        }

        return crc10Table;
      }
      */

      private static ushort[] GenerateCrc10Table()
      {
        ushort[] table = new ushort[256];
        for (int i = 0; i < 256; i++)
        {
          // Shift to align with the top of the CRC register
          ushort crc = (ushort)(i << 2); // Align to top 10 bits for 8-bit data

          for (int j = 0; j < 8; j++)
          {
            crc = (crc & 0x200) != 0 ? // Check if the leftmost (10th) bit is set
              (ushort)((crc << 1) ^ 0x2EB) :
              (ushort)((crc << 1));
          }
          table[i] = (ushort)(crc & 0x03FF); // Keep it within 10 bits
        }
        return table;
      }

      /*
      // Method to calculate CRC10
      public static ushort calculate_crc10(byte[] message, ushort initialRemainder)
      {
        ushort remainder = initialRemainder;

        // Iterate through each byte of the message
        foreach (byte b in message)
        {
          byte data = (byte)(b ^ (remainder >> 6));
          remainder = crc10Table[data];
          remainder ^= (ushort)(remainder << 8);
        }

        // Return the final, right-aligned remainder
        return (ushort)(remainder & 0x03FF); // Ensure it's a 10-bit result
      }
      */
      /*
      public ushort calculate_crc10(byte[] data, ushort init = 0x0010)
      {
        ushort crc = init;
        foreach (byte b in data)
        {
          byte index = (byte)((crc >> 2) ^ b); // Align with top of CRC register
          crc = (ushort)((crc << 8) ^ _crc10Table[index]);
        }
        return crc;
      }
      */
      public ushort calculate_crc10(byte[] message, ushort initialCrc = 0x0010)
      {
        ushort crc = initialCrc;
        foreach (byte b in message)
        {
          byte index = (byte)((crc <<2) ^ b << 2); // Shift by 7 bits to align with CRC register's top
          crc = (ushort)((crc << 8) ^ _crc10Table[index]);
          crc &= 0x3FF;
        }
        return (ushort)(crc); // Ensure the CRC is within 10 bits
      }
    }
  }
}
