using System;

namespace OpenIso8583Net
{
    /// <summary>
    /// Allows for an arbitrary transformation of a Field value before it is set
    /// </summary>
    public class LambdaAdjuster : Adjuster
    {
        private readonly Func<string, string> _getLambda;
        private readonly Func<string, string> _setLambda;

        /// <summary>
        /// Allows an arbitrary transformation of a Field value before it is set
        /// </summary>
        /// <param name="getLambda">fieldValue => adjustedFieldValue; get adjustment lambda expression</param>
        /// <param name="setLambda">fieldValue => adjustedFieldValue; set adjustment lambda expression</param>
        public LambdaAdjuster(Func<string,string> getLambda = null, Func<string,string> setLambda = null)
        {
            _getLambda = getLambda;
            _setLambda = setLambda;
        }

        /// <summary>
        /// Transforms a Field value while getting
        /// </summary>
        /// <param name="value">the actual, stored Field value</param>
        /// <returns>a value that shall be returned</returns>
        public override string Get(string value)
        {
            return _getLambda == null ? value : _getLambda(value);
        }

        /// <summary>
        /// Transforms a Field value while setting
        /// </summary>
        /// <param name="value">a value to be adjusted before set</param>
        /// <returns>the actual value that will be set</returns>
        public override string Set(string value)
        {
            return _setLambda == null ? value : _setLambda(value);
        }
    }
}
