using System;

namespace OpenIso8583Net.NetworkHeaders
{
    /// <summary>
    /// A class implementing a two byte header
    /// </summary>
    public class TwoByteHeader : INetworkHeader
    {
        /// <summary>
        /// Get's the message length given the header
        /// </summary>
        /// <param name="header">byte array of the header</param>
        /// <param name="offset">The offset in the array to start parsing</param>
        /// <param name="newOffset">The offset in the array after the data has been parsed</param>
        /// <returns>Length of the message</returns>
        public int GetMessageLength(byte[] header, int offset, out int newOffset)
        {
            newOffset = HeaderLength;
            return header[offset] * 256 + header[offset + 1];
        }

        /// <summary>
        /// Packs a message into the network stream and appends the length header
        /// </summary>
        /// <param name="message">The message to pack</param>
        /// <returns>A byte array of the message ready to send</returns>
        public byte[] Pack(IMessage message)
        {
            var msg = message.ToMsg();
            var msgLength = msg.Length;
            var data = new byte[msgLength + HeaderLength];
            data[0] = (byte)(msgLength / 256);
            data[1] = (byte)(msgLength % 256);
            Array.Copy(msg, 0, data, HeaderLength, msgLength);
            return data;
        }

        /// <summary>
        /// The length of the length header
        /// </summary>
        public int HeaderLength
        {
            get { return 2; }
        }
    }
}