namespace OpenIso8583Net
{
    using FieldValidator;
    using Formatter;
    using LengthFormatters;

    /// <summary>
    /// Decorates an IFieldDescriptor.Display method with a PCI-DSS PAN mask.
    /// </summary>
    public class PanMaskDecorator : IFieldDescriptor
    {
        private readonly IFieldDescriptor _decoratedFieldDescriptor;

        /// <summary>
        /// Decorate the decoratedFieldDescriptor.Display with a PCI-DSS PAN mask.
        /// </summary>
        /// <param name="decoratedFieldDescriptor">the target IFieldDescriptor to decorate</param>
        public PanMaskDecorator(IFieldDescriptor decoratedFieldDescriptor)
        {
            _decoratedFieldDescriptor = decoratedFieldDescriptor;
        }

        /// <summary>
        ///   The length formatter describing the field
        /// </summary>
        public ILengthFormatter LengthFormatter
        {
            get { return _decoratedFieldDescriptor.LengthFormatter; }
        }

        /// <summary>
        ///   The validator describing the field
        /// </summary>
        public IFieldValidator Validator
        {
            get { return _decoratedFieldDescriptor.Validator; }
        }

        /// <summary>
        ///   The field formatter describing the field
        /// </summary>
        public IFormatter Formatter
        {
            get { return _decoratedFieldDescriptor.Formatter; }
        }

        /// <summary>
        ///   Get the packed length of the field, including a length header if necessary for the given value
        /// </summary>
        /// <param name = "value">Value to calculate length for</param>
        /// <returns>Packed length of the field, including length header</returns>
        public int GetPackedLength(string value)
        {
            return _decoratedFieldDescriptor.GetPackedLength(value);
        }

        /// <summary>
        ///   Display the field after applying the PCI-DSS PAN mask, used in traces
        /// </summary>
        /// <param name = "prefix">Prefix to display before the field</param>
        /// <param name = "fieldNumber">Field number</param>
        /// <param name = "value">field contents</param>
        /// <returns>formatted string representing the field</returns>
        public string Display(string prefix, int fieldNumber, string value)
        {
            return _decoratedFieldDescriptor.Display(prefix, fieldNumber, Utils.MaskPan(value));
        }

        /// <summary>
        ///   Unpack the field from the message
        /// </summary>
        /// <param name = "fieldNumber">field number</param>
        /// <param name = "data">message data to unpack from</param>
        /// <param name = "offset">offset in the message to start</param>
        /// <param name = "newOffset">offset at the end of the field for the next field</param>
        /// <returns>valud of the field</returns>
        public string Unpack(int fieldNumber, byte[] data, int offset, out int newOffset)
        {
            return _decoratedFieldDescriptor.Unpack(fieldNumber, data, offset, out newOffset);
        }

        /// <summary>
        ///   Packs the field into a byte[]
        /// </summary>
        /// <param name = "fieldNumber">number of the field</param>
        /// <param name = "value">Value of the field to pack</param>
        /// <returns>field data packed into a byte[]</returns>
        public byte[] Pack(int fieldNumber, string value)
        {
            return _decoratedFieldDescriptor.Pack(fieldNumber, value);
        }

        /// <summary>
        /// delegated Adjuster
        /// </summary>
        public Adjuster Adjuster
        {
            get { return _decoratedFieldDescriptor.Adjuster; }
        }
    }
}