using System.Text;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
    public class AsciiFieldFormatter : IFieldFormatter
    {
        #region IFieldFormatter Members

        /// <summary>
        ///   Format the string and return as an encoded byte array
        /// </summary>
        /// <param name = "value">value to format</param>
        /// <returns>Encoded byte array</returns>
        public byte[] GetBytes(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        ///   Takes the byte array and converts it to a string for use
        /// </summary>
        /// <param name = "data">Data to convert</param>
        /// <returns>Converted data</returns>
        public string GetString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        ///   Gets the packed length of the data given the unpacked length
        /// </summary>
        /// <param name = "unpackedLength">Unpacked length</param>
        /// <returns>Packed length of the data</returns>
        public int GetPackedLength(int unpackedLength)
        {
            return unpackedLength;
        }

        #endregion
    }
}