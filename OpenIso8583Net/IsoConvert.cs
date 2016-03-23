using System.Text;

namespace OpenIso8583Net
{
    using System;

    /// <summary>
    ///   Convert methods for the ISO message builder
    /// </summary>
    public static class IsoConvert
    {
        /// <summary>
        ///   Convert a message type into ASCII chars representing the string
        /// </summary>
        /// <param name = "value">Message type to convert</param>
        /// <returns>byte[] representing the message type</returns>
        public static byte[] FromIntToMsgTypeData(int value)
        {
            var data = new byte[4];
            for (var i = 3; i >= 0; i--)
            {
                data[i] = (byte)GetHexChar(value & 15);
                value >>= 4;
            }

            return data;
        }

        /// <summary>
        ///   Convert a byte[] of the MTID into the int value in base 16
        /// </summary>
        /// <param name = "data">MTID to convert</param>
        /// <returns>int representation of the message type identifier</returns>
        public static int FromMsgTypeDataToInt(byte[] data)
        {
            var val = 0;
            var len = data.Length;
            for (var i = 0; i < len; i++)
                val = val << 4 | GetHexNibble(data[i]);

            return val;
        }


        private static byte GetHexNibble(byte data)
        {
            if (data >= 48 && data <= 57)
                return (byte)(data & 15);
            if (data >= 65 && data <= 70)
                return (byte)(data - 55);
            if (data >= 97 && data <= 102)
                return (byte)(data - 87);

            return 0;
        }

        private static int GetHexChar(int nibble)
        {
            if (nibble < 10)
                return nibble + 48;

            return nibble + 55;
        }

        /// <summary>
        ///   Convert a message type into an ASCII string
        /// </summary>
        /// <param name = "value">Message type to convert</param>
        /// <returns>A string representing the message type</returns>
        public static string FromIntToMsgType(int value)
        {
            return Encoding.ASCII.GetString(FromIntToMsgTypeData(value));
        }

        /// <summary>
        ///   Convert a message type string, e.g. "0200" to the integer value, e.g. 0x200
        /// </summary>
        /// <param name = "msgType"></param>
        /// <returns></returns>
        public static int FromMsgTypeToInt(string msgType)
        {
            return FromMsgTypeDataToInt(Encoding.ASCII.GetBytes(msgType));
        }
    }
}