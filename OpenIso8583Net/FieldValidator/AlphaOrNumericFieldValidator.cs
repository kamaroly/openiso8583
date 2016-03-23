namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Alpha or Numeric field validator
    /// </summary>
    public class AlphaOrNumericFieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "a or n"; }
        }

        /// <summary>
        ///   Validates the format of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            bool isAllAlpha = true;
            bool isAllNumeric = true;

            foreach (int b in value)
            {
                if (b < 48 || b > 57)
                {
                    isAllNumeric = false;
                }

                if (b < 65 || b > 90 && b < 97 || b > 122)
                {
                    isAllAlpha = false;
                }

                if (!isAllAlpha && !isAllNumeric)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}