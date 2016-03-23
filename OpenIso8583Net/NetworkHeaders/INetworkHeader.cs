namespace OpenIso8583Net.NetworkHeaders
{
    /// <summary>
    ///   Interface exposing functionality of a network header class
    /// </summary>
    public interface INetworkHeader
    {
        /// <summary>
        /// Get's the message length given the header
        /// </summary>
        /// <param name="header">byte array of the header</param>
        /// <param name="offset">The offset in the array to start parsing</param>
        /// <param name="newOffset">The offset in the array after the data has been parsed</param>
        /// <returns>Length of the message</returns>
        int GetMessageLength(byte[] header, int offset, out int newOffset);

        /// <summary>
        /// Packs a message into the network stream and appends the length header
        /// </summary>
        /// <param name="message">The message to pack</param>
        /// <returns>A byte array of the message ready to send</returns>
        byte[] Pack(IMessage message);

        /// <summary>
        /// The length of the length header
        /// </summary>
        int HeaderLength { get; }
    }
}