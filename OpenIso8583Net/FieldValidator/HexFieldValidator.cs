namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Hex field formatter
    /// </summary>
    public class HexFieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "hex"; }
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
                if (b < 48 || b > 57 && b < 65 || b > 70 && b < 97 || b > 102)
                    return false;
            }
            return true;
        }

        #endregion
    }
}