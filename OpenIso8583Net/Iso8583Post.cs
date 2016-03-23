using OpenIso8583Net;
using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Postilion ISO 8583 Message
    /// </summary>
    /// <remarks>
    ///   This inherits from Iso8583 and adds a number of postilion specific fields.  In particular, field 127
    ///   the postilion private bitmap field has been added
    /// </remarks>
    /// <example>
    ///   <code>
    ///     public byte[] GetDataFromMessage()
    ///     {
    ///     Iso8583Post msg = new Iso8583Post();
    ///     msg[Iso8583Post.Bit._002_PAN] = "123456789012345";
    ///     msg.Private[Field127.Bit._002_SWITCH_KEY] = "SimKey00000001";
    ///     byte[] data = msg.ToMsg();
    ///     return data;
    ///     }
    /// 
    ///     public Iso8583Post GetMessageFromData(byte[] data)
    ///     {
    ///     Iso8583Post msg = new Iso8583Post();
    ///     msg.Unpack(data, 0);
    ///     return msg;
    ///     }
    ///   </code>
    ///   <code lang = "VB">
    ///     Public Function GetDataFromMessage() As Byte()
    ///     Dim msg As New Iso8583Post()
    ///     msg(Iso8583Post.Bit._002_PAN) = "123456789012345"
    ///     msg.[Private](Field127.Bit._002_SWITCH_KEY) = "SimKey00000001"
    ///     Dim data As Byte() = msg.ToMsg()
    ///     Return data
    ///     End Function
    /// 
    ///     Public Function GetMessageFromData(ByVal data As Byte()) As Iso8583Post
    ///     Dim msg As New Iso8583Post()
    ///     msg.Unpack(data, 0)
    ///     Return msg
    ///     End Function
    ///   </code>
    /// </example>
    public class Iso8583Post : Iso8583
    {
        /// <summary>
        ///   The default template.
        /// </summary>
        private static readonly Template DefaultTemplate;

        private readonly Field127 _field127;
        private HashtableMessage _structuredData;

        /// <summary>
        ///   Initializes static members of the <see cref="Iso8583" /> class.
        /// </summary>
        static Iso8583Post()
        {
            DefaultTemplate = GetDefaultIso8583PostTemplate();
        }

        /// <summary>
        ///   Creates a new Iso8583Post message
        /// </summary>
        public Iso8583Post() : base(DefaultTemplate)
        {
        }

        /// <summary>
        ///   The postilion private field, field 127
        /// </summary>
        public Field127 Private
        {
            get { return (Field127)GetField(127); }
        }

        /// <summary>
        ///   Gets fields 127.22
        /// </summary>
        public HashtableMessage StructuredData
        {
            get
            {
                if (_structuredData == null)
                {
                    _structuredData = new HashtableMessage();
                    _structuredData.FromMessageString(Private[22]);
                }
                return _structuredData;
            }
        }

        /// <summary>
        ///   Create a field of the correct type and length
        /// </summary>
        /// <param name = "field">Field number to create</param>
        /// <returns>AField representing the desired field</returns>
        protected override IField CreateField(int field)
        {
            // Deal with the postilion specific fields first
            switch (field)
            {
                case Bit._127_POSTILION_PRIVATE_FIELD:
                    return new Field127();
            }

            // Handle standard ISO fields later
            return base.CreateField(field);
        }

        /// <summary>
        ///   Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>byte[] representing the message</returns>
        public override byte[] ToMsg()
        {
            if (_structuredData != null)
                Private[22] = _structuredData.ToMessageString();
            return base.ToMsg();
        }

        /// <summary>
        ///   Converts the message to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_structuredData != null)
                Private[PrivBit._022_STRUCTURED_DATA] = _structuredData.ToMessageString();
            return base.ToString();
        }

        /// <summary>
        /// Get the default Is8583-Post template
        /// </summary>
        /// <returns>
        /// A Template
        /// </returns>
        protected static Template GetDefaultIso8583PostTemplate()
        {
            var template = GetDefaultIso8583Template();

            template[Bit._037_RETRIEVAL_REF_NUM] = FieldDescriptor.AsciiFixed(12, FieldValidators.Anp);
            template[Bit._038_AUTH_ID_RESPONSE] = FieldDescriptor.AsciiFixed(6, FieldValidators.Anp);
            template[Bit._059_ECHO_DATA] = FieldDescriptor.AsciiVar(3, 255, FieldValidators.AlphaNumericSpecial);
            template[Bit._123_POS_DATA_CODE] = FieldDescriptor.AsciiVar(3, 15, FieldValidators.AlphaNumeric);

            return template;
        }

        #region Nested type: Bit

        /// <summary>
        ///   Human readable constants mapping to field numbers
        /// </summary>
        public new class Bit : Iso8583.Bit
        {
            /// <summary>
            ///   Field 59 - Echo Data
            /// </summary>
            public const int _059_ECHO_DATA = 59;

            /// <summary>
            ///   POS Data Code
            /// </summary>
            public const int _123_POS_DATA_CODE = 123;

            /// <summary>
            ///   Postilion private field
            /// </summary>
            public const int _127_POSTILION_PRIVATE_FIELD = 127;
        }

        #endregion

        #region Nested type: PrivBit

        ///<summary>
        ///  List Postilion's Private fields
        ///</summary>
        public static class PrivBit
        {
            /// <summary>
            ///   Switch Key
            /// </summary>
            public const int _002_SWITCH_KEY = 2;

            /// <summary>
            ///   Original switch key
            /// </summary>
            public const int _011_ORIGINAL_KEY = 11;

            /// <summary>
            ///   Structured data
            /// </summary>
            public const int _022_STRUCTURED_DATA = 22;

            /// <summary>
            ///   Extended transaction type
            /// </summary>
            public const int _033_EXT_TRAN_TYPE = 33;
        }

        #endregion
    }
}