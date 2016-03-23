using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Class representing a field
    /// </summary>
    public class Field : IField
    {
        /// <summary>
        ///   Field descriptor
        /// </summary>
        protected IFieldDescriptor _fieldDescriptor;

        /// <summary>
        ///   Creates a new instance of the Field class
        /// </summary>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldDescriptor">Field descriptor</param>
        public Field(int fieldNumber, IFieldDescriptor fieldDescriptor)
        {
            FieldNumber = fieldNumber;
            _fieldDescriptor = fieldDescriptor;
        }

        #region IField Members

        private string _value;
        /// <summary>
        ///   The Value contained in the field
        /// </summary>
        /// adjustment is skipped if there is no Adjuster
        public string Value 
        { 
            get 
            { 
                return _fieldDescriptor.Adjuster == null ? _value : _fieldDescriptor.Adjuster.Get(_value); 
            }
            set 
            {
                _value = _fieldDescriptor.Adjuster == null ? value : _fieldDescriptor.Adjuster.Set(value);
            }
        }

        /// <summary>
        ///   Gets the field number that this field representss
        /// </summary>
        public int FieldNumber { get; private set; }

        /// <summary>
        ///   Gets the packed length of the field
        /// </summary>
        public int PackedLength
        {
            get { return _fieldDescriptor.GetPackedLength(Value); }
        }

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <returns>String representation of the field</returns>
        public override string ToString()
        {
            return ToString(string.Empty);
        }

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <param name = "prefix">Prefix to add onto the string</param>
        /// <returns>String representation of the field</returns>
        public string ToString(string prefix)
        {
            return _fieldDescriptor.Display(prefix, FieldNumber, Value);
        }

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        public int Unpack(byte[] msg, int offset)
        {
            int newOffset;
            Value = _fieldDescriptor.Unpack(FieldNumber, msg, offset, out newOffset);

            return newOffset;
        }

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        public byte[] ToMsg()
        {
            return _fieldDescriptor.Pack(FieldNumber, Value);
        }

        #endregion

        ///<summary>
        ///  Create an ASCII fixed length field descriptor
        ///</summary>
        ///<param name = "fieldNumber">Field number</param>
        ///<param name = "packedLength">The packed length of the field.  For BCD fields, this is half the size of the field you want</param>
        ///<param name = "validator">Validator to use on the field</param>
        ///<returns>field</returns>
        public static IField AsciiFixed(int fieldNumber, int packedLength, IFieldValidator validator)
        {
            return new Field(fieldNumber, FieldDescriptor.AsciiFixed(packedLength, validator));
        }

        /// <summary>
        ///   Create an ASCII variable length field
        /// </summary>
        /// <param name = "fieldNumber">field number</param>
        /// <param name = "lengthIndicator">length indicator</param>
        /// <param name = "maxLength">maximum length of field</param>
        /// <param name = "validator">validator to use on the field</param>
        /// <returns>field</returns>
        public static IField AsciiVar(int fieldNumber, int lengthIndicator, int maxLength, IFieldValidator validator)
        {
            return new Field(fieldNumber, FieldDescriptor.AsciiVar(lengthIndicator, maxLength, validator));
        }

        /// <summary>
        ///   Create a binary fixed length field
        /// </summary>
        /// <param name = "fieldNumber">field number</param>
        /// <param name = "packedLength">length of the field</param>
        /// <returns>field</returns>
        public static IField BinFixed(int fieldNumber, int packedLength)
        {
            return new Field(fieldNumber, FieldDescriptor.BinaryFixed(packedLength));
        }
    }
}