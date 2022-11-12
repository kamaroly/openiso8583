using System;
using System.Text;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.LengthValidators;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
    public class VariableFieldRaw : AField
    {
        /// <summary>
        ///   Formatter to apply to the field length indicator
        /// </summary>0
        protected IFieldFormatter _lengthIndicatorFormatter;

        /// <summary>
        ///   Initialises a new instance of the VariableField class
        /// </summary>
        /// <param name = "lengthIndicator">The length indicator of the field</param>
        /// <param name = "maxLength">Maximum length of the field</param>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldValidator">Field formatter to apply to this field</param>
        public VariableFieldRaw(int fieldNumber, IFieldValidator fieldValidator, int lengthIndicator,
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
        public VariableFieldRaw(int fieldNumber, IFieldValidator fieldValidator, IFieldFormatter fieldFormatter,
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
            get { return LengthIndicator + Convert.FromBase64String(Value).Length; }
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
            byte[] binaryValue = Convert.FromBase64String(Value);
            return prefix + "[" + GetLs() + " *   " + GetMaxLength() + " " +
                   Value.Length.ToString().PadLeft(3, '0') + "] " + FieldNumber.ToString().PadLeft(3, '0') +
                   " [" + ByteArrayToString(binaryValue) + "]b";
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            for (int i = 0; i < ba.Length; i++) // <-- use for loop is faster than foreach   
                hex.Append(ba[i].ToString("X2")); // <-- ToString is faster than AppendFormat   

            return hex.ToString();
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
            var lengthBuffer = new byte[PackedLengthOfLengthIndicator];
            Array.Copy(msg, offset, lengthBuffer, 0, PackedLengthOfLengthIndicator);
            var length = int.Parse(_lengthIndicatorFormatter.GetString(lengthBuffer));
            var buffer = new byte[_fieldFormatter.GetPackedLength(length)];
            Array.Copy(msg, offset + PackedLengthOfLengthIndicator, buffer, 0, _fieldFormatter.GetPackedLength(length));

            Value = Convert.ToBase64String(buffer);
            //Value = _fieldFormatter.GetString(buffer);

            if (!_lengthValidator.IsValid(Value))
                throw new FieldLengthException(FieldNumber, "Incorrect field length in incoming message");
            if (!_fieldValidator.IsValid(Value))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");

            return offset + PackedLengthOfLengthIndicator + _fieldFormatter.GetPackedLength(length);
        }

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        public override byte[] ToMsg()
        {
            byte[] originalField = Convert.FromBase64String(Value);
            var len = originalField.Length.ToString().PadLeft(LengthIndicator, '0');

            var lenBytes = _lengthIndicatorFormatter.GetBytes(len);
            var valBytes = originalField;

            if (!_lengthValidator.IsValid(new string('0', originalField.Length)))
                throw new FieldLengthException(FieldNumber, "Value too long");
            if (!_fieldValidator.IsValid(new string('0', originalField.Length)))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            var msg = new byte[lenBytes.Length + valBytes.Length];
            Array.Copy(lenBytes, msg, lenBytes.Length);
            Array.Copy(valBytes, 0, msg, lenBytes.Length, valBytes.Length);
            return msg;
            //var len = Value.Length.ToString().PadLeft(LengthIndicator, '0');

            //var lenBytes = _lengthIndicatorFormatter.GetBytes(len);
            //var valBytes = _fieldFormatter.GetBytes(Value);

            //if (!_lengthValidator.IsValid(Value))
            //    throw new FieldLengthException(FieldNumber, "Value too long");
            //if (!_fieldValidator.IsValid(Value))
            //    throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            //var msg = new byte[lenBytes.Length + valBytes.Length];
            //Array.Copy(lenBytes, msg, lenBytes.Length);
            //Array.Copy(valBytes, 0, msg, lenBytes.Length, valBytes.Length);
            //return msg;
        }
    }
}