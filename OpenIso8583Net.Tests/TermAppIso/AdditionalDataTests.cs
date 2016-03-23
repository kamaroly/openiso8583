using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenIso8583Net.TermAppIso;
using OpenIso8583Net.Formatter;

namespace OpenIso8583Net.Tests.TermAppIso
{
    [TestClass]
    public class AdditionalDataTests
    {
        [TestMethod]
        public void testPack()
        {
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.PosData, "2020202012345601234");
            byte[] actual = addData.ToMsg();
            byte[] expected = { 0x30, 0x30, 0x32, 0x34, (byte) 0xf0, 0x00, 0x21, (byte) 0x80, 0x00, 0x32, 0x30, 0x32, 0x30,
				0x32, 0x30, 0x32, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x30, 0x31, 0x32, 0x33, 0x34 };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testLength()
        {
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.PosData, "2020202012345601234");
            Assert.AreEqual(28, addData.PackedLength);
        }

        [TestMethod]
        public void testAddDataInMsg()
        {
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.PosData, "2020202012345601234");
            Iso8583TermApp msg = new Iso8583TermApp();
            msg.AdditionalData = addData;
            var actual = Formatters.Binary.GetString(msg.ToMsg());
            var expected = Formatters.Binary.GetString(new byte[] { 0x42, 0x30, 0x30, 0x30, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x30, 0x30,
				0x32, 0x34, (byte) 0xf0, 0x00, 0x21, (byte) 0x80, 0x00, 0x32, 0x30, 0x32, 0x30, 0x32, 0x30, 0x32, 0x30,
				0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x30, 0x31, 0x32, 0x33, 0x34 });
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testAddNotData()
        {
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.AddNodeData, "Nwrk");
            addData.ToMsg();
        }

        [TestMethod]
        public void testPosData()
        {
            AdditionalData data = new AdditionalData();
            data.Add(AdditionalData.Field.PosData, "123456");
            String actual = data.ToString("    ");
            String expected = "    [Additional Data     ] 048.001 [123456]";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testTwoFields()
        {
            var newline = Environment.NewLine;
            AdditionalData data = new AdditionalData();
            data.Add(AdditionalData.Field.PosData, "123456");
            data.Add(AdditionalData.Field.BankDetails, "654321");
            String actual = data.ToString("    ");
            String expected = "    [Additional Data     ] 048.001 [123456]" + newline
                    + "    [Additional Data     ] 048.013 [654321]";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testUnpack()
        {
            AdditionalData addData = new AdditionalData();
            byte[] msgData = Formatters.Binary.GetBytes("30313433f0008cc20132303230323032303030303034373838383838303054616953726320202020202053696d536e6b202020202020303030303437303030303437446562697447726f7570202030303635323138506f7374696c696f6e3a4d657461446174613234313233355465726d4170702e49534f3a5265636f6e63696c696174696f6e496e64696361746f72313131");
            int offset = addData.Unpack(msgData, 0);
            Assert.AreEqual(msgData.Length, offset);
            Assert.AreEqual("2020202000004788888", addData[AdditionalData.Field.PosData]);
        }

        [TestMethod]
        public void testAdminMessageUnpack()
        {
            String msgStr = "4231363134723000100AC58000313632303038333831323334353637383930393130303030303030303030303030303030303132303039303330333030303031373134303132303131303330333133313032393030303030303030333530333030303838373737373132383837373737312020202020202020303638303034303443303030303030303030303030303030304430303030303030303030303031343034433030303030303030303030303030303044303030303030303030303030303738F0004BD20038383737373731323030303031372020202020313135303031546169537263202020202020416662536E6B20202020202030303030313730303332343341666247726F757020202020343034";
            byte[] msgData = Formatters.Binary.GetBytes(msgStr);
            Iso8583TermApp msg = new Iso8583TermApp(msgData);
            Assert.IsTrue(msg.IsFieldSet(Iso8583TermApp.Bit._048_PRIVATE_ADDITIONAL_DATA));
            AdditionalData addData = msg.AdditionalData;
            Assert.IsNotNull(addData);
            Assert.AreEqual("5001", addData[AdditionalData.Field.ExtendedTranType]);
        }
    }
}
