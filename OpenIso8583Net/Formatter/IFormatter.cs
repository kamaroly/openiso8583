namespace OpenIso8583Net.Formatter
{
    /// <summary>
    ///   Field Formatter
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        ///   Format the string and return as an encoded byte array
        /// </summary>
        /// <param name = "value">value to format</param>
        /// <returns>Encoded byte array</returns>
        byte[] GetBytes(string value);

        /// <summary>
        ///   Takes the byte array and converts it to a string for use
        /// </summary>
        /// <param name = "data">Data to convert</param>
        /// <returns>Converted data</returns>
        string GetString(byte[] data);

        /// <summary>
        ///   Gets the packed length of the data given the unpacked length
        /// </summary>
        /// <param name = "unpackedLength">Unpacked length</param>
        /// <returns>Packed length of the data</returns>
        int GetPackedLength(int unpackedLength);
    }
}