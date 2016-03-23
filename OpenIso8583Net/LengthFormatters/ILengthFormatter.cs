namespace OpenIso8583Net.LengthFormatters
{
    /// <summary>
    ///   Interface describing a length formatter
    /// </summary>
    public interface ILengthFormatter
    {
        /// <summary>
        ///   Get the length of the packed length indicator
        /// </summary>
        int LengthOfLengthIndicator { get; }

        /// <summary>
        ///   The maximum length of the field displayed as a string for descriptors
        /// </summary>
        string MaxLength { get; }

        /// <summary>
        ///   Descriptor for the length formatter used in ToString methods
        /// </summary>
        string Description { get; }

        /// <summary>
        ///   Get the length of the field
        /// </summary>
        /// <param name = "msg">Byte array of message data</param>
        /// <param name = "offset">offset to start parsing</param>
        /// <returns>The length of the field</returns>
        int GetLengthOfField(byte[] msg, int offset);

        /// <summary>
        ///   Pack the length header into the message
        /// </summary>
        /// <param name = "msg">Byte array of the message</param>
        /// <param name = "length">The length to pack into the message</param>
        /// <param name = "offset">Offset to start the packing</param>
        /// <returns>offset for the start of the field</returns>
        int Pack(byte[] msg, int length, int offset);

        /// <summary>
        ///   Check the length of the field is valid
        /// </summary>
        /// <param name = "packedLength">the packed length of the field</param>
        /// <returns>true if valid, false otherwise</returns>
        bool IsValidLength(int packedLength);
    }
}