using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Class for accessing validators outside of the message
    /// </summary>
    public static class Validators
    {
        /// <summary>
        ///   Numeric validator
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsNumeric(string value)
        {
            var v = new NumericFieldValidator();
            return v.IsValid(value);
        }

        /// <summary>
        ///   Hex validator
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsHex(string value)
        {
            var v = new HexFieldValidator();
            return v.IsValid(value);
        }
    }
}