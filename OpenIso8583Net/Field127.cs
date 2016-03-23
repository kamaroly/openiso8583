using System;
using System.Text;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Field 127 is Postilions private field which itself is a bitmap message.
    /// </summary>
    public class Field127 : AMessage, IField
    {
        public Field127() : base(GetPrivFieldTemplate()) { }

        #region IField Members

        /// <summary>
        ///   Gets the field number that this field representss
        /// </summary>
        public int FieldNumber
        {
            get { return 127; }
        }

        /// <summary>
        ///   The Value contained in the field
        /// </summary>
        public string Value
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   Gets the packed length of the message
        /// </summary>
        public new int PackedLength
        {
            get { return 6 + base.PackedLength; }
        }

        /// <summary>
        ///   Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>byte[] representing the message</returns>
        public override byte[] ToMsg()
        {
            var msg = new byte[PackedLength];
            var contentLength = PackedLength - 6;
            var lenHdr = Encoding.ASCII.GetBytes(contentLength.ToString().PadLeft(6, '0'));
            Array.Copy(lenHdr, msg, 6);
            var baseMsg = base.ToMsg();
            Array.Copy(baseMsg, 0, msg, 6, baseMsg.Length);
            return msg;
        }

        /// <summary>
        ///   Unpacks the message from a byte array
        /// </summary>
        /// <param name = "msg">message data to unpack</param>
        /// <param name = "startingOffset">the offset in the array to start</param>
        /// <returns>the offset in the array representing the start of the next message</returns>
        public override int Unpack(byte[] msg, int startingOffset)
        {
            // Field 127 is actually a bitmap message but inside a field in Iso8583Post.  That field
            // has a length indicator of 6 bytes, so lets just ignore it.
            // Yes I should be checking that everything adds up
            // TODO Stop being a muppet and check it
            return base.Unpack(msg, 6 + startingOffset);
        }

        #endregion

        /// <summary>
        /// Defines the Template used to describe the content of Field 127 - Realtime Private Field
        /// </summary>
        /// <returns>A Template defining the subfields contained in Field 127</returns>
        protected static Template GetPrivFieldTemplate()
        {
            var template = new Template
                {
                    { Bit._002_SWITCH_KEY,          FieldDescriptor.AsciiVar( 2, 32,FieldValidators.AlphaNumericSpecial)},
                    { Bit._003_ROUTING_INFORMATION,    FieldDescriptor.AsciiFixed( 48,FieldValidators.AlphaNumericSpecial)},
                    { Bit._004_POS_DATA,    FieldDescriptor.AsciiFixed( 22,FieldValidators.AlphaNumericSpecial)},
                    { Bit._005_SERVICE_STATION_DATA,    FieldDescriptor.AsciiFixed( 73,FieldValidators.AlphaNumericSpecial)},
                    { Bit._006_AUTH_PROFILE,    FieldDescriptor.AsciiFixed(2, FieldValidators.AlphaNumeric)},
                    { Bit._007_CHECK_DATA,    FieldDescriptor.AsciiVar(2, 70,FieldValidators.AlphaNumericSpecial)},
                    { Bit._008_RETENTION_DATA,    FieldDescriptor.AsciiVar( 3, 999,FieldValidators.AlphaNumericSpecial)},
                    { Bit._009_ADDITIONAL_NODE_DATA,    FieldDescriptor.AsciiVar( 3, 999,FieldValidators.AlphaNumericSpecial)},
                    { Bit._010_CVV2,    FieldDescriptor.AsciiFixed(3,FieldValidators.Numeric)},
                    { Bit._011_ORIG_KEY,    FieldDescriptor.AsciiVar(2, 32,FieldValidators.AlphaNumericSpecial)},
                    { Bit._012_TERM_OWNER,    FieldDescriptor.AsciiVar(2, 25,FieldValidators.AlphaNumericSpecial)},
                    { Bit._013_POS_GEOGRAPHIC_DATA,    FieldDescriptor.AsciiFixed(17, FieldValidators.AlphaNumericSpecial)},
                    { Bit._014_SPONSOR_BANK,    FieldDescriptor.AsciiFixed(8,FieldValidators.AlphaNumericSpecial)},
                    { Bit._015_ADDRESS_VERIFICATION_DATA,    FieldDescriptor.AsciiVar(2, 29,FieldValidators.AlphaNumericSpecial)},
                    { Bit._016_ADDRESS_VERIFICATION_RESULT,    FieldDescriptor.AsciiFixed(1,FieldValidators.Alpha)},
                    { Bit._017_CARDHOLDER_INFORMATION,    FieldDescriptor.AsciiVar( 2, 50,FieldValidators.AlphaNumericSpecial)},
                    { Bit._018_VALIDATION_DATA,    FieldDescriptor.AsciiVar(2, 50,FieldValidators.AlphaNumericSpecial)},
                    { Bit._019_BANK_DETAILS,    FieldDescriptor.AsciiFixed(31,FieldValidators.AlphaNumericSpecial)},
                    { Bit._020_ORIG_AUTH_DATE_SETTLEMENT,    FieldDescriptor.AsciiFixed(8,FieldValidators.Numeric)},
                    { Bit._021_RECORD_ID,    FieldDescriptor.AsciiVar(2, 12,FieldValidators.AlphaNumericSpecial)},
                    { Bit._022_STRUCTURED_DATA,    FieldDescriptor.AsciiVar(5, 99999,FieldValidators.AlphaNumericSpecial)},
                    { Bit._023_PAYEE_NAME_ADDR,    FieldDescriptor.AsciiFixed( 253,FieldValidators.AlphaNumericSpecial)},
                    { Bit._024_PAYER_ACC_ID,    FieldDescriptor.AsciiVar( 2, 28,FieldValidators.AlphaNumericSpecial)},
                    { Bit._025_ICC_DATA,    FieldDescriptor.AsciiVar( 4, 9999,FieldValidators.AlphaNumericSpecial)},
                    { Bit._026_ORIGINAL_NODE,    FieldDescriptor.AsciiVar( 2, 20,FieldValidators.AlphaNumericSpecial)},
                    { Bit._027_CARD_VERIFICATION_RESULT,    FieldDescriptor.AsciiFixed( 1,FieldValidators.AlphaNumericSpecial)},
                    { Bit._028_AMEX_CARD_ID,    FieldDescriptor.AsciiFixed(4,FieldValidators.Numeric)},
                    { Bit._029_3D_SECURE_DATA,    FieldDescriptor.AsciiFixed(40,FieldValidators.Hex)},
                    { Bit._030_3D_SECURE_RESULT,    FieldDescriptor.AsciiFixed(1,FieldValidators.AlphaNumericSpecial)},
                    { Bit._031_ISSUER_NETWORK_ID,    FieldDescriptor.AsciiVar( 2, 11,FieldValidators.AlphaNumericSpecial)},
                    { Bit._032_UCAF_DATA,    FieldDescriptor.AsciiVar( 2, 33,FieldValidators.Hex)},
                    { Bit._033_EXTENDED_TRAN_TYPE,    FieldDescriptor.AsciiFixed( 4,FieldValidators.Numeric)},
                    { Bit._034_ACC_TYPE_QUALIFIERS,    FieldDescriptor.AsciiFixed( 2,FieldValidators.Numeric)},
                    { Bit._035_ACQ_NETWORK_ID,    FieldDescriptor.AsciiVar( 2, 11, FieldValidators.AlphaNumericSpecial)},
                    { Bit._036_CUSTOMER_ID,    FieldDescriptor.AsciiVar( 2, 25, FieldValidators.AlphaNumericSpecial)},
                    { Bit._037_EXTENDED_RESPONSE_CODE,    FieldDescriptor.AsciiFixed( 4, FieldValidators.AlphaNumeric)},
                    { Bit._038_ADDITIONAL_POS_DATA_CODE,    FieldDescriptor.AsciiVar( 2, 99, FieldValidators.AlphaNumeric)},
                    { Bit._039_ORIG_RESPONSE_CODE,    FieldDescriptor.AsciiFixed( 2, FieldValidators.AlphaNumeric)},
                    { Bit._40_TRANSACTION_REFERENCE,    FieldDescriptor.AsciiVar( 3, 512, FieldValidators.AlphaNumericSpecial)},
                    { Bit._41_ORIGINATING_REMOTE_ADDR,    FieldDescriptor.AsciiVar( 2, 99, FieldValidators.AlphaNumericSpecial)},
                    { Bit._42_TRANSACTION_NUMBER,    FieldDescriptor.AsciiVar( 2, 99, FieldValidators.Numeric)}  //NOTE: the RTFW 5.5 Interface spec has a typo, the field is 'n..99, LLVAR' and *not* 'n..10, LLVAR' as described in the spec.
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

            throw new UnknownFieldException("127." + field);
        }

        #region Nested type: Bit

        /// <summary>
        ///   Human readable constants mapping to field numbers
        /// </summary>
        public new class Bit
        {
            /// <summary>
            ///   Switch Key
            /// </summary>
            public const int _002_SWITCH_KEY = 2;

            /// <summary>
            ///   Routing information
            /// </summary>
            public const int _003_ROUTING_INFORMATION = 3;

            /// <summary>
            ///   POS Data
            /// </summary>
            public const int _004_POS_DATA = 4;

            /// <summary>
            ///   Service station data
            /// </summary>
            public const int _005_SERVICE_STATION_DATA = 5;

            /// <summary>
            ///   Authorisation profile
            /// </summary>
            public const int _006_AUTH_PROFILE = 6;

            /// <summary>
            ///   Check data
            /// </summary>
            public const int _007_CHECK_DATA = 7;

            /// <summary>
            ///   Retention Data
            /// </summary>
            public const int _008_RETENTION_DATA = 8;

            /// <summary>
            ///   Additional Node Data
            /// </summary>
            public const int _009_ADDITIONAL_NODE_DATA = 9;

            /// <summary>
            ///   CVV2
            /// </summary>
            public const int _010_CVV2 = 10;

            /// <summary>
            ///   Original switch key
            /// </summary>
            public const int _011_ORIG_KEY = 11;

            /// <summary>
            ///   Terminal owner
            /// </summary>
            public const int _012_TERM_OWNER = 12;

            /// <summary>
            ///   POS Geographic Data
            /// </summary>
            public const int _013_POS_GEOGRAPHIC_DATA = 13;

            /// <summary>
            ///   Sponsor Bank
            /// </summary>
            public const int _014_SPONSOR_BANK = 14;

            /// <summary>
            ///   Address Verification Data
            /// </summary>
            public const int _015_ADDRESS_VERIFICATION_DATA = 15;

            /// <summary>
            ///   Address Verification Result
            /// </summary>
            public const int _016_ADDRESS_VERIFICATION_RESULT = 16;

            /// <summary>
            ///   Cardholder Information
            /// </summary>
            public const int _017_CARDHOLDER_INFORMATION = 17;

            /// <summary>
            ///   Validation data
            /// </summary>
            public const int _018_VALIDATION_DATA = 18;

            /// <summary>
            ///   Bank details
            /// </summary>
            public const int _019_BANK_DETAILS = 19;

            /// <summary>
            ///   Originator/Authoriser date settlement
            /// </summary>
            public const int _020_ORIG_AUTH_DATE_SETTLEMENT = 20;

            /// <summary>
            ///   Record Identification
            /// </summary>
            public const int _021_RECORD_ID = 21;

            /// <summary>
            ///   Structured Data
            /// </summary>
            public const int _022_STRUCTURED_DATA = 22;

            /// <summary>
            ///   Payee Name and Address
            /// </summary>
            public const int _023_PAYEE_NAME_ADDR = 23;

            /// <summary>
            ///   Payer account identification
            /// </summary>
            public const int _024_PAYER_ACC_ID = 24;

            /// <summary>
            ///   Integrated circuit card (ICC) data
            /// </summary>
            public const int _025_ICC_DATA = 25;

            /// <summary>
            ///   Original Node
            /// </summary>
            public const int _026_ORIGINAL_NODE = 26;

            /// <summary>
            ///   Card Verification Result
            /// </summary>
            public const int _027_CARD_VERIFICATION_RESULT = 27;

            /// <summary>
            ///   American Express Card Identifier (CID)
            /// </summary>
            public const int _028_AMEX_CARD_ID = 28;

            /// <summary>
            ///   3D-Secure Data
            /// </summary>
            public const int _029_3D_SECURE_DATA = 29;

            /// <summary>
            ///   3D-Secure Result
            /// </summary>
            public const int _030_3D_SECURE_RESULT = 30;

            /// <summary>
            ///   Issuer Network Id
            /// </summary>
            public const int _031_ISSUER_NETWORK_ID = 31;

            /// <summary>
            ///   UCAF Data
            /// </summary>
            public const int _032_UCAF_DATA = 32;

            /// <summary>
            ///   Extended Transaction Type
            /// </summary>
            public const int _033_EXTENDED_TRAN_TYPE = 33;

            /// <summary>
            ///   Account Type Qualifiers
            /// </summary>
            public const int _034_ACC_TYPE_QUALIFIERS = 34;

            /// <summary>
            ///   Acquirer Network Id
            /// </summary>
            public const int _035_ACQ_NETWORK_ID = 35;

            /// <summary>
            /// Customer Id
            /// </summary>
            public const int _036_CUSTOMER_ID = 36;

            /// <summary>
            /// Extended Response Code
            /// </summary>
            public const int _037_EXTENDED_RESPONSE_CODE = 37;

            /// <summary>
            /// Additional Pos Data Code
            /// </summary>
            public const int _038_ADDITIONAL_POS_DATA_CODE = 38;

            /// <summary>
            ///   Original Response Code
            /// </summary>
            public const int _039_ORIG_RESPONSE_CODE = 39;

            /// <summary>
            ///   Transaction Reference
            /// </summary>
            public const int _40_TRANSACTION_REFERENCE = 40;

            /// <summary>
            /// Originating Remote Address
            /// </summary>
            public const int _41_ORIGINATING_REMOTE_ADDR = 41;

            /// <summary>
            /// Transaction Number
            /// </summary>
            public const int _42_TRANSACTION_NUMBER = 42;
        }

        #endregion
    }
}