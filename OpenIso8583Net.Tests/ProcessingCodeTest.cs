// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingCodeTest.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   This is a test class for ProcessingCodeTest and is intended to contain all ProcessingCodeTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for ProcessingCodeTest and is intended to contain all ProcessingCodeTest Unit Tests
    /// </summary>
    [TestClass]
    public class ProcessingCodeTest
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test data too long.
        /// </summary>
        [TestMethod]
        public void TestDataTooLong()
        {
            const string Data = "1234567";
            try
            {
                new ProcessingCode(Data);
                Assert.Fail("Failed length processing");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// The test data too short.
        /// </summary>
        [TestMethod]
        public void TestDataTooShort()
        {
            const string Data = "12345";
            try
            {
                new ProcessingCode(Data);
                Assert.Fail("Failed length processing");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// The test valid constructor.
        /// </summary>
        [TestMethod]
        public void TestValidConstructor()
        {
            const string Data = "112233";
            var proc = new ProcessingCode(Data);
            Assert.AreEqual("11", proc.TranType);
            Assert.AreEqual("22", proc.FromAccountType);
            Assert.AreEqual("33", proc.ToAccountType);
        }

        #endregion
    }
}