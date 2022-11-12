using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.LengthValidators;

namespace OpenIso8583Net.IFSF.FieldFormatters
{
       public abstract class AField : IField
    {
        /// <summary>
        ///   Field formatter to apply to this field
        /// </summary>
        protected IFieldFormatter _fieldFormatter;

        /// <summary>
        ///   Field validator to apply to this field
        /// </summary>
        protected IFieldValidator _fieldValidator;

        /// <summary>
        ///   The length validator applied to the field
        /// </summary>
        protected ILengthValidator _lengthValidator;

        /// <summary>
        ///   Creates a new instance of the AField class
        /// </summary>
        /// <param name = "fieldNumber">The field number that this object represents</param>
        /// <param name = "fieldValidator">Field validator to apply to this field</param>
        /// <param name = "fieldFormatter">Field formatter to apply to this field</param>
        protected AField(int fieldNumber, IFieldValidator fieldValidator, IFieldFormatter fieldFormatter)
        {
            FieldNumber = fieldNumber;
            _fieldValidator = fieldValidator;
            _fieldFormatter = fieldFormatter;
        }

        #region IField Members

        /// <summary>
        ///   The Value contained in the field
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///   Gets the field number that this field representss
        /// </summary>
        public int FieldNumber { get; private set; }

        /// <summary>
        ///   Gets the packed length of the field
        /// </summary>
        public abstract int PackedLength { get; }

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <returns>String representation of the field</returns>
        public override string ToString()
        {
            return ToString("");
        }

        /// <summary>
        ///   Gets a representation of the field as a string
        /// </summary>
        /// <param name = "prefix">Prefix to add onto the string</param>
        /// <returns>String representation of the field</returns>
        public abstract string ToString(string prefix);

        /// <summary>
        ///   Unpacks the field from the message
        /// </summary>
        /// <param name = "msg">byte[] of the full message</param>
        /// <param name = "offset">offset indicating the start of the field</param>
        /// <returns>new offset in message to start unpacking the next field</returns>
        public abstract int Unpack(byte[] msg, int offset);

        /// <summary>
        ///   Returns a byte[] representation of the field
        /// </summary>
        /// <returns>byte[] representing the field</returns>
        public abstract byte[] ToMsg();

        #endregion
    }
}