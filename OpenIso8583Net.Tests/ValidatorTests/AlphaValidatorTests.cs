using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    /// <summary>
    /// Summary description for AlphaTests
    /// </summary>
    [TestClass]
    public class AlphaValidatorTests : BaseValidatorTests
    {
        public AlphaValidatorTests()
            : base(OpenIso8583Net.FieldValidator.FieldValidators.Alpha)
        {
            ValidValues.Add("abcdef");
            ValidValues.Add("ABCDEF");

            InvalidValues.Add("adsf234");
            InvalidValues.Add("1324234");
            InvalidValues.Add(".,?#'");
            InvalidValues.Add(" ");
            InvalidValues.Add("adsf fasdf");
        }

        [TestMethod]
        public void TestAlphaValidValues()
        {
            TestValidValues();
        }

        [TestMethod]
        public void TestAlphaInvalidValues()
        {
            TestInvalidValues();
        }

    }
}
