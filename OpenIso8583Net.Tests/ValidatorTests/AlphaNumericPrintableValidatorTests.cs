using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    /// <summary>
    ///   Summary description for AlphaNumericPrintableValidatorTests
    /// </summary>
    [TestClass]
    public class AlphaNumericPrintableValidatorTests : BaseValidatorTests
    {
        public AlphaNumericPrintableValidatorTests()
            : base(OpenIso8583Net.FieldValidator.FieldValidators.AlphaNumericPrintable)
        {
            ValidValues.Add("ab23cdef");
            ValidValues.Add("ABC23DEF");
            ValidValues.Add("adsf7346,.");
            ValidValues.Add("1324234");
            ValidValues.Add("ab23c def");
            ValidValues.Add(".,?#'");
        }

        [TestMethod]
        public void TestAlphaNumericPrintableValidValues()
        {
            TestValidValues();
        }

        [TestMethod]
        public void TestAlphaNumericPrintableInvalidValues()
        {
            TestInvalidValues();
        }
    }
}