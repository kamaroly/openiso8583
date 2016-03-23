// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Iso8583AsciiMsgTypeTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The iso 8583 ascii msg type tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.Tests.TestMessages;

    /// <summary>
    /// The ISO 8583 ascii msg type tests.
    /// </summary>
    [TestClass]
    public class Iso8583BinaryMsgTypeTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test pack unpack.
        /// </summary>
        [TestMethod]
        public void TestPackUnpack()
        {
            var msg = new IsoMsgBinaryMsgTypeFormatter();
            msg.MessageType = Iso8583.MsgType._0200_TRAN_REQ;
            msg[Iso8583.Bit._002_PAN] = "4005550000000001";
            msg[Iso8583.Bit._003_PROC_CODE] = "000000";

            byte[] data = msg.ToMsg();
            var unpackedMsg = new IsoMsgBinaryMsgTypeFormatter();
            unpackedMsg.Unpack(data, 0);
        }

        #endregion
    }
}