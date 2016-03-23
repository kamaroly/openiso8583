// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VariableFieldTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Summary description for VariableFieldTests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenIso8583Net.Exceptions;
    using OpenIso8583Net.FieldValidator;
    using OpenIso8583Net.Formatter;
    using OpenIso8583Net.LengthFormatters;

    /// <summary>
    /// Variable Field Tests
    /// </summary>
    [TestClass]
    public class VariableFieldTests
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test pan field.
        /// </summary>
        [TestMethod]
        public void PackedLength()
        {
            var field = Field.AsciiVar(2, 2, 19, FieldValidators.N);
            field.Value = "58889212344567886";
            Assert.AreEqual(19, field.PackedLength);
        }

        /// <summary>
        /// The test variable field implements validator pack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldImplementsValidatorPack()
        {
            // Going to create a numeric field and assign valid length but invalid data to it
            // We're only testing that it implements the validator.  All validators are checked
            // in the various tests for them
            var f = new Field(2, FieldDescriptor.AsciiVar(2, 7, FieldValidators.N));
            f.Value = "12345a";
            try
            {
                f.ToMsg();
                Assert.Fail("Expected FieldFormatException");
            }
            catch (FieldFormatException)
            {
            }
        }

        /// <summary>
        /// The test variable field implements validator unpack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldImplementsValidatorUnpack()
        {
            var data = Encoding.ASCII.GetBytes("0612345a");
            var f = new Field(2, FieldDescriptor.AsciiVar(2, 7, FieldValidators.N));
            try
            {
                f.Unpack(data, 0);
                Assert.Fail("Expected FieldFormatException");
            }
            catch (FieldFormatException)
            {
            }
        }

        /// <summary>
        /// The test variable field pack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldPack()
        {
            var field = new Field(2, FieldDescriptor.AsciiVar(2, 20, FieldValidators.Ans));
            field.Value = "Hello dear bobbit";
            const string Expected = "17Hello dear bobbit";

            var msg = field.ToMsg();
            var actual = Encoding.ASCII.GetString(msg);

            Assert.AreEqual(Expected, actual);
        }

        /// <summary>
        /// The test variable field too long pack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldTooLongPack()
        {
            var field = new Field(2, FieldDescriptor.AsciiVar(2, 16, FieldValidators.Ans));
            field.Value = "Hello dear bobbit";

            try
            {
                field.ToMsg();
                Assert.Fail("Expecting FieldLengthException");
            }
            catch (FieldLengthException)
            {
            }
        }

        /// <summary>
        /// The test variable field too long unpack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldTooLongUnpack()
        {
            var msg = Encoding.ASCII.GetBytes("05hello");
            var field = new Field(2, FieldDescriptor.AsciiVar(2, 4, FieldValidators.Ans));

            try
            {
                field.Unpack(msg, 0);
                Assert.Fail("Expected FieldLengthException");
            }
            catch (FieldLengthException)
            {
            }
        }

        /// <summary>
        /// The test variable field unpack.
        /// </summary>
        [TestMethod]
        public void TestVariableFieldUnpack()
        {
            var field = new Field(2, FieldDescriptor.AsciiVar(2, 20, FieldValidators.Ans));
            var msg = Encoding.ASCII.GetBytes("xxxxx17Hello dear bobbityyyyyyy");

            var offset = field.Unpack(msg, 5);
            Assert.AreEqual(24, offset, "Offset expected to be 24");
            Assert.AreEqual("Hello dear bobbit", field.Value, "Expected value 'Hello dear bobbit'");
        }

        #endregion
    }
}