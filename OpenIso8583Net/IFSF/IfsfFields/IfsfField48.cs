using System;
using System.Linq;
using System.Text;
using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.IFSF.FieldFormatters;

namespace OpenIso8583Net.IFSF.IfsfFields
{
    public class IfsfField48 : AMessage, IField
    {
        /// <summary>
        ///   Creates a new instance of the Iso8583 class
        /// </summary>
        public IfsfField48()
            : base(GetIfsfBaseTemplate())
        {
        }

        /// <summary>
        ///   Gets or sets the message type
        /// </summary>
        public int MessageType { get; set; }
        
        /// <summary>
        /// Field number indicator (48)
        /// </summary>
        public int FieldNumber => 48;

        /// <summary>
        ///   Gets the packed length of the message.
        ///     As this message gets a extra 3 byte as LLLVAR, we need to add it here.
        /// </summary>
        public new int PackedLength => 3 + base.PackedLength;

        public string Value { get; set; }

        protected override IField CreateField(int field)
        {
            if (Template.ContainsKey(field))
            {
                return new Field(field, Template[field]);
            }

            throw new UnknownFieldException("48." + field);
        }

        private static Template GetIfsfBaseTemplate()
        {
            var template = new Template
            {
                {
                    Bit._02_HARDWARE_SOFTWARE_CONF,
                    FieldDescriptor.AsciiFixed(20, FieldValidators.AlphaNumericSpecial)
                },
                {
                    Bit._03_LANG_CODE,
                    FieldDescriptor.AsciiFixed(2, FieldValidators.AlphaNumeric)
                },
                {
                    Bit._04_BTCH_SEQ_NUMBER,
                    FieldDescriptor.AsciiFixed(10, FieldValidators.N)
                },
                {
                    Bit._14_PIN_ENCRYPT_METHOD,
                    FieldDescriptor.AsciiFixed(2, FieldValidators.AlphaNumeric)
                }
            };
            return template;
        }

        /// <summary>
        ///   Returns the contents of the message as a string
        /// </summary>
        /// <param name = "prefix">The prefix to apply to each line in the message</param>
        /// <returns>Pretty printed string</returns>
        public override string ToString(string prefix)
        {
            var sb = new StringBuilder();
            sb.Append(prefix + "Field IFSF 48 " + ":" + Environment.NewLine);
            sb.Append(base.ToString(prefix));
            return sb.ToString();
        }

        public override byte[] ToMsg()
        {
            var baseMsg = base.ToMsg();

            var messageLength = baseMsg.Length;
            var lenHdr = Encoding.ASCII.GetBytes(messageLength.ToString().PadLeft(3, '0'));

            return lenHdr.Concat(baseMsg).ToArray();
        }

        #region Nested type: Bit

        /// <summary>
        ///   Human readable constants mapping to field numbers
        /// </summary>
        public class Bit
        {
            /// <summary>
            ///   Communications diagnostics
            /// </summary>
            public const int _01_COM_DIAG = 1;

            /// <summary>
            ///   Hardware and Software Configuration
            /// </summary>
            public const int _02_HARDWARE_SOFTWARE_CONF = 2;

            /// <summary>
            ///   Language Code
            /// </summary>
            public const int _03_LANG_CODE = 3;

            /// <summary>
            ///  Batch/Sequence Number
            /// </summary>
            public const int _04_BTCH_SEQ_NUMBER = 4;

            /// <summary>
            ///  Shift Number
            /// </summary>
            public const int _05_SHIFT_NUMBER = 5;

            /// <summary>
            /// Clerk ID
            /// </summary>
            public const int _06_CLERK_ID = 6;

            /// <summary>
            /// Multiple transaction control 
            /// </summary>
            public const int _07_MULT_TRANS_CTRL = 7;

            /// <summary>
            /// Customer data
            /// </summary>
            public const int _08_CUSTOMER_DATA = 8;

            /// <summary>
            /// Track 2 for second card
            /// </summary>
            public const int _09_TRACK2_SECOND_CARD = 9;

            /// <summary>
            /// Track 1 for second card
            /// </summary>
            public const int _10_TRACK1_SECOND_CARD = 10;

            /// <summary>
            /// Type Of Card
            /// </summary>
            public const int _11_TYPE_OF_CARD = 11;

