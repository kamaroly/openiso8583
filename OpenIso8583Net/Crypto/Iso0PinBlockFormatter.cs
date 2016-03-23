using System;
using OpenIso8583Net.Formatter;

namespace OpenIso8583Net.Crypto
{
    /// <summary>
    ///   Class dealing with creating an ISO-0 PIN Block
    /// </summary>
    public class Iso0PinBlockFormatter
    {
        /// <summary>
        ///   Creates a clear PIN block of the given PAN and PIN
        /// </summary>
        /// <param name = "pan">Primary Account Number</param>
        /// <param name = "pin">Personal Identification Number</param>
        /// <returns>Clear PIN Block</returns>
        public static string CreatePinBlock(string pan, string pin)
        {
            string line1 = pin.Length.ToString().PadLeft(2, '0') + pin;
            line1 = line1.PadRight(16, 'F');
            string line2 = String.Format("0000{0}", pan.Substring(pan.Length - 13, 12));

            byte[] byte1 = BinaryFormatter.GetBytes(line1);
            byte[] byte2 = BinaryFormatter.GetBytes(line2);

            var result = new byte[8];
            for (int i = 0; i < 8; i++)
                result[i] = (byte) (byte1[i] ^ byte2[i]);

            return BinaryFormatter.GetString(result);
        }
    }
}