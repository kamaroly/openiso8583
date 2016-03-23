// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldDescriptorTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The field descriptor tests.
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
    /// The field descriptor tests.
    /// </summary>
    [TestClass]
    public class FieldDescriptorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test binary field must have hex validator.
        /// </summary>
        [TestMethod]
        public void TestBinaryFieldMustHaveHexValidator()
        {
            try
            {
                FieldDescriptor.Create(new FixedLengthFormatter(8), FieldValidators.None, Formatters.Binary);
                Assert.Fail("Binary formatter must have hex validator");
            }
            catch (FieldDescriptorException)
            {
            }
        }

        #endregion
    }
}