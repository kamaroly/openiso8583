// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BcdFixedFieldTest.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   This is a test class for BcdFixedFieldTest and is intended to contain all BcdFixedFieldTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.Exceptions;
    using OpenIso8583Net.FieldValidator;
    using OpenIso8583Net.Formatter;
    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// This is a test class for BcdFixedFieldTest and is intended to contain all BcdFixedFieldTest Unit Tests
    /// </summary>
    [TestClass]
    public class BcdFixedFieldTest
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The pack test.
        /// </summary>
        [TestMethod]
        public void PackTest()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(2), FieldValidators.N, Formatters.Bcd));
            field.Value = "0012";
            var actual = field.ToMsg();
            var expected = new byte[] { 0x00, 0x12 };
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for PackedLength
        /// </summary>
        [TestMethod]
        public void PackedLengthTest()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.N, Formatters.Bcd));
            field.Value = "12345678";
            var actual = field.PackedLength;
            Assert.AreEqual(4, actual);
        }

        /// <summary>
        /// The test bcd has numeric validator.
        /// </summary>
        [TestMethod]
        public void TestBcdHasNumericValidator()
        {
            try
            {
                FieldDescriptor.Create(new FixedLengthFormatter(8), FieldValidators.Ans, Formatters.Bcd);
                Assert.Fail("Expected FieldDescriptorException");
            }
            catch (FieldDescriptorException)
            {
            }
        }

        /// <summary>
        /// A test for Unpack
        /// </summary>
        [TestMethod]
        public void UnpackTest()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.N, Formatters.Bcd));
            var msg = new byte[] { 0x00, 0x12 };
            field.Unpack(msg, 0);
            var actual = field.Value;
            const string Expected = "0012";
            Assert.AreEqual(Expected, actual);
        }

        #endregion
    }
}