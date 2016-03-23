// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedFieldTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Summary description for FixedFieldTests
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
    /// Fixed Field Tests
    /// </summary>
    [TestClass]
    public class FixedFieldTests
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test fixed field correct length pack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldCorrectLengthPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.Ans, Formatters.Ascii));
            f.Value = "123456";
            try
            {
                f.ToMsg();
            }
            catch (FieldLengthException)
            {
                Assert.Fail("Did not expect FieldLengthException");
            }
        }

        /// <summary>
        /// The test fixed field implements validator pack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldImplementsValidatorPack()
        {
            // Going to create a numeric field and assign valid length but invalid data to it
            // We're only testing that it implements the validator.  All validators are checked
            // in the various tests for them
            var f = new Field(2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.N, Formatters.Ascii));
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
        /// The test fixed field implements validator unpack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldImplementsValidatorUnpack()
        {
            // have a look at the comment inside TestFixedFieldImplementsValidatorPack
            var data = Encoding.ASCII.GetBytes("12345a");
            var f = new Field(2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.N, Formatters.Ascii));
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
        /// The test fixed field pack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldPack()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.Ans, Formatters.Ascii));
            field.Value = "AAAAAA";
            var expected = Encoding.ASCII.GetBytes("AAAAAA");

            var msg = field.ToMsg();
            var equal = true;
            for (var i = 0; i < msg.Length; i++)
            {
                if (expected[i] != msg[i])
                {
                    equal = false;
                }
            }

            Assert.IsTrue(equal);
        }

        /// <summary>
        /// The test fixed field too long pack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldTooLongPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.Ans, Formatters.Ascii));
            f.Value = "1234567";
            try
            {
                f.ToMsg();
                Assert.Fail("Expected FieldLengthException");
            }
            catch (FieldLengthException)
            {
            }
        }

        /// <summary>
        /// The test fixed field too short pack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldTooShortPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.Ans, Formatters.Ascii));
            f.Value = "12345";
            try
            {
                f.ToMsg();
                Assert.Fail("Expected FieldLengthException");
            }
            catch (FieldLengthException)
            {
            }
        }

        /// <summary>
        /// The test fixed field unpack.
        /// </summary>
        [TestMethod]
        public void TestFixedFieldUnpack()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(6), FieldValidators.A, Formatters.Ascii));
            var msg = new byte[10];
            msg[2] = 65;
            msg[3] = 65;
            msg[4] = 65;
            msg[5] = 65;
            msg[6] = 65;
            msg[7] = 65;

            var offset = field.Unpack(msg, 2);
            Assert.AreEqual(8, offset, "Offset expected to be 8");
            Assert.AreEqual("AAAAAA", field.Value, "Expected value AAAAAA");
        }

        #endregion
    }
}