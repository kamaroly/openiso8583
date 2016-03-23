using OpenIso8583Net.Exceptions;
using OpenIso8583Net.FieldValidator;
using OpenIso8583Net.LengthFormatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenIso8583Net.TermAppIso
{
    public class AdditionalData : Dictionary<AdditionalData.Field, string>, IField
    {
        public enum Field
        {
            PosData = 1, AuthProfile = 2, CardVerificationData = 3, ExtendedTranType = 4, AddNodeData = 5, InquiryRspData = 6,
            RoutingInfo = 7, CardholderInfo = 8, AddressVerificationData = 9, CardVerificationResult = 10,
            AddressVerificationResult = 11, RetentionData = 12, BankDetails = 13, PayeeNameAddress = 14, PayerAccIdentification = 15,
            StructuredData = 16
        }

        private static VariableLengthFormatter lenFormatter = new VariableLengthFormatter(4, 9999);

        private IFieldDescriptor GetFieldDescriptor(Field field)
        {
            switch (field)
            {
                case Field.PosData:
                    return FieldDescriptor.AsciiFixed(19, FieldValidators.Ans);
                case Field.AuthProfile:
                    return FieldDescriptor.AsciiFixed(2, FieldValidators.An);
                case Field.CardVerificationData:
                    return FieldDescriptor.AsciiFixed(4, FieldValidators.Ans);
                case Field.ExtendedTranType:
                    return FieldDescriptor.AsciiFixed(4, FieldValidators.N);
                case Field.AddNodeData:
                    return FieldDescriptor.AsciiVar(3, 255, FieldValidators.Ans);
                case Field.InquiryRspData:
                    return FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans);
                case Field.RoutingInfo:
                    return FieldDescriptor.AsciiFixed(48, FieldValidators.Ans);
                case Field.CardholderInfo:
                    return FieldDescriptor.AsciiVar(2, 50, FieldValidators.Ans);
                case Field.AddressVerificationData:
                    return FieldDescriptor.AsciiFixed(29, FieldValidators.Ans);
                case Field.CardVerificationResult:
                    return FieldDescriptor.AsciiFixed(1, FieldValidators.Ans);
                case Field.AddressVerificationResult:
                    return FieldDescriptor.AsciiFixed(1, FieldValidators.An);
                case Field.RetentionData:
                    return FieldDescriptor.AsciiVar(3, 999, FieldValidators.Ans);
                case Field.BankDetails:
                    return FieldDescriptor.AsciiFixed(31, FieldValidators.Ans);
                case Field.PayeeNameAddress:
                    return FieldDescriptor.AsciiFixed(253, FieldValidators.Ans);
                case Field.PayerAccIdentification:
                    return FieldDescriptor.AsciiVar(2, 28, FieldValidators.Ans);
                case Field.StructuredData:
                    return FieldDescriptor.AsciiVar(4, 9999, FieldValidators.Ans);
                default:
                    throw new UnknownFieldException("" + (int)field);
            }
        }

        private byte[] GenerateBitmap()
        {
            int bitmap = 0;
            foreach (var kvp in this)
            {
                int f = (int)kvp.Key;
                bitmap += (int)Math.Pow(2, 16 - f);
            }

            byte[] b = new byte[2];
            b[0] = (byte)(bitmap / 256);
            b[1] = (byte)(bitmap % 256);
            return b;
        }

        private List<Field> GetFieldsFrom(byte[] bitmap)
        {
            int bit = (bitmap[0] & 0xff) * 256 + (bitmap[1] & 0xff);
            var fields = new List<Field>();
            foreach (var field in Enum.GetValues(typeof(Field)).Cast<Field>())
            {
                int a = 0x10000 >> ((int)field);
                int b = bit & a;
                if (a == b)
                {
                    fields.Add(field);
                }
            }
            return fields;
        }

        public int MsgLength
        {
            get
            {
                int length = GenerateBitmap().Length;

                foreach (var kvp in this)
                {
                    IFieldDescriptor fieldDesc = GetFieldDescriptor(kvp.Key);
                    length += fieldDesc.GetPackedLength(kvp.Value);
                }
                return length;
            }
        }

        public int FieldNumber
        {
            get { return Iso8583TermApp.Bit._048_PRIVATE_ADDITIONAL_DATA; }
        }

        public int PackedLength
        {
            get { return MsgLength + lenFormatter.LengthOfLengthIndicator + 3; }
        }

        public string Value
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte[] ToMsg()
        {
            try
            {
                var msg = new byte[PackedLength];
                lenFormatter.Pack(msg, PackedLength - 4, 0);

                msg[4] = (byte)0xf0;
                msg[5] = 0x00;
                msg[6] = 0x21;
                int offset = 7;

                byte[] bitmap = GenerateBitmap();

                Array.Copy(bitmap, 0, msg, offset, bitmap.Length);
                offset += bitmap.Length;

                foreach (var kvp in this)
                {
                    IFieldDescriptor fieldDesc = GetFieldDescriptor(kvp.Key);
                    byte[] fieldData = fieldDesc.Pack((int)kvp.Key, kvp.Value);
                    Array.Copy(fieldData, 0, msg, offset, fieldData.Length);
                    offset += fieldData.Length;
                }

                return msg;
            }
            catch (FieldFormatException ffe)
            {
                throw new FieldFormatException("48.", ffe.FieldNumber, ffe.Message);
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix)
        {
            var sb = new StringBuilder();
            foreach (var kvp in this)
            {
                var fieldNr = "" + (int)kvp.Key;
                sb.Append(Environment.NewLine);
                sb.Append(prefix);
                sb.Append("[Additional Data     ] 048.");
                sb.Append(fieldNr.PadLeft(3, '0'));
                sb.Append(" [");
                sb.Append(kvp.Value);
                sb.Append("]");
            }

            int newlineLen = Environment.NewLine.Length;
            return sb.ToString().Substring(newlineLen);
        }

        public int Unpack(byte[] msg, int offset)
        {
            offset += 7; // 0xf0 + length
            byte[] bitmap = new byte[2];
            bitmap[0] = msg[offset];
            bitmap[1] = msg[offset + 1];
            offset += 2;

            List<Field> setFields = GetFieldsFrom(bitmap);
            foreach (var field in setFields)
            {
                IFieldDescriptor fieldDesc = GetFieldDescriptor(field);
                int newOffset;
                var data = fieldDesc.Unpack((int)field, msg, offset, out newOffset);
                this.Add(field, data);
                offset = newOffset;
            }

            return offset;
        }
    }
}
