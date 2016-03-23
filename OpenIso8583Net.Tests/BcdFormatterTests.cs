// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BcdFormatterTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The bcd formatter tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.Formatter;

    /// <summary>
    /// The bcd formatter tests.
    /// </summary>
    [TestClass]
    public class BcdFormatterTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get bytes test.
        /// </summary>
        [TestMethod]
        public void GetBytesTest()
        {
            IFormatter target = new BcdFormatter();
            const string Value = "0245";
            var expected = new byte[2];
            expected[0] = 0x02;
            expected[1] = 0x45;
            var actual = target.GetBytes(Value);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The test packed length.
        /// </summary>
        [TestMethod]
        public void TestPackedLength()
        {
            IFormatter formatter = new BcdFormatter();
            var actual = formatter.GetPackedLength(8);
            Assert.AreEqual(4, actual);
        }

        /// <summary>
        /// The test packed length odd.
        /// </summary>
        [TestMethod]
        public void TestPackedLengthOdd()
        {
            var formatter = new BcdFormatter();
            Assert.AreEqual(1, formatter.GetPackedLength(1));
            Assert.AreEqual(2, formatter.GetPackedLength(3));
            Assert.AreEqual(3, formatter.GetPackedLength(5));
        }

        /// <summary>
        /// The test unpack.
        /// </summary>
        [TestMethod]
        public void TestUnpack()
        {
            IFormatter formatter = new BcdFormatter();
            var data = new byte[2];
            data[0] = 0x02;
            data[1] = 0x45;
            var actual = formatter.GetString(data);
            Assert.AreEqual("0245", actual);
        }

        #endregion
    }
}