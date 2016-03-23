using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenIso8583Net.FieldValidator;

namespace OpenIso8583Net.Tests.ValidatorTests
{
    public abstract class BaseValidatorTests
    {
        protected IFieldValidator FieldValidator;
        protected List<string> InvalidValues;
        protected List<string> ValidValues;

        protected BaseValidatorTests(IFieldValidator fieldValidator)
        {
            FieldValidator = fieldValidator;
            ValidValues = new List<string>();
            InvalidValues = new List<string>();
        }

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        public void TestValidValues()
        {
            foreach (var value in ValidValues)
            {
                var actual = FieldValidator.IsValid(value);
                Assert.AreEqual(true, actual, value + " is a valid value");
            }
        }

        public void TestInvalidValues()
        {
            foreach (var value in InvalidValues)
            {
                var actual = FieldValidator.IsValid(value);
                Assert.AreEqual(false, actual, value + " is an invalid value");
            }
        }
    }
}