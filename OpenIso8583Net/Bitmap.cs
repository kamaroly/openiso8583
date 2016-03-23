// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bitmap.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Class representing a bitmap in an ISO 8583 message
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net
{
    using System;

    using OpenIso8583Net.Formatter;

    /// <summary>
    /// Class representing a bitmap in an ISO 8583 message
    /// </summary>
    public class Bitmap
    {
        #region Constants and Fields

        /// <summary>
        ///   The _bits.
        /// </summary>
        private readonly bool[] bits;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Bitmap" /> class. Create a new instance of the Bitmap class
        /// </summary>
        public Bitmap()
            : this(new BinaryFormatter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bitmap"/> class. Create a new instance of the Bitmap class
        /// </summary>
        /// <param name="formatter">
        /// The formatter to use 
        /// </param>
        public Bitmap(IFormatter formatter)
        {
            Formatter = formatter;
            this.bits = new bool[128];
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets a value indicating whether or not the extended bitmap is set
        /// </summary>
        public bool IsExtendedBitmap
        {
            get
            {
                return this.IsFieldSet(1);
            }
        }

        public IFormatter Formatter { get; set; }

        /// <summary>
        ///   Gets the packed length of the message
        /// </summary>
        public int PackedLength
        {
            get
            {
                return this.Formatter.GetPackedLength(this.IsExtendedBitmap ? 32 : 16);
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        ///   Gets or sets the presence of a field in the bitmap
        /// </summary>
        /// <param name="field"> Field in question </param>
        /// <returns> true if set, false otherwise </returns>
        public bool this[int field]
        {
            get
            {
                return this.IsFieldSet(field);
            }

            set
            {
                this.SetField(field, value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets if a field is set
        /// </summary>
        /// <param name="field">
        /// Field to query 
        /// </param>
        /// <returns>
        /// true if set, false otherwise 
        /// </returns>
        public bool IsFieldSet(int field)
        {
            return this.bits[field - 1];
        }

        /// <summary>
        /// Sets a field
        /// </summary>
        /// <param name="field">
        /// Field to set 
        /// </param>
        /// <param name="on">
        /// Whether or not the field is on 
        /// </param>
        public void SetField(int field, bool on)
        {
            this.bits[field - 1] = on;
            this.bits[0] = false;
            for (var i = 64; i <= 127; i++)
            {
                if (this.bits[i])
                {
                    this.bits[0] = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>
        /// byte[] representing the message 
        /// </returns>
        public virtual byte[] ToMsg()
        {
            var lengthOfBitmap = this.IsExtendedBitmap ? 16 : 8;
            var data = new byte[lengthOfBitmap];

            for (var i = 0; i < lengthOfBitmap; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (this.bits[i * 8 + j])
                    {
                        data[i] = (byte)(data[i] | (128 / (int)Math.Pow(2, j)));
                    }
                }
            }

            if (this.Formatter is BinaryFormatter)
            {
                return data;
            }

            IFormatter binaryFormatter = new BinaryFormatter();
            var bitmapString = binaryFormatter.GetString(data);

            return this.Formatter.GetBytes(bitmapString);
        }

        /// <summary>
        /// Unpacks the bitmap from the message
        /// </summary>
        /// <param name="msg">
        /// byte[] of the full message 
        /// </param>
        /// <param name="offset">
        /// offset indicating the start of the bitmap 
        /// </param>
        /// <returns>
        /// new offset to start unpacking the first field 
        /// </returns>
        public int Unpack(byte[] msg, int offset)
        {
            // This is a horribly nasty way of doing the bitmaps, but it works
            // I think...
            var lengthOfBitmap = this.Formatter.GetPackedLength(16);
            if (this.Formatter is BinaryFormatter)
            {
                if (msg[offset] >= 128)
                {
                    lengthOfBitmap += 8;
                }
            }
            else
            {
                if (msg[offset] >= 0x38)
                {
                    lengthOfBitmap += 16;
                }
            }

            var bitmapData = new byte[lengthOfBitmap];
            Array.Copy(msg, offset, bitmapData, 0, lengthOfBitmap);

            if (!(this.Formatter is BinaryFormatter))
            {
                IFormatter binaryFormatter = new BinaryFormatter();
                var value = this.Formatter.GetString(bitmapData);
                bitmapData = binaryFormatter.GetBytes(value);
            }

            // good luck understanding this
            for (var i = 0; i < bitmapData.Length; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    this.bits[i * 8 + j] = (bitmapData[i] & (128 / (int)Math.Pow(2, j))) > 0;
                }
            }

            return offset + lengthOfBitmap;
        }

        #endregion
    }
}