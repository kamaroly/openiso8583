namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   ISO 8583 Revision 87 amount field formatter
    /// </summary>
    public class Rev87AmountFieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "amt"; }
        }

        /// <summary>
        ///   Validates the format of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            var first = value[0];
            if (first != 'C' && first != 'D')
                return false;
            var numeric = new NumericFieldValidator();
            return numeric.IsValid(value.Substring(1));
        }

        #endregion
    }
}