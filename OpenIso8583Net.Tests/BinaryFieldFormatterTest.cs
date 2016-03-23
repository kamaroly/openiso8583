// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryFieldFormatterTest.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The binary field formatter test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.Formatter;

    /// <summary>
    /// The binary field formatter test.
    /// </summary>
    [TestClass]
    public class BinaryFieldFormatterTest
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get bytes test.
        /// </summary>
        [TestMethod]
        public void GetBytesTest()
        {
            IFormatter target = new BinaryFormatter();
            const string Value = "31323334";
            var expected = new byte[] { 0x31, 0x32, 0x33, 0x34 };
            var actual = target.GetBytes(Value);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The get string.
        /// </summary>
        [TestMethod]
        public void GetString()
        {
            IFormatter target = new BinaryFormatter();
            const string expected = "31323334";
            var input = new byte[] { 0x31, 0x32, 0x33, 0x34 };
            var actual = target.GetString(input);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The test packed length.
        /// </summary>
        [TestMethod]
        public void TestPackedLength()
        {
            IFormatter formatter = new BinaryFormatter();
            var actual = formatter.GetPackedLength(8);
            Assert.AreEqual(4, actual);
        }

        #endregion
    }
}