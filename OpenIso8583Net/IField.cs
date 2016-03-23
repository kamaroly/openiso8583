namespace OpenIso8583Net
{
    /// <summary>
    ///   Interface representing a field
    /// </summary>
    public interface IField
    {
        /// <summary>
        ///   Gets the field number that this field representss
        /// </summary>
        int FieldNumber { get; }

        /// <summary>
        ///   Gets the packed length of the field
        /// </summary>
        int PackedLength { get; }

        /// <summary>
        ///   The Value contained in the field
        /// </summary>
        string Value { get; set; }

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        byte[] ToMsg();

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <param name = "prefix">Prefix to add onto the string</param>
        /// <returns>String representation of the field</returns>
        string ToString(string prefix);

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <returns>String representation of the field</returns>
        string ToString();

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        int Unpack(byte[] msg, int offset);
    }
}