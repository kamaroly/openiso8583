namespace OpenIso8583Net.LengthValidators
{
    /// <summary>
    ///   Class to validate a fixed length
    /// </summary>
    public class VariableLengthValidator : ILengthValidator
    {
        private readonly int _maxLength;
        private readonly int _minLength;

        /// <summary>
        ///   Creates a new instance of the VariableLengthValidator class
        /// </summary>
        /// <param name = "maximumLength">Maximum length</param>
        public VariableLengthValidator(int maximumLength) : this(0, maximumLength)
        {
        }

        /// <summary>
        ///   Creates a new instance of the VariableLengthValidator class
        /// </summary>
        /// <param name = "maximumLength">Maximum length</param>
        /// <param name = "minimumLength">Minimum length</param>
        public VariableLengthValidator(int minimumLength, int maximumLength)
        {
            _minLength = minimumLength;
            _maxLength = maximumLength;
        }

        #region ILengthValidator Members

        /// <summary>
        ///   Validates the length of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            return value.Length >= _minLength && value.Length <= _maxLength;
        }

        #endregion
    }
}