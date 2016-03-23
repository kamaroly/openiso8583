// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalAmountTest.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   This is a test class for AdditionalAmountTest and is intended to contain all AdditionalAmountTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for AdditionalAmountTest and is intended to contain all AdditionalAmountTest Unit Tests
    /// </summary>
    [TestClass]
    public class AdditionalAmountTest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test amount pads.
        /// </summary>
        [TestMethod]
        public void TestAmountPads()
        {
            var amount = new AdditionalAmount { Amount = "200" };
            Assert.AreEqual("000000000200", amount.Amount);
        }

        /// <summary>
        /// The test constructor too long.
        /// </summary>
        [TestMethod]
        public void TestConstructorTooLong()
        {
            const string Input = "1001840C0000000220000";
            try
            {
                new AdditionalAmount(Input);
                Assert.Fail("Expected ArgumentException");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// The test constructor too short.
        /// </summary>
        [TestMethod]
        public void TestConstructorTooShort()
        {
            const string Input = "1001840C00000002200";
            try
            {
                new AdditionalAmount(Input);
                Assert.Fail("Expected ArgumentException");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// The test to string.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            var amount = new AdditionalAmount
                {
                   AccountType = "10", Amount = "200", AmountType = "01", CurrencyCode = "840", Sign = "C" 
                };
            var actual = amount.ToString();
            const string Expected = "1001840C000000000200";
            Assert.AreEqual(Expected, actual);
        }

        /// <summary>
        /// The test valid constructor.
        /// </summary>
        [TestMethod]
        public void TestValidConstructor()
        {
            const string Input = "1001840C000000022000";
            var amount = new AdditionalAmount(Input);
            Assert.AreEqual("10", amount.AccountType, "AccountType");
            Assert.AreEqual("01", amount.AmountType, "AmountType");
            Assert.AreEqual("840", amount.CurrencyCode, "CurrencyCode");
            Assert.AreEqual("C", amount.Sign, "Sign");
            Assert.AreEqual("000000022000", amount.Amount, "Amount");
            Assert.AreEqual(22000, amount.Value, "Value");
        }

        /// <summary>
        /// The test value negative.
        /// </summary>
        [TestMethod]
        public void TestValueNegative()
        {
            var amount = new AdditionalAmount();
            amount.Sign = "D";
            amount.Amount = "000002000000";
            const long Expected = -2000000;
            var actual = amount.Value;
            Assert.AreEqual(Expected, actual);
        }

        /// <summary>
        /// The test value positive.
        /// </summary>
        [TestMethod]
        public void TestValuePositive()
        {
            var amount = new AdditionalAmount();
            amount.Sign = "C";
            amount.Amount = "000002000000";
            const long Expected = 2000000;
            var actual = amount.Value;
            Assert.AreEqual(Expected, actual);
        }

        /// <summary>
        /// The test value propagates negative.
        /// </summary>
        [TestMethod]
        public void TestValuePropagatesNegative()
        {
            var amount = new AdditionalAmount { Value = -2245 };
            Assert.AreEqual("D", amount.Sign);
            Assert.AreEqual("000000002245", amount.Amount);
        }

        /// <summary>
        /// The test value propagates positive.
        /// </summary>
        [TestMethod]
        public void TestValuePropagatesPositive()
        {
            var amount = new AdditionalAmount { Value = 2245 };
            Assert.AreEqual("C", amount.Sign);
            Assert.AreEqual("000000002245", amount.Amount);
        }

        #endregion
    }
}