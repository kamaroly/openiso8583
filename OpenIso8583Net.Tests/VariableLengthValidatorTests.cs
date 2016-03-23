// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VariableLengthValidatorTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Variable Length Validator Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.LengthValidators;

    /// <summary>
    /// Variable Length Validator Tests
    /// </summary>
    [TestClass]
    public class VariableLengthValidatorTests
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test variable length validator correct.
        /// </summary>
        [TestMethod]
        public void TestVariableLengthValidatorCorrect()
        {
            var val = new VariableLengthValidator(5, 7);
            var actual = val.IsValid("12345");
            Assert.AreEqual(true, actual);
            actual = val.IsValid("1234567");
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// The test variable length validator too long.
        /// </summary>
        [TestMethod]
        public void TestVariableLengthValidatorTooLong()
        {
            var val = new VariableLengthValidator(5, 7);
            var actual = val.IsValid("12345678");
            Assert.AreEqual(false, actual);
        }

        /// <summary>
        /// The test variable length validator too short.
        /// </summary>
        [TestMethod]
        public void TestVariableLengthValidatorTooShort()
        {
            var val = new VariableLengthValidator(5, 7);
            var actual = val.IsValid("1234");
            Assert.AreEqual(false, actual);
        }

        #endregion
    }
}