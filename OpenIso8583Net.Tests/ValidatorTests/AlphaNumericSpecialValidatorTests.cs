using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    /// <summary>
    ///   Summary description for AlphaNumericSpecialValidatorTests
    /// </summary>
    [TestClass]
    public class AlphaNumericSpecialValidatorTests : BaseValidatorTests
    {
        public AlphaNumericSpecialValidatorTests()
            : base(OpenIso8583Net.FieldValidator.FieldValidators.AlphaNumericSpecial)
        {
            ValidValues.Add("ab23cdef");
            ValidValues.Add("ABC23DEF");
            ValidValues.Add("adsf7346,.");
            ValidValues.Add("1324234");
            ValidValues.Add("ab23c def");
            ValidValues.Add(".,?#'");
        }

        [TestMethod]
        public void TestAlphaNumericSpecialValidValues()
        {
            TestValidValues();
        }

        [TestMethod]
        public void TestAlphaNumericSpecialInvalidValues()
        {
            TestInvalidValues();
        }
    }
}