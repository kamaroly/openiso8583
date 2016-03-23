namespace OpenIso8583Net.Emv
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///   Class representing a collection of EMV Tags
    /// </summary>
    public class EmvTags : Dictionary<Tag, byte[]>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Add a BCD value tag to the collection
        /// </summary>
        /// <param name="tag">
        /// The tag 
        /// </param>
        /// <param name="value">
        /// The value 
        /// </param>
        public void AddBcd(Tag tag, string value)
        {
            int paddedLength = value.Length % 2 == 0 ? value.Length : value.Length + 1;
            this.Add(tag, Utils.ToByteArray(value.PadLeft(paddedLength, '0')));
        }

        /// <summary>
        /// ASCII encode a string value and add it to the tags collection
        /// </summary>
        /// <param name="tag">
        /// Tag to add 
        /// </param>
        /// <param name="value">
        /// String value 
        /// </param>
        public void AddString(Tag tag, string value)
        {
            this[tag] = Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Get a BCD value from the collection
        /// </summary>
        /// <param name="tag">
        /// The tag 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> value 
        /// </returns>
        public string GetBcd(Tag tag)
        {
            return Utils.ToHex(this[tag]);
        }

        /// <summary>
        /// Get an ASCII string value from the collection
        /// </summary>
        /// <param name="tag">
        /// The tag 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> value 
        /// </returns>
        public string GetString(Tag tag)
        {
            return Encoding.ASCII.GetString(this[tag]);
        }

        /// <summary>
        /// Convert EmvTags to a string
        /// </summary>
        /// <param name="indent">
        /// Indent to prepend to each line 
        /// </param>
        /// <returns>
        /// A string representing the tags 
        /// </returns>
        public string ToString(string indent)
        {
            var sb = new StringBuilder();
            foreach (var kvp in this)
            {
                sb.Append(indent);
                var hexTag = "0x" + Convert.ToString((int)kvp.Key, 16);
                sb.Append(hexTag.PadRight(7, ' '));
                sb.Append('\'');
                sb.Append(kvp.Key.ToString().PadRight(20));
                sb.Append("' = [");
                if (kvp.Value != null)
                {
                    sb.Append(GetTagMasked(kvp.Key, Utils.ToHex(kvp.Value)));
                }

                sb.Append("]");
                sb.Append(Environment.NewLine);
            }

            var str = sb.ToString();

            return str.Substring(0, str.Length - Environment.NewLine.Length);
        }

        /// <summary>
        ///   Convert EmvTags to a string
        /// </summary>
        /// <returns> A string representing the tags </returns>
        public override string ToString()
        {
            return this.ToString(string.Empty);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the tag value and masks if appropriate
        /// </summary>
        /// <param name="tag">
        /// The tag 
        /// </param>
        /// <param name="value">
        /// The value 
        /// </param>
        /// <returns>
        /// Masked value 
        /// </returns>
        private static string GetTagMasked(Tag tag, string value)
        {
            switch (tag)
            {
                case Tag.appl_pan:
                case Tag.track2_eq_data:
                case Tag.track1_disc_data:
                    return "*".PadLeft(value.Length, '*');
            }

            return value;
        }

        #endregion
    }
}