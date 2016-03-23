using System;
using System.Text;

namespace OpenIso8583Net
{
    /// <summary>
    /// Utilities class with helper functions
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Returns the luhn digit for a given PAN
        /// </summary>
        /// <param name="pan">PAN missing the luhn check digit</param>
        /// <returns>Luhn check digit</returns>
        public static string GetLuhn(string pan)
        {
            int sum = 0;

            bool alternate = true;
            for (int i = pan.Length - 1; i >= 0; i--)
            {
                int num = int.Parse(pan[i].ToString());

                if (alternate)
                {
                    num *= 2;
                    if (num > 9)
                        num = num - 9;
                }

                sum += num;
                alternate = !alternate;
            }

            int luhnDigit = 10 - (sum % 10);
            if (luhnDigit == 10)
                luhnDigit = 0;

            return luhnDigit.ToString();
        }

        /// <summary>
        /// Checks that the luhn check digit is valid
        /// </summary>
        /// <param name="pan">PAN to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValidPAN(String pan)
        {
            string luhn = GetLuhn(pan.Substring(0, pan.Length - 1));
            return luhn == pan.Substring(pan.Length - 1);
        }

        /// <summary>
        /// PCI DSS PAN mask. For strings longer than 10 chars masks characters [6..Length-4] 
        /// by character 'x'; otherwise returns the pan parameter unchanged.
        /// </summary>
        /// <param name="pan">a PAN string</param>
        /// <returns>a masked PAN string</returns>
        public static string MaskPan(string pan)
        {
            if (pan == null)
                return null;

            const int frontLength = 6;
            const int endLength = 4;
            const int unmaskedLength = frontLength + endLength;

            var totalLength = pan.Length;

            if (totalLength <= unmaskedLength)
                return pan;

            return
                new StringBuilder(totalLength, totalLength)
                    .Append(pan.Substring(0, frontLength)) // front
                    .Append(new string('x', totalLength - unmaskedLength))  // mask
                    .Append(pan.Substring((totalLength - endLength), endLength)) // end
                    .ToString();
        }

          /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        /// <param name="data">
        /// The data 
        /// </param>
        /// <returns>
        /// Hex string representing the input data 
        /// </returns>
        public static string ToHex(this byte[] data)
        {
            var hex = BitConverter.ToString(data);
            return hex.Replace("-", string.Empty);
        }

        /// <summary>
        /// Debug print a byte array
        /// </summary>
        /// <param name="data">
        /// The data to pring 
        /// </param>
        /// <returns>
        /// Debug output string 
        /// </returns>
        public static string DebugPrint(byte[] data)
        {
            var sb = new StringBuilder();

            var numberOfLines = (data.Length / 16) + 1;
            for (int line = 0; line < numberOfLines; line++)
            {
                var lineOffset = line * 16;
                sb.Append(Convert.ToString(lineOffset, 16).PadLeft(5, '0'));
                sb.Append("  ");

                int endOffset = lineOffset + 16;
                if (endOffset > data.Length)
                {
                    endOffset = data.Length;
                }

                var textBuilder = new StringBuilder();

                for (int i = lineOffset; i < endOffset; i++)
                {
                    var b = data[i];
                    sb.Append(Convert.ToString(b, 16).ToUpper().PadLeft(2, '0'));
                    sb.Append(" ");
                    textBuilder.Append(GetChar(b));
                }

                if (endOffset != lineOffset + 16)
                {
                    sb.Append(' ', (lineOffset + 16 - endOffset) * 3);
                }

                sb.Append(" ");
                sb.Append(textBuilder);

                sb.Append(Environment.NewLine);
            }

            var str = sb.ToString();
            return str.Substring(0, str.Length - Environment.NewLine.Length);
        }

        /// <summary>
        /// Convert a hex string to byte array.
        /// </summary>
        /// <param name="hex">
        /// The string 
        /// </param>
        /// <returns>
        /// Byte array representing the input string 
        /// </returns>
        public static byte[] ToByteArray(this string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

       /// <summary>
        /// Get a printable character for a byte. Used in DebugPrint
        /// </summary>
        /// <param name="b">
        /// The byte 
        /// </param>
        /// <returns>
        /// The character 
        /// </returns>
        private static char GetChar(byte b)
        {
            if (b < 0x20 || b > 126)
            {
                return '.';
            }

            return (char)b;
        }
    }
}
