namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Interface describing a field format validator
    /// </summary>
    public interface IFieldValidator
    {
        /// <summary>
        ///   Description of the validator
        /// </summary>
        string Description { get; }

        /// <summary>
        ///   Validates the format of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        bool IsValid(string value);
    }
}