using System;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
    public class BinaryFieldFormatter : IFieldFormatter
    {
        #region IFieldFormatter Members

        byte[] IFieldFormatter.GetBytes(string value)
        {
            return GetBytes(value);
        }

        string IFieldFormatter.GetString(byte[] data)
        {
            return GetString(data);
        }

        int IFieldFormatter.GetPackedLength(int unpackedLength)
        {
            return GetPackedLength(unpackedLength);
        }

        #endregion

        /// <summary>
        ///   Format the string and return as an encoded byte array
        /// </summary>
        /// <param name = "value">value to format</param>
        /// <returns>Encoded byte array</returns>
        public static byte[] GetBytes(string value)
        {
            if (!Validators.IsHex(value))
                throw new FormatException("Value is not valid HEX");

            var numberChars = value.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        ///   Takes the byte array and converts it to a string for use
        /// </summary>
        /// <param name = "data">Data to convert</param>
        /// <returns>Converted data</returns>
        public static string GetString(byte[] data)
        {
            var hex = BitConverter.ToString(data);
            return hex.Replace("-", "");
        }

        /// <summary>
        ///   Gets the packed length of the data given the unpacked length
        /// </summary>
        /// <param name = "unpackedLength">Unpacked length</param>
        /// <returns>Packed length of the data</returns>
        public static int GetPackedLength(int unpackedLength)
        {
            return unpackedLength / 2;
        }
    }
}