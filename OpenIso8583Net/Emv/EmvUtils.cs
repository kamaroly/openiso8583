namespace OpenIso8583Net.Emv
{
    using System;
    using System.IO;

    /// <summary>
    ///   EMV Utilities
    /// </summary>
    public static class EmvUtils
    {
        #region Public Methods and Operators

        /// <summary>
        /// Pack an <see cref="EmvTags"/> object into a byte array
        /// </summary>
        /// <param name="tags">
        /// The tags 
        /// </param>
        /// <returns>
        /// The packed byte array 
        /// </returns>
        public static byte[] Pack(this EmvTags tags)
        {
            var stream = new MemoryStream();

            foreach (var kvp in tags)
            {
                var field = PackField(kvp.Key, kvp.Value);
                stream.Write(field, 0, field.Length);
            }

            return stream.ToArray();
        }

        /// <summary>
        /// Unpack a byte array into an <see cref="EmvTags"/> object
        /// </summary>
        /// <param name="msg">
        /// The data 
        /// </param>
        /// <returns>
        /// The <see cref="EmvTags"/> object 
        /// </returns>
        public static EmvTags UnpackEmvTags(byte[] msg)
        {
            var emvTags = new EmvTags();
            var stream = new MemoryStream(msg);
            while (stream.Position < stream.Length)
            {
                var tagValue = stream.ReadByte();

                // Extended tag
                if ((tagValue & 0x1F) == 0x1F)
                {
                    tagValue <<= 8;
                    tagValue += stream.ReadByte();
                }

                var tag = (Tag)tagValue;
                var length = stream.ReadByte();
                if (length > 127)
                {
                    int lengthOfLength = length - 128;
                    stream.Seek(lengthOfLength, SeekOrigin.Current);
                    length = stream.ReadByte();
                }

                byte[] data = null;

                if (length > 0)
                {
                    data = new byte[length];
                    stream.Read(data, 0, length);
                }

                emvTags.Add(tag, data);
            }

            return emvTags;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Pack an individual field into a byte array
        /// </summary>
        /// <param name="tag">
        /// The tag. 
        /// </param>
        /// <param name="data">
        /// The data. 
        /// </param>
        /// <returns>
        /// The packed field 
        /// </returns>
        private static byte[] PackField(Tag tag, byte[] data)
        {
            var dataLength = data == null ? 0 : data.Length;
            var tagValue = (int)tag;
            var stream = new MemoryStream();

            int low = tagValue & 0xff;
            if (tagValue > 255)
            {
                int high = (tagValue & 0xff00) >> 8;
                stream.WriteByte((byte)high);
            }

            stream.WriteByte((byte)low);

            if (dataLength > 127)
            {
                var lengthOfLength = Math.Ceiling((double)dataLength / 256);
                stream.WriteByte((byte)(0x80 + lengthOfLength));
                if (dataLength > 255)
                {
                    stream.WriteByte((byte)(dataLength / 256));
                }
            }

            stream.WriteByte((byte)(dataLength % 256));

            if (data != null)
            {
                stream.Write(data, 0, data.Length);
            }

            return stream.ToArray();
        }

        #endregion
    }
}