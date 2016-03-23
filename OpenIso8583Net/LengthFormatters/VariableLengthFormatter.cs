using System;
using OpenIso8583Net.Formatter;

namespace OpenIso8583Net.LengthFormatters
{
    /// <summary>
    ///   Variable length formatter
    /// </summary>
    public class VariableLengthFormatter : ILengthFormatter
    {
        private readonly IFormatter _lengthFormatter;
        private readonly int _lengthIndicator;
        private readonly int _maxLength;

        /// <summary>
        ///   Variable length field formatter
        /// </summary>
        /// <param name = "lengthIndicator">Length of the length indicator</param>
        /// <param name = "maxLength">Maximum length of the field</param>
        /// <param name = "lengthFormatter">The field formatter used to pack the field</param>
        public VariableLengthFormatter(int lengthIndicator, int maxLength, IFormatter lengthFormatter)
        {
            _lengthIndicator = lengthIndicator;
            _maxLength = maxLength;
            _lengthFormatter = lengthFormatter;
            LengthOfLengthIndicator = _lengthFormatter.GetPackedLength(_lengthIndicator);
        }

        /// <summary>
        ///   Variable length field formatter that uses ASCII packing for the length indicator
        /// </summary>
        /// <param name = "lengthIndicator">Length of the length indicator</param>
        /// <param name = "maxLength">Maximum length of the field</param>
        public VariableLengthFormatter(int lengthIndicator, int maxLength)
            : this(lengthIndicator, maxLength, new AsciiFormatter())
        {
        }

        #region ILengthFormatter Members

        /// <summary>
        ///   Get the length of the packed length indicator
        /// </summary>
        public int LengthOfLengthIndicator { get; private set; }

        /// <summary>
        ///   The maximum length of the field displayed as a string for descriptors
        /// </summary>
        public string MaxLength
        {
            get { return ".." + _maxLength; }
        }

        /// <summary>
        ///   Descriptor for the length formatter used in ToString methods
        /// </summary>
        public string Description
        {
            get
            {
                var places = (int)Math.Log10(_maxLength);
                return new string('L', 1 + places) + "Var";
            }
        }

        /// <summary>
        ///   Get the length of the field
        /// </summary>
        /// <param name = "msg">Byte array of message data</param>
        /// <param name = "offset">offset to start parsing</param>
        /// <returns>The length of the field</returns>
        public int GetLengthOfField(byte[] msg, int offset)
        {
            var len = LengthOfLengthIndicator;
            var lenData = new byte[len];
            Array.Copy(msg, offset, lenData, 0, len);
            var lenStr = _lengthFormatter.GetString(lenData);
            return int.Parse(lenStr);
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
            var lengthStr = length.ToString().PadLeft(LengthOfLengthIndicator, '0');
            var header = _lengthFormatter.GetBytes(lengthStr);
            Array.Copy(header, 0, msg, offset, LengthOfLengthIndicator);
            return offset + LengthOfLengthIndicator;
        }

        /// <summary>
        ///   Check the length of the field is valid
        /// </summary>
        /// <param name = "packedLength">the packed length of the field</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValidLength(int packedLength)
        {
            return packedLength <= _maxLength;
        }

        #endregion
    }
}