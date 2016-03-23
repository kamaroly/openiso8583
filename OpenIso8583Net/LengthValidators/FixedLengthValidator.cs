namespace OpenIso8583Net.LengthValidators
{
    /// <summary>
    ///   Class to validate a fixed length
    /// </summary>
    public class FixedLengthValidator : ILengthValidator
    {
        private readonly int _length;

        /// <summary>
        ///   Creates a new instance of the FixedLengthValidator class
        /// </summary>
        /// <param name = "length">Desired length</param>
        public FixedLengthValidator(int length)
        {
            _length = length;
        }

        #region ILengthValidator Members

        /// <summary>
        ///   Validates the length of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            return value.Length == _length;
        }

        #endregion
    }
}