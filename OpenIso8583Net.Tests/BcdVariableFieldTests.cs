// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BcdVariableFieldTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Summary description for BcdVariableField
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.FieldValidator;
    using OpenIso8583Net.Formatter;
    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// BCD Variable Field Tests
    /// </summary>
    [TestClass]
    public class BcdVariableFieldTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test pack.
        /// </summary>
        [TestMethod]
        public void TestPack()
        {
            var f = new Field(2, FieldDescriptor.BcdVar(2, 15, Formatters.Bcd));
            f.Value = "77";
            var actual = f.ToMsg();
            var expected = new byte[2];
            expected[0] = 0x01;
            expected[1] = 0x77;
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The unpack.
        /// </summary>
        [TestMethod]
        public void Unpack()
        {
            var f = new Field(
                2,
                FieldDescriptor.Create(
                    new VariableLengthFormatter(2, 15, Formatters.Bcd), FieldValidators.N, Formatters.Bcd));
            var msg = new byte[2];
            msg[0] = 0x02;
            msg[1] = 0x77;
            f.Unpack(msg, 0);
            var actual = f.Value;
            const string Expected = "77";
            Assert.AreEqual(Expected, actual);
        }

        #endregion
    }
}