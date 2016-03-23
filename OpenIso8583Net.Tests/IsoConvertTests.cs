// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsoConvertTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Summary description for ConvertTests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///   IsoConvert tests
    /// </summary>
    [TestClass]
    public class IsoConvertTests
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The from int to msg type test.
        /// </summary>
        [TestMethod]
        public void FromIntToMsgTypeTest()
        {
            var res = IsoConvert.FromIntToMsgType(0x200);
            Assert.AreEqual("0200", res);
        }

        /// <summary>
        ///   The from msg type to int.
        /// </summary>
        [TestMethod]
        public void FromMsgTypeToInt()
        {
            var res = IsoConvert.FromMsgTypeToInt("0200");
            Assert.AreEqual(0x200, res);
        }

        #endregion
    }
}