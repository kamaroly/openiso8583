namespace OpenIso8583Net
{
    /// <summary>
    ///   Interface representing a message that can be send over the network
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///   Gets the message as a byte array ready to send over the network
        /// </summary>
        /// <returns>byte[] representing the message</returns>
        byte[] ToMsg();

        /// <summary>
        ///   Unpacks the message from a byte array
        /// </summary>
        /// <param name = "msg">message data to unpack</param>
        /// <param name = "startingOffset">the offset in the array to start</param>
        /// <returns>the offset in the array representing the start of the next message</returns>
        int Unpack(byte[] msg, int startingOffset);
    }
}