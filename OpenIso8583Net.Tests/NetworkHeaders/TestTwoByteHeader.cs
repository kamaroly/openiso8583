using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenIso8583Net.NetworkHeaders;

namespace OpenIso8583Net.Tests.NetworkHeaders
{
    [TestClass]
    public class TestTwoByteHeader
    {
        private Iso8583 _origMsg;
        private byte[] _packedData;
        private Iso8583 _unpackedMsg;

        [TestInitialize]
        public void Setup()
        {
            _origMsg = new Iso8583 { MessageType = Iso8583.MsgType._0200_TRAN_REQ };
            _origMsg[3] = "090000";
            var header = new TwoByteHeader();

            _packedData = header.Pack(_origMsg);

            var headerData = new byte[header.HeaderLength];
            Array.Copy(_packedData, 0, headerData, 0, header.HeaderLength);
            int newOffset;
            var msgLength = header.GetMessageLength(headerData, 0, out newOffset);
            _unpackedMsg = new Iso8583();

            // Doing this will test that get message length works correctly
            _unpackedMsg.Unpack(_packedData, _packedData.Length - msgLength);
        }

        [TestMethod]
        public void TestLength()
        {
            var actual = _packedData.Length;
            var expected = _origMsg.PackedLength + 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestField()
        {
            var expected = _origMsg[3];
            var actual = _unpackedMsg[3];
            Assert.AreEqual(expected, actual);
        }
    }
}