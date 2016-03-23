namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Alpha numeric printable field formatter
    /// </summary>
    public class AlphaNumericPrintableFieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "anp"; }
        }

        /// <summary>
        ///   Validates the format of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            foreach (int b in value)
            {
                if (b < 32)
                    return false;
            }
            return true;
        }

        #endregion
    }
}