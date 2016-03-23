// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AMessage.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Class representing a generic ISO 8583 message
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class representing a generic ISO 8583 message
    /// </summary>
    /// <remarks>
    /// This class has been designed to be overridden and apply to all sorts of Bitmap messages. As such it does not create any fields itself (rev 87 and rev 93) nor does it have an MTID in it so you can use it if you need a sub message as a field. See <see cref="Iso8583"/> for an implementation of it
    /// </remarks>
    public abstract class AMessage : IMessage
    {
        #region Constants and Fields

        /// <summary>
        ///   Bitmap for the ISO message
        /// </summary>
        protected readonly Bitmap bitmap;

        /// <summary>
        ///   Dictionary containing all the fields in the message
        /// </summary>
        protected readonly Dictionary<int, IField> fields;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AMessage"/> class.
        /// </summary>
        /// <param name="template">
        /// The template. 
        /// </param>
        protected AMessage(Template template)
        {
            Template = template;
            this.fields = new Dictionary<int, IField>();
            this.bitmap = new Bitmap(template.BitmapFormatter);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the packed length of the message
        /// </summary>
        public int PackedLength
        {
            get
            {
                var length = this.bitmap.PackedLength;
                for (var i = 2; i <= 128; i++)
                {
                    if (this.bitmap[i])
                    {
                        length += this.fields[i].PackedLength;
                    }
                }

                return length;
            }
        }

        /// <summary>
        ///   Gets the processing code (field 3) of the message
        /// </summary>
        public ProcessingCode ProcessingCode
        {
            get
            {
                return new ProcessingCode(this[3]);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///  Gets a template describing the ISO message
        /// </summary>
        protected Template Template { get; set; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///   Gets or sets the value of a field
        /// </summary>
        /// <param name="field"> Field number to get or set </param>
        /// <returns> Value of the field or null if not present </returns>
        public string this[int field]
        {
            get
            {
                return this.GetFieldValue(field);
            }

            set
            {
                this.SetFieldValue(field, value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clears the field in the message
        /// </summary>
        /// <param name="field">
        /// Field number to clear 
        /// </param>
        public void ClearField(int field)
        {
            this.bitmap[field] = false;
            this.fields.Remove(field);
        }

        /// <summary>
        /// Describe the packing of the message
        /// </summary>
        /// <returns>
        /// the packing of the message 
        /// </returns>
        public virtual string DescribePacking()
        {
            return Template.DescribePacking();
        }

        /// <summary>
        /// Returns if a field is set
        /// </summary>
        /// <param name="field">
        /// Field number to retrieve 
        /// </param>
        /// <returns>
        /// true if set, false otherwise 
        /// </returns>
        public bool IsFieldSet(int field)
        {
            return this.bitmap[field];
        }

        /// <summary>
        /// Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>
        /// byte[] representing the message 
        /// </returns>
        public virtual byte[] ToMsg()
        {
            var packedLen = this.PackedLength;
            var data = new byte[packedLen];

            var offset = 0;

            // bitmap
            var bmap = this.bitmap.ToMsg();
            Array.Copy(bmap, 0, data, offset, this.bitmap.PackedLength);
            offset += this.bitmap.PackedLength;

            // Fields
            for (var i = 2; i <= 128; i++)
            {
                if (this.bitmap[i])
                {
                    var field = this.fields[i];
                    Array.Copy(field.ToMsg(), 0, data, offset, field.PackedLength);
                    offset += field.PackedLength;
                }
            }

            return data;
        }

        /// <summary>
        /// Returns the contents of the message as a string
        /// </summary>
        /// <returns>
        /// Pretty printed string 
        /// </returns>
        public override string ToString()
        {
            return ToString(string.Empty);
        }

        /// <summary>
        /// Returns the contents of the message as a string
        /// </summary>
        /// <param name="prefix">
        /// The prefix to apply to each line in the message 
        /// </param>
        /// <returns>
        /// Pretty printed string 
        /// </returns>
        public virtual string ToString(string prefix)
        {
            var sb = new StringBuilder();

            for (var i = 2; i <= 128; i++)
            {
                if (this.bitmap[i])
                {
                    sb.AppendLine(ToString(i, prefix));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the field contents as a string for displaying in traces and the like
        /// </summary>
        /// <param name="field">
        /// Field number 
        /// </param>
        /// <param name="prefix">
        /// What each line must prepended with the prefix 
        /// </param>
        /// <returns>
        /// Value of the field nicely formatted 
        /// </returns>
        public virtual string ToString(int field, string prefix)
        {
            return this.fields[field].ToString(prefix + "   ");
        }

        /// <summary>
        /// Unpacks the message from a byte array
        /// </summary>
        /// <param name="msg">
        /// message data to unpack 
        /// </param>
        /// <param name="startingOffset">
        /// the offset in the array to start 
        /// </param>
        /// <returns>
        /// the offset in the array representing the start of the next message 
        /// </returns>
        public virtual int Unpack(byte[] msg, int startingOffset)
        {
            var offset = startingOffset;

            // get bitmap
            offset = this.bitmap.Unpack(msg, offset);

            // get fields
            for (var i = 2; i <= 128; i++)
            {
                if (this.bitmap[i])
                {
                    offset = this.GetField(i).Unpack(msg, offset);
                }
            }

            return offset;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a field of the correct type and length
        /// </summary>
        /// <param name="field">
        /// Field number to create 
        /// </param>
        /// <returns>
        /// IField representing the desired field 
        /// </returns>
        protected abstract IField CreateField(int field);

        /// <summary>
        /// Gets a field to work on. Creates the field if it does not exist
        /// </summary>
        /// <param name="field">
        /// Field number to get 
        /// </param>
        /// <returns>
        /// Field to work on 
        /// </returns>
        protected IField GetField(int field)
        {
            if (!this.bitmap[field] || !this.fields.ContainsKey(field))
            {
                this.fields.Add(field, this.CreateField(field));
                this.bitmap[field] = true;
            }

            return this.fields[field];
        }

        /// <summary>
        /// Get a field from the ISO message
        /// </summary>
        /// <param name="field">
        /// Field to retrieve 
        /// </param>
        /// <returns>
        /// Value of the field or null if not present 
        /// </returns>
        protected string GetFieldValue(int field)
        {
            return this.bitmap[field] ? this.fields[field].Value : null;
        }

        /// <summary>
        /// Sets a field with the given value in the ISO message.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <remarks>
        /// Don't worry about creating the IField as this method will do so
        /// </remarks>
        protected void SetFieldValue(int field, string value)
        {
            if (value == null)
            {
                this.ClearField(field);
                return;
            }

            this.GetField(field).Value = value;
        }

        #endregion

        /// <summary>
        /// Account Types
        /// </summary>
        public static class AccountType
        {
            #region Constants and Fields

            /// <summary>
            ///   Default
            /// </summary>
            public const string _00_DEFAULT = "00";

            /// <summary>
            ///   Savings
            /// </summary>
            public const string _10_SAVINGS = "10";

            /// <summary>
            ///   Cheque/Check
            /// </summary>
            public const string _20_CHECK = "20";

            /// <summary>
            ///   Credit
            /// </summary>
            public const string _30_CREDIT = "30";

            /// <summary>
            ///   Universal
            /// </summary>
            public const string _40_UNIVERSAL = "40";

            /// <summary>
            ///   Investment
            /// </summary>
            public const string _50_INVESTMENT = "50";

            /// <summary>
            ///   Electronic purse default
            /// </summary>
            public const string _60_ELECTRONIC_PURSE_DEFAULT = "60";

            #endregion
        }

        /// <summary>
        /// Amount Types
        /// </summary>
        public static class AmountType
        {
            #region Constants and Fields

            /// <summary>
            ///   Ledger Balance
            /// </summary>
            public const string _01_LEDGER_BALANCE = "01";

            /// <summary>
            ///   Available Balance
            /// </summary>
            public const string _02_AVAILABLE_BALANCE = "02";

            /// <summary>
            ///   Owing
            /// </summary>
            public const string _03_OWING = "03";

            /// <summary>
            ///   Due
            /// </summary>
            public const string _04_DUE = "04";

            /// <summary>
            ///   Remaining this cycle
            /// </summary>
            public const string _20_REMAINING_THIS_CYCLE = "20";

            /// <summary>
            ///   Cash
            /// </summary>
            public const string _40_CASH = "40";

            /// <summary>
            ///   Goods and Services
            /// </summary>
            public const string _41_GOODS_SERVICES = "41";

            /// <summary>
            ///   Approved
            /// </summary>
            public const string _53_APPROVED = "53";

            /// <summary>
            ///   Tip
            /// </summary>
            public const string _56_TIP = "56";

            /// <summary>
            ///   Available Credit
            /// </summary>
            public const string _90_AVAILABLE_CREDIT = "90";

            /// <summary>
            ///   Credit Limit
            /// </summary>
            public const string _91_CREDIT_LIMIT = "91";

            #endregion
        }
    }
}