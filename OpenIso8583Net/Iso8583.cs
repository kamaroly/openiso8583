// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Iso8583.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Generic Implmentable ISO 8583 Revision 87 class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OpenIso8583Net.Exceptions;
    using OpenIso8583Net.FieldValidator;

    /// <summary>
    /// Generic Implmentable ISO 8583 Revision 87 class
    /// </summary>
    public class Iso8583 : AMessage
    {
        #region Constants and Fields

        /// <summary>
        ///   The default template.
        /// </summary>
        private static readonly Template DefaultTemplate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="Iso8583" /> class.
        /// </summary>
        static Iso8583()
        {
            DefaultTemplate = GetDefaultIso8583Template();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Iso8583" /> class. Creates a new instance of the Iso8583 class
        /// </summary>
        public Iso8583()
            : this(DefaultTemplate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iso8583"/> class. Creates a new instance of the Iso8583 class using a specific message type formatter
        /// </summary>
        /// <param name="template">
        /// The template overrides to use for the ISO message 
        /// </param>
        public Iso8583(Template template)
            : base(template)
        {
            this.MessageType = MsgType._0000_INVALID_MSG;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets a list of additional amounts for this ISO message
        /// </summary>
        public IEnumerable<AdditionalAmount> AdditionalAmounts
        {
            get
            {
                if (!IsFieldSet(54))
                {
                    return null;
                }

                var addAmounts = new List<AdditionalAmount>();
                var amounts = GetField(54).Value;

                if (amounts.Length % 20 != 0)
                {
                    throw new Exception("AdditionalAmounts incorrect length");
                }

                for (var i = 0; i < amounts.Length; i += 20)
                {
                    var amount = amounts.Substring(i, 20);
                    addAmounts.Add(new AdditionalAmount(amount));
                }

                return addAmounts;
            }
        }

        /// <summary>
        ///   Gets or sets the message type
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        ///   Gets the packed length of the message
        /// </summary>
        public new int PackedLength
        {
            get
            {
                return base.PackedLength + IsoConvert.FromIntToMsgTypeData(this.MessageType).Length;
            }
        }

        /// <summary>
        ///   Gets or sets the transaction amount for the message
        /// </summary>
        public long TransactionAmount
        {
            get
            {
                return long.Parse(this[4]);
            }

            set
            {
                this[4] = value.ToString().PadLeft(12, '0');
            }
        }

        /// <summary>
        ///   Gets the transmission date time
        /// </summary>
        public TransmissionDateTime TransmissionDateTime
        {
            get
            {
                return new TransmissionDateTime(this);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>
        /// byte[] representing the message 
        /// </returns>
        public override byte[] ToMsg()
        {
            var length = this.PackedLength;
            var data = new byte[length];

            var msgTypeString = IsoConvert.FromIntToMsgType(this.MessageType);

            // MsgType
            var newMsgType = Template.MsgTypeFormatter.GetBytes(msgTypeString);
            var msgTypeLen = newMsgType.Length;

            // Array.Copy(IsoConvert.FromIntToMsgTypeData(MessageType), 0, data, 0, 4);
            Array.Copy(newMsgType, 0, data, 0, newMsgType.Length);
            var baseMsg = base.ToMsg();
            Array.Copy(baseMsg, 0, data, msgTypeLen, baseMsg.Length);
            return data;
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
        public override string ToString(string prefix)
        {
            var sb = new StringBuilder();
            sb.Append(prefix + IsoConvert.FromIntToMsgType(this.MessageType) + ":" + Environment.NewLine);
            sb.Append(base.ToString(prefix));
            return sb.ToString();
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
        public override int Unpack(byte[] msg, int startingOffset)
        {
            // get mtid
            var mtidLength = this.Template.MsgTypeFormatter.GetPackedLength(4);
            var buffer = new byte[mtidLength];
            var offset = startingOffset;
            Array.Copy(msg, offset, buffer, 0, mtidLength);
            string msgTypeString = this.Template.MsgTypeFormatter.GetString(buffer);
            this.MessageType = IsoConvert.FromMsgTypeToInt(msgTypeString);
            offset += mtidLength;
            return base.Unpack(msg, offset);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get default iso 8583 template.
        /// </summary>
        /// <returns>
        /// A Template
        /// </returns>
        protected static Template GetDefaultIso8583Template()
        {
            var template = new Template
                {
                    { Bit._002_PAN, FieldDescriptor.AsciiVar(2, 19, FieldValidators.N) },
                    { Bit._003_PROC_CODE, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._004_TRAN_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._005_SETTLE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._007_TRAN_DATE_TIME, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._009_CONVERSION_RATE_SETTLEMENT, FieldDescriptor.AsciiFixed(8, FieldValidators.N) },
                    { Bit._011_SYS_TRACE_AUDIT_NUM, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._012_LOCAL_TRAN_TIME, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._013_LOCAL_TRAN_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._014_EXPIRATION_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._015_SELLTLEMENT_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._016_CONVERSION_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._018_MERCHANT_TYPE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._022_POS_ENTRY_MODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._023_CARD_SEQUENCE_NUM, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._025_POS_CONDITION_CODE, FieldDescriptor.AsciiFixed(2, FieldValidators.N) },
                    { Bit._026_POS_PIN_CAPTURE_CODE, FieldDescriptor.AsciiFixed(2, FieldValidators.N) },
                    { Bit._027_AUTH_ID_RSP, FieldDescriptor.AsciiFixed(1, FieldValidators.N) },
                    { Bit._028_TRAN_FEE_AMOUNT, FieldDescriptor.AsciiFixed(9, FieldValidators.Rev87AmountValidator) },
                    {
                        Bit._029_SETTLEMENT_FEE_AMOUNT, FieldDescriptor.AsciiFixed(9, FieldValidators.Rev87AmountValidator)
                        },
                    { Bit._030_TRAN_PROC_FEE_AMOUNT, FieldDescriptor.AsciiFixed(9, FieldValidators.Rev87AmountValidator) },
                    {
                        Bit._031_SETTLEMENT_PROC_FEE_AMOUNT,
                        FieldDescriptor.AsciiFixed(9, FieldValidators.Rev87AmountValidator)
                        },
                    { Bit._032_ACQUIRING_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._033_FORWARDING_INT_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._035_TRACK_2_DATA, FieldDescriptor.AsciiVar(2, 37, FieldValidators.Track2) },
                    { Bit._037_RETRIEVAL_REF_NUM, FieldDescriptor.AsciiFixed(12, FieldValidators.An) },
                    { Bit._038_AUTH_ID_RESPONSE, FieldDescriptor.AsciiFixed(6, FieldValidators.An) },
                    { Bit._039_RESPONSE_CODE, FieldDescriptor.AsciiFixed(2, FieldValidators.An) },
                    { Bit._040_SERVICE_RESTRICTION_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._041_CARD_ACCEPTOR_TERMINAL_ID, FieldDescriptor.AsciiFixed(8, FieldValidators.Ans) },
                    { Bit._042_CARD_ACCEPTOR_ID_CODE, FieldDescriptor.AsciiFixed(15, FieldValidators.Ans) },
                    { Bit._043_CARD_ACCEPTOR_NAME_LOCATION, FieldDescriptor.AsciiFixed(40, FieldValidators.Ans) },
                    { Bit._044_ADDITIONAL_RESPONSE_DATA, FieldDescriptor.AsciiVar(2, 25, FieldValidators.Ans) },
                    { Bit._045_TRACK_1_DATA, FieldDescriptor.AsciiVar(2, 76, FieldValidators.Ans) },
                    { Bit._048_ADDITIONAL_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans) },
                    { Bit._049_TRAN_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.AorN) },
                    { Bit._050_SETTLEMENT_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.AorN) },
                    { Bit._052_PIN_DATA, FieldDescriptor.BinaryFixed(8) },
                    { Bit._053_SECURITY_RELATED_CONTROL_INFORMATION, FieldDescriptor.BinaryFixed(48) },
                    { Bit._054_ADDITIONAL_AMOUNTS, FieldDescriptor.AsciiVar(3, 120, FieldValidators.An) },
                    { Bit._056_MESSAGE_REASON_CODE, FieldDescriptor.AsciiVar(3, 4, FieldValidators.N) },
                    { Bit._057_AUTHORISATION_LIFE_CYCLE, FieldDescriptor.AsciiVar(3, 3, FieldValidators.N) },
                    { Bit._058_AUTHORISING_AGENT_INSTITUTION, FieldDescriptor.AsciiVar(3, 11, FieldValidators.Anp) },
                    { Bit._066_SETTLEMENT_CODE, FieldDescriptor.AsciiFixed(1, FieldValidators.N) },
                    { Bit._067_EXTENDED_PAYMENT_CODE, FieldDescriptor.AsciiFixed(2, FieldValidators.N) },
                    { Bit._070_NETWORK_MANAGEMENT_INFORMATION_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._073_DATE_ACTION, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._074_CREDITS_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._075_CREDITS_REVERSAL_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._076_DEBITS_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._077_DEBITS_REVERSAL_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._078_TRANSFER_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._079_TRANSFER_REVERSAL_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._080_INQUIRIES_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._081_AUTHORISATIONS_NUMBER, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._082_CREDITS_PROCESSING_FEE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._083_CREDITS_TRANSACTION_FEE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._084_DEBITS_PROCESSING_FEE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._085_DEBITS_TRANSACTION_FEE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._086_CREDITS_AMOUNT, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._087_CREDITS_REVERSAL_AMOUNT, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._088_DEBITS_AMOUNT, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._089_DEBITS_REVERSAL_AMOUNT, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._090_ORIGINAL_DATA_ELEMENTS, FieldDescriptor.AsciiFixed(42, FieldValidators.N) },
                    { Bit._091_FILE_UPDATE_CODE, FieldDescriptor.AsciiFixed(1, FieldValidators.An) },
                    { Bit._095_REPLACEMENT_AMOUNTS, FieldDescriptor.AsciiFixed(42, FieldValidators.Ans) },
                    {
                        Bit._097_AMOUNT_NET_SETTLEMENT, FieldDescriptor.AsciiFixed(17, FieldValidators.Rev87AmountValidator)
                        },
                    { Bit._098_PAYEE, FieldDescriptor.AsciiFixed(25, FieldValidators.Ans) },
                    { Bit._100_RECEIVING_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._101_FILE_NAME, FieldDescriptor.AsciiVar(2, 17, FieldValidators.Ans) },
                    { Bit._102_ACCOUNT_ID_1, FieldDescriptor.AsciiVar(2, 28, FieldValidators.Ans) },
                    { Bit._103_ACCOUNT_ID_2, FieldDescriptor.AsciiVar(2, 28, FieldValidators.Ans) },
                    { Bit._118_PAYMENTS_NUMBER, FieldDescriptor.AsciiVar(3, 30, FieldValidators.N) },
                    { Bit._119_PAYMENTS_REVERSAL_NUMBER, FieldDescriptor.AsciiVar(3, 10, FieldValidators.N) },
                };

            return template;
        }

        /// <summary>
        /// Create a field of the correct type and length
        /// </summary>
        /// <param name="field">
        /// Field number to create 
        /// </param>
        /// <returns>
        /// IField representing the desired field 
        /// </returns>
        protected override IField CreateField(int field)
        {
            if (Template.ContainsKey(field))
            {
                return new Field(field, Template[field]);
            }

            throw new UnknownFieldException(field.ToString());
        }

        #endregion

        /// <summary>
        /// Human readable constants mapping to field numbers
        /// </summary>
        public class Bit
        {
            #region Constants and Fields

            /// <summary>
            ///   Primary Account Number
            /// </summary>
            public const int _002_PAN = 2;

            /// <summary>
            ///   Processing Code
            /// </summary>
            public const int _003_PROC_CODE = 3;

            /// <summary>
            ///   Transaction Amount
            /// </summary>
            public const int _004_TRAN_AMOUNT = 4;

            /// <summary>
            ///   Settlement Amount
            /// </summary>
            public const int _005_SETTLE_AMOUNT = 5;

            /// <summary>
            ///   Transmission Date and Time
            /// </summary>
            public const int _007_TRAN_DATE_TIME = 7;

            /// <summary>
            ///   Conversion Rate Settlement
            /// </summary>
            public const int _009_CONVERSION_RATE_SETTLEMENT = 9;

            /// <summary>
            ///   Systems Trace Audit Number
            /// </summary>
            public const int _011_SYS_TRACE_AUDIT_NUM = 11;

            /// <summary>
            ///   Field 12 - Time, Local Transaction
            /// </summary>
            public const int _012_LOCAL_TRAN_TIME = 12;

            /// <summary>
            ///   Field 13 - Date, Local Transaction
            /// </summary>
            public const int _013_LOCAL_TRAN_DATE = 13;

            /// <summary>
            ///   Field 14 - Date, Expiration
            /// </summary>
            public const int _014_EXPIRATION_DATE = 14;

            /// <summary>
            ///   Field 15 - Date, Settlement
            /// </summary>
            public const int _015_SELLTLEMENT_DATE = 15;

            /// <summary>
            ///   Field 16 - Date, Conversion
            /// </summary>
            public const int _016_CONVERSION_DATE = 16;

            /// <summary>
            ///   Field 18 - Merchant Type
            /// </summary>
            public const int _018_MERCHANT_TYPE = 18;

            /// <summary>
            ///   Field 22 - POS Entry Mode
            /// </summary>
            public const int _022_POS_ENTRY_MODE = 22;

            /// <summary>
            ///   Field 23 – Card Sequence Number
            /// </summary>
            public const int _023_CARD_SEQUENCE_NUM = 23;

            /// <summary>
            ///   Field 25 - POS Condition Code
            /// </summary>
            public const int _025_POS_CONDITION_CODE = 25;

            /// <summary>
            ///   Field 26 - POS PIN Capture Code
            /// </summary>
            public const int _026_POS_PIN_CAPTURE_CODE = 26;

            /// <summary>
            ///   Authorisation ID Response
            /// </summary>
            public const int _027_AUTH_ID_RSP = 27;

            /// <summary>
            ///   Transaction fee amount
            /// </summary>
            public const int _028_TRAN_FEE_AMOUNT = 28;

            /// <summary>
            ///   Settlement fee amount
            /// </summary>
            public const int _029_SETTLEMENT_FEE_AMOUNT = 29;

            /// <summary>
            ///   Transaction processing fee amount
            /// </summary>
            public const int _030_TRAN_PROC_FEE_AMOUNT = 30;

            /// <summary>
            ///   Settlement processing fee amount
            /// </summary>
            public const int _031_SETTLEMENT_PROC_FEE_AMOUNT = 31;

            /// <summary>
            ///   Field 32 - Acquiring Institution ID Code
            /// </summary>
            public const int _032_ACQUIRING_INST_ID_CODE = 32;

            /// <summary>
            ///   Field 33 - Forwarding Institution ID Code
            /// </summary>
            public const int _033_FORWARDING_INT_ID_CODE = 33;

            /// <summary>
            ///   Field 35 - Track 2 Data
            /// </summary>
            public const int _035_TRACK_2_DATA = 35;

            /// <summary>
            ///   Field 37 - Retrieval Reference Number
            /// </summary>
            public const int _037_RETRIEVAL_REF_NUM = 37;

            /// <summary>
            ///   Field 38 - Authorization ID Response
            /// </summary>
            public const int _038_AUTH_ID_RESPONSE = 38;

            /// <summary>
            ///   Field 39 - Response Code
            /// </summary>
            public const int _039_RESPONSE_CODE = 39;

            /// <summary>
            ///   Field 40 - Service Restriction Code
            /// </summary>
            public const int _040_SERVICE_RESTRICTION_CODE = 40;

            /// <summary>
            ///   Field 41 - Card Acceptor Terminal ID
            /// </summary>
            public const int _041_CARD_ACCEPTOR_TERMINAL_ID = 41;

            /// <summary>
            ///   Field 42 - Card Acceptor ID Code
            /// </summary>
            public const int _042_CARD_ACCEPTOR_ID_CODE = 42;

            /// <summary>
            ///   Field 43 - Card Acceptor Name Location
            /// </summary>
            public const int _043_CARD_ACCEPTOR_NAME_LOCATION = 43;

            /// <summary>
            ///   Field 44 - Additional Response Data
            /// </summary>
            public const int _044_ADDITIONAL_RESPONSE_DATA = 44;

            /// <summary>
            ///   Track 1 Data
            /// </summary>
            public const int _045_TRACK_1_DATA = 45;

            /// <summary>
            ///   Field 48 - Additional Data
            /// </summary>
            public const int _048_ADDITIONAL_DATA = 48;

            /// <summary>
            ///   Field 49 - Currency Code, Transaction
            /// </summary>
            public const int _049_TRAN_CURRENCY_CODE = 49;

            /// <summary>
            ///   Field 50 - Currency Code, Settlement
            /// </summary>
            public const int _050_SETTLEMENT_CURRENCY_CODE = 50;

            /// <summary>
            ///   Field 52 - PIN Data
            /// </summary>
            public const int _052_PIN_DATA = 52;

            /// <summary>
            ///   Security Related Control Information
            /// </summary>
            public const int _053_SECURITY_RELATED_CONTROL_INFORMATION = 53;

            /// <summary>
            ///   Field 54 - Additional Amounts
            /// </summary>
            public const int _054_ADDITIONAL_AMOUNTS = 54;

            /// <summary>
            ///   Field 56 - Message Reason Code
            /// </summary>
            public const int _056_MESSAGE_REASON_CODE = 56;

            /// <summary>
            ///   Authorisation Life Cycle
            /// </summary>
            public const int _057_AUTHORISATION_LIFE_CYCLE = 57;

            /// <summary>
            ///   Authorising Agent Institution
            /// </summary>
            public const int _058_AUTHORISING_AGENT_INSTITUTION = 58;

            /// <summary>
            ///   Settlement Code
            /// </summary>
            public const int _066_SETTLEMENT_CODE = 66;

            /// <summary>
            ///   Extended Payment Code
            /// </summary>
            public const int _067_EXTENDED_PAYMENT_CODE = 67;

            /// <summary>
            ///   Network Management Information Code
            /// </summary>
            public const int _070_NETWORK_MANAGEMENT_INFORMATION_CODE = 70;

            /// <summary>
            ///   Date Action
            /// </summary>
            public const int _073_DATE_ACTION = 73;

            /// <summary>
            ///   Credits, Number
            /// </summary>
            public const int _074_CREDITS_NUMBER = 74;

            /// <summary>
            ///   Credits Reversal, Number
            /// </summary>
            public const int _075_CREDITS_REVERSAL_NUMBER = 75;

            /// <summary>
            ///   Debits, Number
            /// </summary>
            public const int _076_DEBITS_NUMBER = 76;

            /// <summary>
            ///   Debits Reversal, Number
            /// </summary>
            public const int _077_DEBITS_REVERSAL_NUMBER = 77;

            /// <summary>
            ///   Transfers, Number
            /// </summary>
            public const int _078_TRANSFER_NUMBER = 78;

            /// <summary>
            ///   Transfers Reversal, Number
            /// </summary>
            public const int _079_TRANSFER_REVERSAL_NUMBER = 79;

            /// <summary>
            ///   Inquiries, Number
            /// </summary>
            public const int _080_INQUIRIES_NUMBER = 80;

            /// <summary>
            ///   Authorisations, Number
            /// </summary>
            public const int _081_AUTHORISATIONS_NUMBER = 81;

            /// <summary>
            ///   Credits, Processing Fee Amount
            /// </summary>
            public const int _082_CREDITS_PROCESSING_FEE_AMOUNT = 82;

            /// <summary>
            ///   Credits, Transaction Fee Amount
            /// </summary>
            public const int _083_CREDITS_TRANSACTION_FEE_AMOUNT = 83;

            /// <summary>
            ///   Debits, Processing Fee Amount
            /// </summary>
            public const int _084_DEBITS_PROCESSING_FEE_AMOUNT = 84;

            /// <summary>
            ///   Debits, Transaction Fee Amount
            /// </summary>
            public const int _085_DEBITS_TRANSACTION_FEE_AMOUNT = 85;

            /// <summary>
            ///   Credits, Amount
            /// </summary>
            public const int _086_CREDITS_AMOUNT = 86;

            /// <summary>
            ///   Credits Reversal, Amount
            /// </summary>
            public const int _087_CREDITS_REVERSAL_AMOUNT = 87;

            /// <summary>
            ///   Debits, Amount
            /// </summary>
            public const int _088_DEBITS_AMOUNT = 88;

            /// <summary>
            ///   Debits Reversal, Amount
            /// </summary>
            public const int _089_DEBITS_REVERSAL_AMOUNT = 89;

            /// <summary>
            ///   Original Data Elements
            /// </summary>
            public const int _090_ORIGINAL_DATA_ELEMENTS = 90;

            /// <summary>
            ///   File Update Code
            /// </summary>
            public const int _091_FILE_UPDATE_CODE = 91;

            /// <summary>
            ///   Replacement Amounts
            /// </summary>
            public const int _095_REPLACEMENT_AMOUNTS = 95;

            /// <summary>
            ///   Amount Net Settlement
            /// </summary>
            public const int _097_AMOUNT_NET_SETTLEMENT = 97;

            /// <summary>
            ///   Payee
            /// </summary>
            public const int _098_PAYEE = 98;

            /// <summary>
            ///   Field 100 - Receiving Institution ID Code
            /// </summary>
            public const int _100_RECEIVING_INST_ID_CODE = 100;

            /// <summary>
            ///   Field 101 - File Name
            /// </summary>
            public const int _101_FILE_NAME = 101;

            /// <summary>
            ///   Field 102 - Account Identification 1
            /// </summary>
            public const int _102_ACCOUNT_ID_1 = 102;

            /// <summary>
            ///   Field 103 - Account Identification 2
            /// </summary>
            public const int _103_ACCOUNT_ID_2 = 103;

            /// <summary>
            ///   Payments, Number
            /// </summary>
            public const int _118_PAYMENTS_NUMBER = 118;

            /// <summary>
            ///   Payments Reversal, Number
            /// </summary>
            public const int _119_PAYMENTS_REVERSAL_NUMBER = 119;

            #endregion
        }

        /// <summary>
        /// Human readable constants mapping to message types
        /// </summary>
        public class MsgType
        {
            #region Constants and Fields

            /// <summary>
            ///   Invalid Message Type
            /// </summary>
            public const int _0000_INVALID_MSG = 0x000;

            /// <summary>
            ///   Auth Request
            /// </summary>
            public const int _0100_AUTH_REQ = 0x100;

            /// <summary>
            ///   Auth Response
            /// </summary>
            public const int _0110_AUTH_REQ_RSP = 0x110;

            /// <summary>
            ///   Auth Advice
            /// </summary>
            public const int _0120_AUTH_ADV = 0x120;

            /// <summary>
            ///   Auth Advice Response
            /// </summary>
            public const int _0130_AUTH_ADV_RSP = 0x130;

            /// <summary>
            ///   Transaction Request
            /// </summary>
            public const int _0200_TRAN_REQ = 0x200;

            /// <summary>
            ///   Transaction Request Repeat
            /// </summary>
            public const int _0201_TRAN_REQ_REP = 0x201;

            /// <summary>
            ///   Transaction Completion
            /// </summary>
            public const int _0202_TRAN_CMP = 0x202;

            /// <summary>
            ///   Transaction Completion Repeat
            /// </summary>
            public const int _0203_TRAN_CMP_REP = 0x203;

            /// <summary>
            ///   Transaction Response
            /// </summary>
            public const int _0210_TRAN_REQ_RSP = 0x210;

            /// <summary>
            ///   Transaction Completion Response
            /// </summary>
            public const int _0212_TRAN_CMP_RSP = 0x212;

            /// <summary>
            ///   Transaction Advice
            /// </summary>
            public const int _0220_TRAN_ADV = 0x220;

            /// <summary>
            ///   Transaction Advice Repeat
            /// </summary>
            public const int _0221_TRAN_ADV_REP = 0x221;

            /// <summary>
            ///   Transaction Advice Response
            /// </summary>
            public const int _0230_TRAN_ADV_RSP = 0x230;

            /// <summary>
            ///   Acquirer file update request
            /// </summary>
            public const int _0300_ACQUIRER_FILE_UPDATE_REQ = 0x300;

            /// <summary>
            ///   Acquirer file update response
            /// </summary>
            public const int _0310_ACQUIRER_FILE_UPDATE_RSP = 0x310;

            /// <summary>
            ///   Acquirer File Update Advice
            /// </summary>
            public const int _0320_ACQUIRER_FILE_UPDATE_ADV = 0x320;

            /// <summary>
            ///   Issuer File Update Advice
            /// </summary>
            public const int _0322_ISSUER_FILE_UPDATE_ADV = 0x322;

            /// <summary>
            ///   Acquirer File Update Advice Response
            /// </summary>
            public const int _0330_ACQUIRER_FILE_UPDATE_ADV_RSP = 0x330;

            /// <summary>
            ///   Issuer File Update Advice Response
            /// </summary>
            public const int _0332_ISSUER_FILE_UPDATE_ADV_RSP = 0x332;

            /// <summary>
            ///   Acquirer Reversal Request
            /// </summary>
            public const int _0400_ACQUIRER_REV_REQ = 0x400;

            /// <summary>
            ///   Acquirer Reversal Request Response
            /// </summary>
            public const int _0410_ACQUIRER_REV_REQ_RSP = 0x410;

            /// <summary>
            ///   Acquirer Reversal Advice
            /// </summary>
            public const int _0420_ACQUIRER_REV_ADV = 0x420;

            /// <summary>
            ///   Acquirer Reversal Advice Repeat
            /// </summary>
            public const int _0421_ACQUIRER_REV_ADV_REP = 0x421;

            /// <summary>
            ///   Acquirer Reversal Advice Response
            /// </summary>
            public const int _0430_ACQUIRER_REV_ADV_RSP = 0x430;

            /// <summary>
            ///   Acquirer Recon Request
            /// </summary>
            public const int _0500_ACQUIRER_RECONCILE_REQ = 0x500;

            /// <summary>
            ///   Acquirer Recon Request Response
            /// </summary>
            public const int _0510_ACQUIRER_RECONCILE_REQ_RSP = 0x510;

            /// <summary>
            ///   Acquirer Recon Advice
            /// </summary>
            public const int _0520_ACQUIRER_RECONCILE_ADV = 0x520;

            /// <summary>
            ///   Acquirer Recon Advice Repeat
            /// </summary>
            public const int _0521_ACQUIRER_RECONCILE_ADV_REP = 0x521;

            /// <summary>
            ///   Acquirer Recon Advice Response
            /// </summary>
            public const int _0530_ACQUIRER_RECONCILE_ADV_RSP = 0x530;

            /// <summary>
            ///   Administrative Request
            /// </summary>
            public const int _0600_ADMIN_REQ = 0x600;

            /// <summary>
            ///   Administrative Request
            /// </summary>
            public const int _0601_ADMIN_REQ_REP = 0x601;

            /// <summary>
            ///   Administrative Request Response
            /// </summary>
            public const int _0610_ADMIN_REQ_RSP = 0x610;

            /// <summary>
            ///   Network Management Request
            /// </summary>
            public const int _0800_NWRK_MNG_REQ = 0x800;

            /// <summary>
            ///   Network Management Request Repeat
            /// </summary>
            public const int _0801_NWRK_MNG_REQ_REP = 0x801;

            /// <summary>
            ///   Network Management Response
            /// </summary>
            public const int _0810_NWRK_MNG_REQ_RSP = 0x810;

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Gets the response message type for the given message type. E.g. 0220 -&gt; 0230, 0421 -&gt; 0430
            /// </summary>
            /// <param name="msgType">
            /// Request Message Type 
            /// </param>
            /// <returns>
            /// Response Message Type 
            /// </returns>
            public static int GetResponse(int msgType)
            {
                return msgType - (msgType % 2) + 0x10;
            }

            #endregion
        }
    }
}