            /// <summary>
            /// Administratively directed task
            /// </summary>
            public const int _12_ADMIN_DIRECT_TASK = 12;

            /// <summary>
            /// RFID Data
            /// </summary>
            public const int _13_RFID_DATA = 13;

            /// <summary>
            /// PIN Encryption methodology
            /// </summary>
            public const int _14_PIN_ENCRYPT_METHOD = 14;

            /// <summary>
            /// Settlement period
            /// </summary>
            public const int _15_SETTLE_PERIOD = 15;

            /// <summary>
            /// Online Time
            /// </summary>
            public const int _16_ONLINE_TIME = 16;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _17_RESERVED_FUTURE = 17;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _18_RESERVED_FUTURE = 18;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _19_RESERVED_FUTURE = 19;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _20_RESERVED_FUTURE = 20;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _21_RESERVED_FUTURE = 21;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _22_RESERVED_FUTURE = 22;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _23_RESERVED_FUTURE = 23;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _24_RESERVED_FUTURE = 24;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _25_RESERVED_FUTURE = 25;


            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _26_RESERVED_FUTURE = 26;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _27_RESERVED_FUTURE = 27;


            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _28_RESERVED_FUTURE = 28;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _29_RESERVED_FUTURE = 29;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _30_RESERVED_FUTURE = 30;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _31_RESERVED_FUTURE = 31;

            /// <summary>
            /// Reserved Future USE
            /// </summary>
            public const int _32_RESERVED_FUTURE = 32;


            /// <summary>
            /// Track 3 for second card
            /// </summary>
            public const int _33_TRACK3_SECOND_CARD = 33;

            /// <summary>
            /// Encrypted new PIN
            /// </summary>
            public const int _34_ENCRYPTED_NEW_PIN = 34;

            /// <summary>
            /// PAN, second card
            /// </summary>
            public const int _35_PAN_SECOND_CARD = 35;

            /// <summary>
            /// Expiration date, second card
            /// </summary>
            public const int _36_EXP_DATE_SECOND_CARD = 36;

            /// <summary>
            /// Vehicle identification entry mode
            /// </summary>
            public const int _37_VEHICLE_ENTRY_MODE = 37;


            /// <summary>
            /// Pump linked indicator
            /// </summary>
            public const int _38_PUMP_LINKED_INDICATOR = 38;

            /// <summary>
            /// Delivery note number
            /// </summary>
            public const int _39_DELIVERY_NOTE_NUMBER = 39;

            /// <summary>
            /// Encryption Parameter
            /// </summary>
            public const int _40_ENCRYPT_PARAMETER = 40;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _41_RESERVED_PROPRIETARY_USE = 41;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _42_RESERVED_PROPRIETARY_USE = 42;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _43_RESERVED_PROPRIETARY_USE = 43;


            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _44_RESERVED_PROPRIETARY_USE = 44;


            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _45_RESERVED_PROPRIETARY_USE = 45;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _46_RESERVED_PROPRIETARY_USE = 46;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _47_RESERVED_PROPRIETARY_USE = 47;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _48_RESERVED_PROPRIETARY_USE = 48;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _49_RESERVED_PROPRIETARY_USE = 49;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _50_RESERVED_PROPRIETARY_USE = 50;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _51_RESERVED_PROPRIETARY_USE = 51;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _52_RESERVED_PROPRIETARY_USE = 52;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _53_RESERVED_PROPRIETARY_USE = 53;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _54_RESERVED_PROPRIETARY_USE = 54;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _55_RESERVED_PROPRIETARY_USE = 55;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _56_RESERVED_PROPRIETARY_USE = 56;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _57_RESERVED_PROPRIETARY_USE = 57;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _58_RESERVED_PROPRIETARY_USE = 58;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _59_RESERVED_PROPRIETARY_USE = 59;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _60_RESERVED_PROPRIETARY_USE = 60;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _61_RESERVED_PROPRIETARY_USE = 61;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _62_RESERVED_PROPRIETARY_USE = 62;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _63_RESERVED_PROPRIETARY_USE = 63;

            /// <summary>
            /// Reserved for proprietary use
            /// </summary>
            public const int _64_RESERVED_PROPRIETARY_USE = 64;
        }

        #endregion
    }
}