using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenIso8583Net.TermAppIso;

namespace OpenIso8583Net.Tests.TermAppIso
{
    [TestClass]
    public class StructuredDataTests
    {
        [TestMethod]
        public void testField48NoF16()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.PosData, "123456");
            msg.AdditionalData = addData;
            Assert.IsTrue(msg.IsFieldSet(Iso8583Rev93.Bit._048_PRIVATE_ADDITIONAL_DATA));
            Assert.IsNull(msg.StructuredData);
        }

        [TestMethod]
        public void testGetStructuredData()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            AdditionalData addData = new AdditionalData();
            addData.Add(AdditionalData.Field.StructuredData, "13PSI11V");
            msg.AdditionalData = addData;
            Assert.IsTrue(msg.IsFieldSet(Iso8583Rev93.Bit._048_PRIVATE_ADDITIONAL_DATA));
            Assert.IsNotNull(msg.StructuredData);
            Assert.AreEqual("V", msg.StructuredData["PSI"]);
        }

        [TestMethod]
        public void testNoField48()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            Assert.IsFalse(msg.IsFieldSet(Iso8583Rev93.Bit._048_PRIVATE_ADDITIONAL_DATA));
            Assert.IsNull(msg.StructuredData);
        }

        [TestMethod]
        public void testPutStructuredDataEmtpy()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            HashtableMessage sd = new HashtableMessage();
            sd["PSI"] = "V";
            msg.StructuredData = sd;
            AdditionalData addData = msg.AdditionalData;
            Assert.AreEqual("13PSI11V", addData[AdditionalData.Field.StructuredData]);
        }

        [TestMethod]
        public void testPutStructuredDataExistingAddData()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            AdditionalData addData = new AdditionalData();
            addData[AdditionalData.Field.PosData] = "FieldData";
            msg.AdditionalData = addData;
            HashtableMessage sd = new HashtableMessage();
            sd.Add("ABC", "1234");
            msg.StructuredData = sd;

            HashtableMessage checkSd = msg.StructuredData;
            Assert.AreNotSame(sd, checkSd);
            Assert.IsTrue(checkSd.ContainsKey("ABC"));

            AdditionalData checkData = msg.AdditionalData;
            Assert.AreEqual(checkData[AdditionalData.Field.PosData], "FieldData");
        }

        [TestMethod]
        public void testPutStructuredDataExistingSd()
        {
            Iso8583TermApp msg = new Iso8583TermApp();
            AdditionalData addData = new AdditionalData();
            addData[AdditionalData.Field.StructuredData] = "13PSI11V";
            msg.AdditionalData = addData;
            HashtableMessage sd = new HashtableMessage();
            sd.Add("ABC", "1234");
            msg.StructuredData = sd;

            HashtableMessage checkSd = msg.StructuredData;
            Assert.AreNotSame(sd, checkSd);
            Assert.IsTrue(checkSd.ContainsKey("ABC"));
            Assert.IsFalse(checkSd.ContainsKey("PSI"));
        }
    }
}
