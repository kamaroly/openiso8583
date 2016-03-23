using System.Text.RegularExpressions;

namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Track 2 data field formatter
    /// </summary>
    public class Track2FieldValidator : IFieldValidator
    {
        private static readonly Regex Matcher = new Regex(@"^\d{1,19}[=D]([=D]|\d{4})[=D]?\d*$");

        #region IFieldValidator Members

        /// <summary>
        ///   Description of the validator
        /// </summary>
        public string Description
        {
            get { return "z"; }
        }

        /// <summary>
        ///   Validates the format of the given string value
        /// </summary>
        /// <param name = "value">Value to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsValid(string value)
        {
            // match up to 19 digits
            // 1 separators
            // another separator or expiry date, optional
            // another separator, optioinal
            // as many more digits as you want, which may include the service restriction code
            return Matcher.IsMatch(value);
        }

        #endregion
    }
}