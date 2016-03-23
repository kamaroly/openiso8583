using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.Formatter;
using OpenIso8583Net.LengthFormatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenIso8583Net.TermAppIso
{
    public class Iso8583TermApp : Iso8583Rev93
    {
        private static readonly Template binaryTemplate;
        private static readonly Template asciiTemplate;

        static Iso8583TermApp()
        {
            binaryTemplate =
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
                    { Bit._048_PRIVATE_ADDITIONAL_DATA, FieldDescriptor.AsciiVar(4, 9999, FieldValidators.None) },
                    { Bit._049_TRAN_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._050_SETTLEMENT_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._052_PIN_DATA, FieldDescriptor.BinaryFixed(8) },
                    { Bit._053_SECURITY_INFO, new FieldDescriptor(new VariableLengthFormatter(2, 96), FieldValidators.Hex, Formatters.Binary, null) },
                    { Bit._054_ADDITIONAL_AMOUNTS, FieldDescriptor.AsciiVar(3, 96, FieldValidators.Hex) },
                    { Bit._055_ICC_DATA, FieldDescriptor.BinaryVar(3, 999, FieldValidators.None) },
                    { Bit._056_ORIG_DATA_ELEMENTS, FieldDescriptor.AsciiVar(2, 35, FieldValidators.N) },
                    { Bit._057_AUTH_LIFE_CYCLE_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._058_AUTH_AGENT_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._059_ECHO_DATA, FieldDescriptor.AsciiVar(3, 200, FieldValidators.Ans) },
                    { Bit._062_HOTCARD_CAPACITY, FieldDescriptor.AsciiVar(3, 5, FieldValidators.N) },
                    { Bit._063_TERMAPP_PRIVATE_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Hex) },
                    { Bit._064_MAC, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
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
                    { Bit._128_MAC, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
                };
            binaryTemplate.BitmapFormatter = Formatters.Binary;

            asciiTemplate =
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
                    { Bit._048_PRIVATE_ADDITIONAL_DATA, FieldDescriptor.AsciiVar(4, 9999, FieldValidators.None) },
                    { Bit._049_TRAN_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._050_SETTLEMENT_CURRENCY_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._052_PIN_DATA, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
                    { Bit._053_SECURITY_INFO, new FieldDescriptor(new VariableLengthFormatter(2, 96), FieldValidators.Hex, Formatters.Binary, null) },
                    { Bit._054_ADDITIONAL_AMOUNTS, FieldDescriptor.AsciiVar(3, 96, FieldValidators.Hex) },
                    { Bit._055_ICC_DATA, FieldDescriptor.BinaryVar(3, 999, FieldValidators.None) },
                    { Bit._056_ORIG_DATA_ELEMENTS, FieldDescriptor.AsciiVar(2, 35, FieldValidators.N) },
                    { Bit._057_AUTH_LIFE_CYCLE_CODE, FieldDescriptor.AsciiFixed(3, FieldValidators.N) },
                    { Bit._058_AUTH_AGENT_INST_ID_CODE, FieldDescriptor.AsciiVar(2, 11, FieldValidators.N) },
                    { Bit._059_ECHO_DATA, FieldDescriptor.AsciiVar(3, 200, FieldValidators.Ans) },
                    { Bit._062_HOTCARD_CAPACITY, FieldDescriptor.AsciiVar(3, 5, FieldValidators.N) },
                    { Bit._063_TERMAPP_PRIVATE_DATA, FieldDescriptor.AsciiVar(3, 999, FieldValidators.Hex) },
                    { Bit._064_MAC, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
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
                    { Bit._128_MAC, FieldDescriptor.AsciiFixed(16, FieldValidators.Hex) },
                };
            asciiTemplate.BitmapFormatter = Formatters.Ascii;
        }

        private bool ascii;
        private bool unpacking = false;


        public Iso8583TermApp()
            : base(binaryTemplate)
        {
        }

        protected override IField CreateField(int field)
        {
            if (field == Iso8583TermApp.Bit._048_PRIVATE_ADDITIONAL_DATA)
                return new AdditionalData();

            // I'm certain that TermApp.ISO in Postilion is broken when it comes to MACcing.  Using the 'B' format
            // when sending a message it has to be ASCII fixed 16, otherwise Postilion doesn't pick up the full MAC
            // However when Postilion replies, it packs the MAC in binary fixed 8
            if (unpacking && (field == Bit._064_MAC || field == Bit._128_MAC))
            {
                return new Field(field, FieldDescriptor.BinaryFixed(8));
            }

            return base.CreateField(field);
        }

        public Iso8583TermApp(byte[] data)
        {
            Unpack(data, 0);
        }

        public AdditionalData AdditionalData
        {
            get
            {
                if (this.IsFieldSet(Iso8583Rev93.Bit._048_PRIVATE_ADDITIONAL_DATA))
                {
                    return (AdditionalData)GetField(Bit._048_PRIVATE_ADDITIONAL_DATA);
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    this.ClearField(48);
                }
                this.fields.Add(48, value);
                this.bitmap[48] = true;
            }
        }

        public HashtableMessage StructuredData
        {
            get
            {
                if (!IsFieldSet(Bit._048_PRIVATE_ADDITIONAL_DATA))
                    return null;
                if (!AdditionalData.ContainsKey(AdditionalData.Field.StructuredData))
                    return null;

                HashtableMessage sd = new HashtableMessage();
                sd.FromMessageString(AdditionalData[AdditionalData.Field.StructuredData]);

                return sd;
            }
            set
            {
                var sd = value.ToMessageString();

                if (AdditionalData == null)
                {
                    AdditionalData = new AdditionalData();
                }

                AdditionalData[AdditionalData.Field.StructuredData] = sd;
            }
        }

        public override byte[] ToMsg()
        {
            var superMsg = base.ToMsg();
            var msg = new byte[superMsg.Length + 1];

            if (ascii)
            {
                msg[0] = (byte)'A';
            }
            else
            {
                msg[0] = (byte)'B';
            }

            Array.Copy(superMsg, 0, msg, 1, superMsg.Length);

            return msg;
        }

        public override int Unpack(byte[] msg, int startingOffset)
        {
            ascii = msg[0] == 'A';
            if (msg[0] == 'A' || msg[0] == 'B')
            {
                startingOffset++;
            }

            if (ascii)
            {
                Template = asciiTemplate;
            }
            else
            {
                Template = binaryTemplate;
            }
            this.bitmap.Formatter = Template.BitmapFormatter;

            try
            {
                // This is retarded.  Bad TermApp.Iso.  Look at create field for why I'm doing this
                unpacking = true;
                return base.Unpack(msg, startingOffset);
            }
            finally
            {
                unpacking = false;
            }
        }
    }
}
