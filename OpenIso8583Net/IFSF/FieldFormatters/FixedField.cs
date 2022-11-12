using System;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.LengthValidators;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
     public class FixedField : AField
    {
        /// <summary>
        ///   Creates a new instance of the FixedField class
        /// </summary>
        /// <param name = "length">The length of the field</param>
        /// <param name = "fieldValidator">Field validator to apply to this field</param>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        public FixedField(int fieldNumber, IFieldValidator fieldValidator, int length)
            : base(fieldNumber, fieldValidator, new AsciiFieldFormatter())
        {
            Length = length;
            _lengthValidator = new FixedLengthValidator(length);
        }

        /// <summary>
        ///   Creates a new instance of the FixedField class
        /// </summary>
        /// <param name = "length">The length of the field</param>
        /// <param name = "fieldValidator">Field validator to apply to this field</param>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldFormatter">Field formatter to apply to this field</param>
        public FixedField(int fieldNumber, IFieldValidator fieldValidator, int length, IFieldFormatter fieldFormatter)
            : base(fieldNumber, fieldValidator, fieldFormatter)
        {
            Length = length;
            _lengthValidator = new FixedLengthValidator(length);
        }

        /// <summary>
        ///   The length of the field
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        ///   Gets the packed length of the field
        /// </summary>
        public override int PackedLength
        {
            get { return Length; }
        }

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <param name = "prefix">Prefix to add onto the string</param>
        /// <returns>String representation of the field</returns>
        public override string ToString(string prefix)
        {
            return prefix + "[Fixed  *     " + Length.ToString().PadLeft(3, ' ') + " " +
                   Length.ToString().PadLeft(3, '0') + "] " + FieldNumber.ToString().PadLeft(3, '0') +
                   " [" + Value + "]";
        }

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        public override int Unpack(byte[] msg, int offset)
        {
            var buffer = new byte[Length];
            if ((msg.Length - offset) < Length)
            {
                // TODO - there are other places where things like this happen. Make a better plan.
                throw new Exception("Message is shorter than expected length. Check that all fields are present.");
            }

            Array.Copy(msg, offset, buffer, 0, Length);

            Value = _fieldFormatter.GetString(buffer);
            if (!_fieldValidator.IsValid(Value))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            return offset + Length;
        }

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        public override byte[] ToMsg()
        {
            if (!_lengthValidator.IsValid(Value))
                throw new FieldLengthException(FieldNumber, "Invalid length for field");
            if (!_fieldValidator.IsValid(Value))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            return _fieldFormatter.GetBytes(Value);
        }
    }
}