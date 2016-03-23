using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    /// <summary>
    ///   Summary description for Rev87AmountValidatorTests
    /// </summary>
    [TestClass]
    public class Rev87AmountValidatorTests : BaseValidatorTests
    {
        public Rev87AmountValidatorTests()
            : base(OpenIso8583Net.FieldValidator.FieldValidators.Rev87AmountValidator)
        {
            ValidValues.Add("C0002135");
            ValidValues.Add("D0002135");
            ValidValues.Add("C000002135");
            ValidValues.Add("D000002135");

            InvalidValues.Add("ABCDEF");
            InvalidValues.Add("abcdef");
            InvalidValues.Add("123468dfc");
            InvalidValues.Add("123456");
            InvalidValues.Add(" ");
            InvalidValues.Add("123abcdefg");
            InvalidValues.Add("./'[]");
            InvalidValues.Add("\t");
            InvalidValues.Add("\n");
        }

        [TestMethod]
        public void TestRev87AmountValidatorValidValues()
        {
            TestValidValues();
        }

        [TestMethod]
        public void TestRev87AmountValidatorInvalidValues()
        {
            TestInvalidValues();
        }
    }
}