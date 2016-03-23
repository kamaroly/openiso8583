// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestVariableLengthFormatter.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The test variable length formatter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests.LengthFormatters
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// The test variable length formatter.
    /// </summary>
    [TestClass]
    public class TestVariableLengthFormatter
    {
        #region Constants and Fields

        /// <summary>
        /// The _formatter.
        /// </summary>
        private readonly VariableLengthFormatter formatter = new VariableLengthFormatter(2, 12);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test length of field.
        /// </summary>
        [TestMethod]
        public void TestLengthOfField()
        {
            var data = new byte[14];
            data[7] = (byte)'1';
            data[8] = (byte)'0';
            var length = this.formatter.GetLengthOfField(data, 7);
            Assert.AreEqual(10, length);
        }

        /// <summary>
        /// The test length of length indicator.
        /// </summary>
        [TestMethod]
        public void TestLengthOfLengthIndicator()
        {
            Assert.AreEqual(2, this.formatter.LengthOfLengthIndicator);
        }

        /// <summary>
        /// The test pack length.
        /// </summary>
        [TestMethod]
        public void TestPackLength()
        {
            var data = new byte[14];
            var offset = this.formatter.Pack(data, 8, 2);
            Assert.AreEqual(4, offset);
            Assert.AreEqual((byte)'0', data[2]);
            Assert.AreEqual((byte)'8', data[3]);
        }

        /// <summary>
        /// The test validity.
        /// </summary>
        [TestMethod]
        public void TestValidity()
        {
            Assert.IsTrue(this.formatter.IsValidLength(0));
            Assert.IsTrue(this.formatter.IsValidLength(8));
            Assert.IsTrue(this.formatter.IsValidLength(12));
            Assert.IsFalse(this.formatter.IsValidLength(13));
        }

        #endregion
    }
}