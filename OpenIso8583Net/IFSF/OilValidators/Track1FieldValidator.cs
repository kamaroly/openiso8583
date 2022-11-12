using System.Text.RegularExpressions;
using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net.IFSF
{
    /// <summary>
    /// TODO: copied from track2 base, implement me.
    /// </summary>
    public class Track1FieldValidator : IFieldValidator
    {
        #region IFieldValidator Members

        public string Description { get; }

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
            var matcher = new Regex(@"^\d{1,19}[=D]([=D]|\d{4})[=D]?\d*$");
            return matcher.IsMatch(value);
        }

        #endregion
    }
}