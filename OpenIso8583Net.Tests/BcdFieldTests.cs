// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BcdFieldTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   BCD Field Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.FieldValidator;
    using OpenIso8583Net.Formatter;
    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// BCD Field Tests
    /// </summary>
    [TestClass]
    public class BcdFieldTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The bcd unpack odd length field.
        /// </summary>
        [TestMethod]
        public void BcdUnpackOddLengthField()
        {
            var fd = FieldDescriptor.BcdFixed(3);
            byte[] msg = { 0x01, 0x23 };
            int newOffset;
            var fieldValue = fd.Unpack(2, msg, 0, out newOffset);
            Assert.AreEqual("123", fieldValue);
            Assert.AreEqual(2, newOffset);
        }

        /// <summary>
        /// The bcd unpack even length field.
        /// </summary>
        [TestMethod]
        public void BcdUnpackEvenLengthField()
        {
            var fd = FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.N, Formatters.Bcd);
            byte[] msg = { 0x01, 0x23 };
            int newOffset;
            var fieldValue = fd.Unpack(2, msg, 0, out newOffset);
            Assert.AreEqual("0123", fieldValue);
            Assert.AreEqual(2, newOffset);
        }

        #endregion
    }
}