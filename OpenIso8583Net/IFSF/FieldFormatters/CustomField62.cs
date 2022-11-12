using System;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.LengthValidators;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
      class CustomField62 : AField
    {
        /// <summary>
        ///   Formatter to apply to the field length indicator
        /// </summary>
        protected IFieldFormatter _lengthIndicatorFormatter;

        /// <summary>
        ///   Initialises a new instance of the VariableField class
        /// </summary>
        /// <param name = "lengthIndicator">The length indicator of the field</param>
        /// <param name = "maxLength">Maximum length of the field</param>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldValidator">Field formatter to apply to this field</param>
        public CustomField62(int fieldNumber, IFieldValidator fieldValidator, int lengthIndicator,
            int maxLength)
            : this(
                fieldNumber, fieldValidator, new AsciiFieldFormatter(), new AsciiFieldFormatter(), lengthIndicator,
                maxLength)
        {
        }

        /// <summary>
        ///   Initialises a new instance of the VariableField class
        /// </summary>
        /// <param name = "lengthIndicator">The length indicator of the field</param>
        /// <param name = "maxLength">Maximum length of the field</param>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldValidator">Field validator to apply to this field</param>
        /// <param name = "fieldFormatter">Field formatter to apply to this field</param>
        /// <param name = "lengthIndicatorFormatter">Length indicator to apply to this field</param>
        public CustomField62(int fieldNumber, IFieldValidator fieldValidator, IFieldFormatter fieldFormatter,
            IFieldFormatter lengthIndicatorFormatter, int lengthIndicator, int maxLength)
            : base(fieldNumber, fieldValidator, fieldFormatter)
        {
            LengthIndicator = lengthIndicator;
            MaxLength = maxLength;
            _lengthValidator = new VariableLengthValidator(maxLength);
            this._lengthIndicatorFormatter = lengthIndicatorFormatter;
        }


        /// <summary>
        ///   The length indicator of the field
        /// </summary>
        public virtual int LengthIndicator { get; private set; }

        /// <summary>
        ///   The maximum length of the field
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        ///   Gets the packed length of the field including the length indicator
        /// </summary>
        public override int PackedLength
        {
            get { return LengthIndicator + Value.Length; }
        }

        /// <summary>
        ///   Shows the packed length of the length indicator
        /// </summary>
        public int PackedLengthOfLengthIndicator
        {
            get { return _lengthIndicatorFormatter.GetPackedLength(LengthIndicator); }
        }


        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <param name = "prefix">Prefix to add onto the string</param>
        /// <returns>String representation of the field</returns>
        public override string ToString(string prefix)
        {
            return prefix + "[" + GetLs() + " *   " + GetMaxLength() + " " +
                   Value.Length.ToString().PadLeft(3, '0') + "] " + FieldNumber.ToString().PadLeft(3, '0') +
                   " [" + Value + "]";
        }

        private string GetMaxLength()
        {
            var s = ".." + MaxLength;
            return s.PadLeft(5, ' ');
        }

        private string GetLs()
        {
            var s = new string('L', LengthIndicator) + "VAR";
            return s.PadRight(6, ' ');
        }

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        public override int Unpack(byte[] msg, int offset)
        {
            var lengthProducts = new byte[2];
            Array.Copy(msg, offset, lengthProducts, 0, 2);
            var length = int.Parse(_lengthIndicatorFormatter.GetString(lengthProducts)) * 3 + 2;
            length++; //DeviceType  

            var lengthMsgTxt = new byte[3];
            Array.Copy(msg, offset + length, lengthMsgTxt, 0, 3);
            length += int.Parse(_lengthIndicatorFormatter.GetString(lengthMsgTxt)) + 3;
            byte[] binaryMessage = new byte[length];
            Array.Copy(msg, offset, binaryMessage, 0, length);
            Value = _fieldFormatter.GetString(binaryMessage);
            return offset + length;
        }

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        public override byte[] ToMsg()
        {
            var len = Value.Length.ToString().PadLeft(LengthIndicator, '0');

            var lenBytes = _lengthIndicatorFormatter.GetBytes(len);
            var valBytes = _fieldFormatter.GetBytes(Value);

            if (!_lengthValidator.IsValid(Value))
                throw new FieldLengthException(FieldNumber, "Value too long");
            if (!_fieldValidator.IsValid(Value))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            var msg = new byte[lenBytes.Length + valBytes.Length];
            Array.Copy(lenBytes, msg, lenBytes.Length);
            Array.Copy(valBytes, 0, msg, lenBytes.Length, valBytes.Length);
            return msg;
        }
    }
}