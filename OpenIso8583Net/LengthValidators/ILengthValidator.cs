namespace OpenIso8583Net.LengthValidators
{
    /// <summary>
    ///   Interface describing a length validator
    /// </summary>
    public interface ILengthValidator
    {
        /// <summary>
        ///   Validates the length of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        bool IsValid(string value);
    }
}