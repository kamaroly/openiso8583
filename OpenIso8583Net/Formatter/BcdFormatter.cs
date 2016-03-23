using System;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenIso8583Net.Formatter
{
    /// <summary>
    ///   This class will format ASCII to BCD
    /// </summary>
    public class BcdFormatter : IFormatter
    {
        #region IFormatter Members

        /// <summary>
        ///   Format the string and return a byte array
        /// </summary>
        /// <param name = "value">value to format</param>
        /// <returns>Encoded byte array</returns>
        public byte[] GetBytes(string value)
        {
            if (value.Length % 2 == 1)
                value = value.PadLeft(value.Length + 1, '0');
            var chars = value.ToCharArray();
            var len = chars.Length / 2;
            var bytes = new byte[len];

            for (var i = 0; i < len; i++)
            {
                var highNibble = byte.Parse(chars[2 * i].ToString());
                var lowNibble = byte.Parse(chars[2 * i + 1].ToString());
                bytes[i] = (byte)((byte)(highNibble << 4) | lowNibble);
            }

            return bytes;
        }

        /// <summary>
        ///   Return a string from the given byte array
        /// </summary>
        /// <param name = "data">encoded byte array</param>
        /// <returns>value to return</returns>
        public string GetString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        /// <summary>
        /// Get the packed length of a data item
        /// </summary>
        /// <param name="unpackedLength">Unpacked length</param>
        /// <returns>Packed length</returns>
        public int GetPackedLength(int unpackedLength)
        {
            double len = unpackedLength;
            return (int)Math.Ceiling(len / 2);
        }

        #endregion
    }
}