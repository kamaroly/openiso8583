// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Adjuster.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Instances will be consulted when getting and setting a Field value
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net
{
    /// <summary>
    /// Instances will be consulted when getting and setting a Field value
    /// </summary>
    public abstract class Adjuster
    {
        #region Public Methods and Operators

        /// <summary>
        /// Transforms a Field value while setting
        /// </summary>
        /// <param name="value">
        /// actual, stored Field value 
        /// </param>
        /// <returns>
        /// a Field value that shall be returned 
        /// </returns>
        public virtual string Get(string value)
        {
            return value;
        }

        /// <summary>
        /// Transforms a Field value while getting
        /// </summary>
        /// <param name="value">
        /// a Field value user is trying to set 
        /// </param>
        /// <returns>
        /// actual adjusted Field value that will be set 
        /// </returns>
        public virtual string Set(string value)
        {
            return value;
        }

        #endregion
    }
}