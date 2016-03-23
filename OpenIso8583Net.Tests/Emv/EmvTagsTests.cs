namespace OpenIso8583Net.Tests.Emv
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIso8583Net.Emv;

    /// <summary>
    ///     Test EMV Tags
    /// </summary>
    [TestClass]
    public class EmvTagsTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The test add string.
        /// </summary>
        [TestMethod]
        public void TestAddBcd()
        {
            var tags = new EmvTags();
            tags.AddBcd(Tag.term_county_code, "710");
            byte[] actual = tags[Tag.term_county_code];
            byte[] expected = "0710".ToByteArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test Add String
        /// </summary>
        [TestMethod]
        public void TestAddString()
        {
            var str = "TestApp";
            var tags = new EmvTags();
            tags.AddString(Tag.appl_label, str);
            var expected = Encoding.ASCII.GetBytes(str);
            var actual = tags[Tag.appl_label];
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     The test get string.
        /// </summary>
        [TestMethod]
        public void TestGetBcd()
        {
            var tags = new EmvTags();
            tags.AddBcd(Tag.term_county_code, "710");
            string actual = tags.GetBcd(Tag.term_county_code);
            string expected = "0710";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test Get String
        /// </summary>
        [TestMethod]
        public void TestGetString()
        {
            var expected = "TestApp";
            var tags = new EmvTags();
            tags[Tag.appl_label] = Encoding.ASCII.GetBytes(expected);
            var actual = tags.GetString(Tag.appl_label);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     The test pack.
        /// </summary>
        [TestMethod]
        public void TestPack()
        {
            var tags = new EmvTags
                           {
                               { Tag.appl_id, "a0000000031010".ToByteArray() }, 
                               { Tag.tran_date, "100824".ToByteArray() }
                           };
            byte[] expected = "9f0607a00000000310109a03100824".ToByteArray();
            byte[] actual = tags.Pack();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test single item to string.
        /// </summary>
        [TestMethod]
        public void TestSingleItemToString()
        {
            var tags = new EmvTags();
            tags.AddBcd(Tag.tran_date, "100824");
            var expected = "0x9a   'tran_date           ' = [100824]";
            var actual = tags.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test two tags to string
        /// </summary>
        [TestMethod]
        public void TestTwoItemsToString()
        {
            var tags = new EmvTags();
            tags.AddBcd(Tag.tran_date, "100824");
            tags.AddBcd(Tag.term_county_code, "710");
            var expected = "0x9a   'tran_date           ' = [100824]"+Environment.NewLine +
                           "0x9f1a 'term_county_code    ' = [0710]";
            var actual = tags.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     The test unpack.
        /// </summary>
        [TestMethod]
        public void TestUnpack()
        {
            var msg = "9f0607a00000000310109a03100824".ToByteArray();
            var tags = EmvUtils.UnpackEmvTags(msg);
            Assert.AreEqual(2, tags.Count);
            var date = tags[Tag.tran_date];
            var appl = tags[Tag.appl_id];
            CollectionAssert.AreEqual("100824".ToByteArray(), date);
            CollectionAssert.AreEqual("a0000000031010".ToByteArray(), appl);
        }

        #endregion
    }
}