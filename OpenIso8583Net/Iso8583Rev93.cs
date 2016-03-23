using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Generic Implmentable ISO 8583 Revision 93 class
    /// </summary>
    public class Iso8583Rev93 : Iso8583
    {
        private static readonly Template DefaultTemplate;

        static Iso8583Rev93()
        {
            // TODO There are some TermApp.ISO specific fields in here.  Need to remove them so as to conform to the spec.
            DefaultTemplate =
                new Template
                {
                    { Bit._002_PAN, FieldDescriptor.AsciiVar(2, 19, FieldValidators.N) },
                    { Bit._003_PROC_CODE, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._004_TRAN_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._005_SETTLE_AMOUNT, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._007_TRAN_DATE_TIME, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._009_CONVERSION_RATE_SETTLEMENT, FieldDescriptor.AsciiFixed(8, FieldValidators.N) },
                    { Bit._011_SYS_TRACE_AUDIT_NUM, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._012_LOCAL_TRAN_DATETIME, FieldDescriptor.AsciiFixed(12, FieldValidators.N) },
                    { Bit._014_EXPIRY_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._016_CONVERSION_DATE, FieldDescriptor.AsciiFixed(4, FieldValidators.N) },
                    { Bit._022_POS_DATA_CODE, FieldDescriptor.AsciiFixed(15, FieldValidators.Ans) },
                    { Bit._023_CARD_SEQ_NR, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._024_FUNC_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._027_APPROVAL_CODE_LEN, FieldDescriptor.AsciiFixed(1, FieldValidators.N) },
                    { Bit._028_RECON_DATE, FieldDescriptor.AsciiFixed(6, FieldValidators.N) },
                    { Bit._029_RECON_INDICATOR, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._030_AMOUNTS_ORIGINAL, FieldDescriptor.AsciiFixed(24, FieldValidators.N) },
                    { Bit._032_ACQ_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._035_TRACK_2_DATA, FieldDescriptor.AsciiVar(2, 37, FieldValidators.Track2) },
                    { Bit._037_RET_REF_NR, FieldDescriptor.AsciiFixed(12, FieldValidators.Anp) },
                    { Bit._038_APPROVAL_CODE, FieldDescriptor.AsciiFixed(6, FieldValidators.Anp) },
                    { Bit._039_ACTION_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._040_SERVICE_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._041_TERMINAL_ID, FieldDescriptor.AsciiFixed(8, FieldValidators.Ans) },
                    { Bit._042_CARD_ACCEPTOR_ID, FieldDescriptor.AsciiFixed(15, FieldValidators.Ans) },
                    { Bit._044_ADDITIONAL_RESPONSE_DATA, FieldDescriptor.AsciiVar(2, 99, FieldValidators.Ans) },
                    { Bit._045_TRACK_1_DATA, FieldDescriptor.AsciiVar(2, 76, FieldValidators.Ans) },
                    { Bit._046_FEES_AMOUNTS, FieldDescriptor.AsciiVar(3, 204, FieldValidators.Ans) },
                    { Bit._048_PRIVATE_ADDITIONAL_DATA, FieldDescriptor.AsciiVar(4, 9999, FieldValidators.Ans) },
                    { Bit._049_TRAN_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._050_SETTLEMENT_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._052_PIN_DATA, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
                    { Bit._053_SECURITY_INFO, FieldDescriptor.AsciiVar(2, 96, FieldValidators.Hex) },
                    { Bit._054_ADDITIONAL_AMOUNTS, FieldDescriptor.AsciiVar(2, 96, FieldValidators.Hex) },
                    { Bit._055_ICC_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Hex) },
                    { Bit._056_ORIG_DATA_ELEMENTS, FieldDescriptor.AsciiVar(2, 35, FieldValidators.N) },
                    { Bit._057_AUTH_LIFE_CYCLE_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._058_AUTH_AGENT_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._059_ECHO_DATA, FieldDescriptor.AsciiVar(3, 200, FieldValidators.Ans) },
                    { Bit._062_HOTCARD_CAPACITY, FieldDescriptor.AsciiVar(3, 5, FieldValidators.N) },
                    { Bit._063_TERMAPP_PRIVATE_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Hex) },
                    { Bit._064_MAC, FieldDescriptor.AsciiFixed(8, FieldValidators.Hex) },
                    { Bit._066_ORIGINAL_FEES_AMOUNTS, FieldDescriptor.AsciiVar(3, 204, FieldValidators.Ans) },
                    { Bit._067_EXT_PAYMENT_DATA, FieldDescriptor.AsciiFixed(2, FieldValidators.N) },
                    { Bit._071_MSG_NR, FieldDescriptor.AsciiFixed(8, FieldValidators.N) },
                    { Bit._072_DATA_RECORD, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans) },
                    { Bit._074_NR_CREDITS, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._075_NR_CREDITS_REVERSAL, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._076_NR_DEBITS, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._077_NR_DEBITS_REVERSAL, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._081_NR_AUTHS, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._086_AMOUNT_CREDITS, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._087_AMOUNT_CREDITS_REVERSAL, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._088_AMOUNT_DEBITS, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._089_AMOUNT_DEBITS_REVERSAL, FieldDescriptor.AsciiFixed(16, FieldValidators.N) },
                    { Bit._090_NR_AUTHS_REVERSAL, FieldDescriptor.AsciiFixed(10, FieldValidators.N) },
                    { Bit._096_KEY_MANAGEMENT_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans) },
                    { Bit._097_AMOUNT_NET_RECON, FieldDescriptor.AsciiFixed(17, FieldValidators.An) },
                    { Bit._098_PAYEE, FieldDescriptor.AsciiFixed(25, FieldValidators.Ans) },
                    { Bit._100_RECEIVING_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._101_FILE_NAME, FieldDescriptor.AsciiVar(2, 99, FieldValidators.Ans) },
                    { Bit._102_ACCOUNT_ID_1, FieldDescriptor.AsciiVar(2, 28, FieldValidators.Ans) },
                    { Bit._103_ACCOUNT_ID_2, FieldDescriptor.AsciiVar(2, 28, FieldValidators.Ans) },
                    { Bit._104_TRAN_DESCRIPTION, FieldDescriptor.AsciiVar(4, 9999, FieldValidators.Ans) },
                    { Bit._109_FEE_AMOUNTS_CREDITS, FieldDescriptor.AsciiVar(2, 84, FieldValidators.Ans) },
                    { Bit._110_FEE_AMOUNTS_DEBITS, FieldDescriptor.AsciiVar(2, 84, FieldValidators.Ans) },
                    { Bit._123_RECEIPT_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans) },
                    { Bit._124_DISPLAY_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans) },
                    { Bit._128_MAC, FieldDescriptor.AsciiFixed(8, FieldValidators.Hex) },
                };
        }

        /// <summary>
        ///   Creates a new instance of the Iso8583 class
        /// </summary>
        public Iso8583Rev93()
            : this(DefaultTemplate)
        {
        }

        /// <summary>
        ///   Create a new instance of the Iso8583Rev93 class with the specified template overrides
        /// </summary>
        /// <param name = "template">Template override</param>
        public Iso8583Rev93(Template template)
            : base(template)
        {
        }

        #region Nested type: Bit

        /// <summary>
        ///   Human readable constants mapping to field numbers
        /// </summary>
        public new class Bit
        {
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
            public const int _012_LOCAL_TRAN_DATETIME = 12;

            /// <summary>
            ///   Field 14 - Expiry Date
            /// </summary>
            public const int _014_EXPIRY_DATE = 14;

            /// <summary>
            ///   Field 16 - Conversion Date
            /// </summary>
            public const int _016_CONVERSION_DATE = 16;

            /// <summary>
            ///   Field 22 - POS Data Code
            /// </summary>
            public const int _022_POS_DATA_CODE = 22;

            /// <summary>
            ///   Field 23 - Card Sequence Number
            /// </summary>
            public const int _023_CARD_SEQ_NR = 23;

            /// <summary>
            ///   Field 24 - Function Code
            /// </summary>
            public const int _024_FUNC_CODE = 24;

            /// <summary>
            ///   Field 25 - Message Reason Code
            /// </summary>
            public const int _025_MSG_REASON_CODE = 25;

            /// <summary>
            ///   Field 27 - Approval Code Length
            /// </summary>
            public const int _027_APPROVAL_CODE_LEN = 27;

            /// <summary>
            ///   Field 28 - Reconciliation Date
            /// </summary>
            public const int _028_RECON_DATE = 28;

            /// <summary>
            ///   Field 29 - Reconciliation Indicator
            /// </summary>
            public const int _029_RECON_INDICATOR = 29;

            /// <summary>
            ///   Field 30 - Original Amounts
            /// </summary>
            public const int _030_AMOUNTS_ORIGINAL = 30;

            /// <summary>
            ///   Field 32 - Acquiring Institutino ID Code
            /// </summary>
            public const int _032_ACQ_INST_ID_CODE = 32;

            /// <summary>
            ///   Field 35 - Track 2 Data
            /// </summary>
            public const int _035_TRACK_2_DATA = 35;

            /// <summary>
            ///   Field 37 - Retrieval Reference Number
            /// </summary>
            public const int _037_RET_REF_NR = 37;

            /// <summary>
            ///   Field 38 - Approval Code
            /// </summary>
            public const int _038_APPROVAL_CODE = 38;

            /// <summary>
            ///   Field 39 - Action Code
            /// </summary>
            public const int _039_ACTION_CODE = 39;

            /// <summary>
            ///   Field 40 - Service Code
            /// </summary>
            public const int _040_SERVICE_CODE = 40;

            /// <summary>
            ///   Field 41 - Terminal ID
            /// </summary>
            public const int _041_TERMINAL_ID = 41;

            /// <summary>
            ///   Field 42 - Card Acceptor ID
            /// </summary>
            public const int _042_CARD_ACCEPTOR_ID = 42;

            /// <summary>
            ///   Field 44 - Additional Response Data
            /// </summary>
            public const int _044_ADDITIONAL_RESPONSE_DATA = 44;

            /// <summary>
            ///   Field 45 - Track 1 Data
            /// </summary>
            public const int _045_TRACK_1_DATA = 45;

            /// <summary>
            ///   Field 46 - Fees Amounts
            /// </summary>
            public const int _046_FEES_AMOUNTS = 46;

            /// <summary>
            ///   Field 48 - Private Additional Data
            /// </summary>
            public const int _048_PRIVATE_ADDITIONAL_DATA = 48;

            /// <summary>
            ///   Field 49 - Transaction Currency Code
            /// </summary>
            public const int _049_TRAN_CURRENCY_CODE = 49;

            /// <summary>
            ///   Field 50 - Settlement Currency Code
            /// </summary>
            public const int _050_SETTLEMENT_CURRENCY_CODE = 50;

            /// <summary>
            ///   Field 52 - PIN Data
            /// </summary>
            public const int _052_PIN_DATA = 52;

            /// <summary>
            ///   Field 53 - Security Related Information
            /// </summary>
            public const int _053_SECURITY_INFO = 53;

            /// <summary>
            ///   Field 54 - Additional Amounts
            /// </summary>
            public const int _054_ADDITIONAL_AMOUNTS = 54;

            /// <summary>
            ///   Field 55 - ICC Data
            /// </summary>
            public const int _055_ICC_DATA = 55;

            /// <summary>
            ///   Field 56 - Original Data Elements
            /// </summary>
            public const int _056_ORIG_DATA_ELEMENTS = 56;

            /// <summary>
            ///   Field 57 - Auth Life Cycle Code
            /// </summary>
            public const int _057_AUTH_LIFE_CYCLE_CODE = 57;

            /// <summary>
            ///   Field 58 - Authorizing Agent Institution ID Code
            /// </summary>
            public const int _058_AUTH_AGENT_INST_ID_CODE = 58;

            /// <summary>
            ///   Field 59 - Echo Data
            /// </summary>
            public const int _059_ECHO_DATA = 59;

            /// <summary>
            ///   Field 62 - Hotcard Capacity
            /// </summary>
            public const int _062_HOTCARD_CAPACITY = 62;

            /// <summary>
            ///   Field 63 - TermApp.ISO Private Data
            /// </summary>
            public const int _063_TERMAPP_PRIVATE_DATA = 63;

            /// <summary>
            ///   Field 64 - Message Authentication Code
            /// </summary>
            public const int _064_MAC = 64;

            /// <summary>
            ///   Field 66 - Original Fees Amounts
            /// </summary>
            public const int _066_ORIGINAL_FEES_AMOUNTS = 66;

            /// <summary>
            ///   Fiel 67 - Extended Payment Data
            /// </summary>
            public const int _067_EXT_PAYMENT_DATA = 67;

            /// <summary>
            ///   Field 71 - Message Number
            /// </summary>
            public const int _071_MSG_NR = 71;

            /// <summary>
            ///   Field 72 - Data Record
            /// </summary>
            public const int _072_DATA_RECORD = 72;

            /// <summary>
            ///   Field 74 - Credits, Number
            /// </summary>
            public const int _074_NR_CREDITS = 74;

            /// <summary>
            ///   Field 75 - Credits Reversal, Number
            /// </summary>
            public const int _075_NR_CREDITS_REVERSAL = 75;

            /// <summary>
            ///   Field 76 - Debits, Number
            /// </summary>
            public const int _076_NR_DEBITS = 76;

            /// <summary>
            ///   Field 77 - Debits Reversal, Number
            /// </summary>
            public const int _077_NR_DEBITS_REVERSAL = 77;

            /// <summary>
            ///   Field 81 - Authorisations, Number
            /// </summary>
            public const int _081_NR_AUTHS = 81;

            /// <summary>
            ///   Field 86 - Credits, Amount
            /// </summary>
            public const int _086_AMOUNT_CREDITS = 86;

            /// <summary>
            ///   Field 87 - Credits Reversal, Amount
            /// </summary>
            public const int _087_AMOUNT_CREDITS_REVERSAL = 87;

            /// <summary>
            ///   Field 88 - Debits, Amount
            /// </summary>
            public const int _088_AMOUNT_DEBITS = 88;

            /// <summary>
            ///   Field 89 - Debits Reversal, Amount
            /// </summary>
            public const int _089_AMOUNT_DEBITS_REVERSAL = 89;

            /// <summary>
            ///   Field 90 - Authorisations Reversal, Number
            /// </summary>
            public const int _090_NR_AUTHS_REVERSAL = 90;

            /// <summary>
            ///   Field 96 - Key Management Data
            /// </summary>
            public const int _096_KEY_MANAGEMENT_DATA = 96;

            /// <summary>
            ///   Field 97 - Net Reconciliation Amount
            /// </summary>
            public const int _097_AMOUNT_NET_RECON = 97;

            /// <summary>
            ///   Field 98 - Payee
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
            ///   Field 102 - Account ID 1
            /// </summary>
            public const int _102_ACCOUNT_ID_1 = 102;

            /// <summary>
            ///   Field 103 - Account ID 2
            /// </summary>
            public const int _103_ACCOUNT_ID_2 = 103;

            /// <summary>
            ///   Field 104 - Transaction Description
            /// </summary>
            public const int _104_TRAN_DESCRIPTION = 104;

            /// <summary>
            ///   Field 109 - Fee Amounts, Credits
            /// </summary>
            public const int _109_FEE_AMOUNTS_CREDITS = 109;

            /// <summary>
            ///   Field 110 - Fee Amounts, Debits
            /// </summary>
            public const int _110_FEE_AMOUNTS_DEBITS = 110;

            /// <summary>
            ///   Fied 123 - Receipt Data
            /// </summary>
            public const int _123_RECEIPT_DATA = 123;

            /// <summary>
            ///   Field 124 - Display Data
            /// </summary>
            public const int _124_DISPLAY_DATA = 124;

            /// <summary>
            ///   Field 128 - Message Authentication Code
            /// </summary>
            public const int _128_MAC = 128;
        }

        #endregion

        #region Nested type: MsgType

        /// <summary>
        ///   Human readable constants mapping to message types
        /// </summary>
        public new class MsgType
        {
            /// <summary>
            ///   Invalid Message
            /// </summary>
            public const int _0000_INVALID_MSG = Iso8583.MsgType._0000_INVALID_MSG;

            /// <summary>
            ///   Authorisation Request
            /// </summary>
            public const int _1100_AUTH_REQ = 0x1100;

            /// <summary>
            ///   Authorisation Request Response
            /// </summary>
            public const int _1110_AUTH_REQ_RSP = 0x1110;

            /// <summary>
            ///   Authorisation Advice
            /// </summary>
            public const int _1120_AUTH_ADV = 0x1120;

            /// <summary>
            ///   Authorisation Advice Response
            /// </summary>
            public const int _1130_AUTH_ADV_RSP = 0x1130;

            /// <summary>
            ///   Transaction Request
            /// </summary>
            public const int _1200_TRAN_REQ = 0x1200;

            /// <summary>
            ///   Transaction Request Response
            /// </summary>
            public const int _1210_TRAN_REQ_RSP = 0x1210;

            /// <summary>
            ///   Transaction Advice
            /// </summary>
            public const int _1220_TRAN_ADV = 0x1220;

            /// <summary>
            ///   Transaction Advice Response
            /// </summary>
            public const int _1230_TRAN_ADV_RSP = 0x1230;

            /// <summary>
            ///   File Action Request
            /// </summary>
            public const int _1304_FILE_ACTION_REQ = 0x1304;

            /// <summary>
            ///   File Action Request Response
            /// </summary>
            public const int _1314_FILE_ACTION_REQ_RSP = 0x1314;

            /// <summary>
            ///   Reversal Advice
            /// </summary>
            public const int _1420_TRAN_REV_ADV = 0x1420;

            /// <summary>
            ///   Reversal Advice Response
            /// </summary>
            public const int _1430_TRAN_REV_ADV_RSP = 0x1430;

            /// <summary>
            ///   Reconciliation Request
            /// </summary>
            public const int _1500_RECON_REQ = 0x1500;

            /// <summary>
            ///   Reconciliation Request Response
            /// </summary>
            public const int _1510_RECON_REQ_RSP = 0x1510;

            /// <summary>
            ///   Reconciliation Advice
            /// </summary>
            public const int _1520_RECON_ADV = 0x1520;

            /// <summary>
            ///   Reconciliation Advice Response
            /// </summary>
            public const int _1530_RECON_ADV_RSP = 0x1530;

            /// <summary>
            ///   Administration Request
            /// </summary>
            public const int _1604_ADMIN_REQ = 0x1604;

            /// <summary>
            ///   Administration Request Response
            /// </summary>
            public const int _1614_ADMIN_REQ_RSP = 0x1614;

            /// <summary>
            ///   Administration Advice
            /// </summary>
            public const int _1624_ADMIN_ADV = 0x1624;

            /// <summary>
            ///   Administration Advice Response
            /// </summary>
            public const int _1634_ADMIN_ADV_RSP = 0x1634;

            /// <summary>
            ///   Network Management Request
            /// </summary>
            public const int _1804_NWRK_MNG_REQ = 0x1804;

            /// <summary>
            ///   Network Management Request Response
            /// </summary>
            public const int _1814_NWRK_MNG_REQ_RSP = 0x1814;
        }

        #endregion
    }
}