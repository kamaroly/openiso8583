using System;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
    /// <summary>
    ///   Represents a binary packed fixed length field in an ISO message
    /// </summary>
    public class BinaryFixedField : FixedField
    {
        /// <summary>
        ///   Create a new instance of the BinaryFixedFieldClass
        /// </summary>
        /// <param name = "fieldNumber">Field Number</param>
        /// <param name = "length">Length of the packed field in bytes, NOT the value it is set to</param>
        public BinaryFixedField(int fieldNumber, int length)
            : base(fieldNumber, new HexFieldValidator(), length * 2, new BinaryFieldFormatter())
        {
        }

        /// <summary>
        ///   Gets the packed length of the field
        /// </summary>
        public override int PackedLength
        {
            get { return base.PackedLength / 2; }
        }

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        public override int Unpack(byte[] msg, int offset)
        {
            var length = Length / 2;
            var buffer = new byte[length];
            //patch felicio if ((msg.Length - offset) < Length)
            if ((msg.Length - offset) < length)
            {
                // TODO - there are other places where things like this happen. Make a better plan.
                throw new Exception("Message is shorter than expected length. Check that all fields are present.");
            }

            Array.Copy(msg, offset, buffer, 0, length);

            Value = _fieldFormatter.GetString(buffer);
            if (!_fieldValidator.IsValid(Value))
                throw new FieldFormatException(FieldNumber, "Invalid format for field. Value was: [" + Value + "]");
            return offset + length;
        }
    }
}