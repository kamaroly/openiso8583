// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestFixedLengthFormatter.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The test fixed length formatter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests.LengthFormatters
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// The test fixed length formatter.
    /// </summary>
    [TestClass]
    public class TestFixedLengthFormatter
    {
        #region Constants and Fields

        /// <summary>
        /// The _formatter.
        /// </summary>
        private readonly FixedLengthFormatter formatter = new FixedLengthFormatter(8);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test length of field.
        /// </summary>
        [TestMethod]
        public void TestLengthOfField()
        {
            var data = new byte[14];
            var length = this.formatter.GetLengthOfField(data, 7);
            Assert.AreEqual(8, length);
        }

        /// <summary>
        /// The test length of length indicator.
        /// </summary>
        [TestMethod]
        public void TestLengthOfLengthIndicator()
        {
            Assert.AreEqual(0, this.formatter.LengthOfLengthIndicator);
        }

        /// <summary>
        /// The test pack length.
        /// </summary>
        [TestMethod]
        public void TestPackLength()
        {
            var data = new byte[4];
            var offset = this.formatter.Pack(data, 8, 2);
            Assert.AreEqual(2, offset);
            CollectionAssert.AreEqual(new byte[4], data);
        }

        /// <summary>
        /// The test validity.
        /// </summary>
        [TestMethod]
        public void TestValidity()
        {
            Assert.IsFalse(this.formatter.IsValidLength(0));
            Assert.IsFalse(this.formatter.IsValidLength(7));
            Assert.IsFalse(this.formatter.IsValidLength(9));
            Assert.IsTrue(this.formatter.IsValidLength(8));
        }

        #endregion
    }
}