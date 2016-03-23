using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    /// <summary>
    ///   Summary description for Track2ValidatorTests
    /// </summary>
    [TestClass]
    public class Track2ValidatorTests : BaseValidatorTests
    {
        public Track2ValidatorTests()
            : base(OpenIso8583Net.FieldValidator.FieldValidators.Track2)
        {
            // TODO add more invalid test cases for things like bad month and bad service restriction code
            ValidValues.Add("58889290122738116===13216843253657432");
            ValidValues.Add("58889290122738116=991250113216843253657432");
            ValidValues.Add("58889290122738116==50113216843253657432");
            ValidValues.Add("58889290122738116=9912=13216843253657432");
            ValidValues.Add("58889290122738116DDD13216843253657432");
            ValidValues.Add("58889290122738116D991250113216843253657432");
            ValidValues.Add("58889290122738116DD50113216843253657432");
            ValidValues.Add("58889290122738116D9912D13216843253657432");

            InvalidValues.Add("D0002135");
            InvalidValues.Add("C000002135");
            InvalidValues.Add("D000002135");
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
        public void TestTrack2ValidValues()
        {
            TestValidValues();
        }

        [TestMethod]
        public void TestTrack2InvalidValues()
        {
            TestInvalidValues();
        }
    }
}