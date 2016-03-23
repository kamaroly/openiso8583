// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Template.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   A Template describing a message
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net
{
    using System.Collections.Generic;
    using System.Text;

    using OpenIso8583Net.Formatter;

    /// <summary>
    /// A Template describing a message
    /// </summary>
    public class Template : Dictionary<int, IFieldDescriptor>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Template"/> class. 
        /// </summary>
        public Template()
        {
            this.MsgTypeFormatter = Formatters.Ascii;
            this.BitmapFormatter = Formatters.Binary;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the message type formatter
        /// </summary>
        public IFormatter MsgTypeFormatter { get; set; }

        /// <summary>
        /// Gets or sets the bitmap formatter
        /// </summary>
        public IFormatter BitmapFormatter { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Describe the packing format of the template
        /// </summary>
        /// <returns>
        /// The packing of the template 
        /// </returns>
        public string DescribePacking()
        {
            var sb = new StringBuilder();

            foreach (var kvp in this)
            {
                var field = kvp.Key;
                var descriptor = kvp.Value;
                sb.AppendLine(descriptor.Display(string.Empty, field, null));
            }

            return sb.ToString();
        }

        #endregion
    }
}