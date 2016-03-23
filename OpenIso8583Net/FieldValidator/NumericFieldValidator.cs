namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Numeric field formatter
    /// </summary>
    public class NumericFieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "n"; }
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
                if (b < 48)
                    return false;
                if (b > 57)
                    return false;
            }
            return true;
        }

        #endregion
    }
}