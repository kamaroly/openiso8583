namespace OpenIso8583Net.LengthFormatters
{
    /// <summary>
    ///   Fixed field formatter
    /// </summary>
    public class FixedLengthFormatter : ILengthFormatter
    {
        private readonly int _packedLength;

        ///<summary>
        ///  Fixed length field formatter
        ///</summary>
        ///<param name = "packedLength">The packed length of the field.  For BCD fields, this is half the size of the field you want</param>
        public FixedLengthFormatter(int packedLength)
        {
            _packedLength = packedLength;
        }

        #region ILengthFormatter Members

        /// <summary>
        ///   Get the length of the packed length indicator.  For fixed length fields this is 0
        /// </summary>
        public int LengthOfLengthIndicator
        {
            get { return 0; }
        }

        /// <summary>
        ///   The maximum length of the field displayed as a string for descriptors
        /// </summary>
        public string MaxLength
        {
            get { return _packedLength.ToString(); }
        }

        /// <summary>
        ///   Descriptor for the length formatter used in ToString methods
        /// </summary>
        public string Description
        {
            get { return "Fixed"; }
        }

        /// <summary>
        ///   Get the length of the field
        /// </summary>
        /// <param name = "msg">Byte array of message data</param>
        /// <param name = "offset">offset to start parsing</param>
        /// <returns>The length of the field</returns>
        public int GetLengthOfField(byte[] msg, int offset)
        {
            return _packedLength;
        }

        /// <summary>
        ///   Pack the length header into the message
        /// </summary>
        /// <param name = "msg">Byte array of the message</param>
        /// <param name = "length">The length to pack into the message</param>
        /// <param name = "offset">Offset to start the packing</param>
        /// <returns>offset for the start of the field</returns>
        public int Pack(byte[] msg, int length, int offset)
        {
            return offset;
        }

        /// <summary>
        ///   Check the length of the field is valid
        /// </summary>
        /// <param name = "packedLength">the packed length of the field</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValidLength(int packedLength)
        {
            return packedLength == _packedLength;
        }

        #endregion
    }
}