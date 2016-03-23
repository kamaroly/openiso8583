// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryFixedFieldTests.cs" company="John Oxley">
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
    /// Fixed field tests
    /// </summary>
    [TestClass]
    public class BinaryFixedFieldTests
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The test binary fixed field correct length pack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldCorrectLengthPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(3), FieldValidators.Hex, Formatters.Binary));
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
        /// The test binary fixed field implements validator pack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldImplementsValidatorPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(3), FieldValidators.Hex, Formatters.Binary));
            f.Value = "abcdr5";
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
        /// The test binary fixed field pack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldPack()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.Hex, Formatters.Binary));
            field.Value = "31323334";
            var expected = Encoding.ASCII.GetBytes("1234");

            var msg = field.ToMsg();
            CollectionAssert.AreEqual(expected, msg);
        }

        /// <summary>
        /// The test binary fixed field packed length.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldPackedLength()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.Hex, Formatters.Binary));
            f.Value = "12345678";
            Assert.AreEqual(4, f.PackedLength);
        }

        /// <summary>
        /// The test binary fixed field too long pack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldTooLongPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(3), FieldValidators.Hex, Formatters.Binary));
            f.Value = "12345678";
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
        /// The test binary fixed field too short pack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldTooShortPack()
        {
            var f = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(4), FieldValidators.Hex, Formatters.Binary));
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
        /// The test binary fixed field unpack.
        /// </summary>
        [TestMethod]
        public void TestBinaryFixedFieldUnpack()
        {
            var field = new Field(
                2, FieldDescriptor.Create(new FixedLengthFormatter(2), FieldValidators.Hex, Formatters.Binary));
            var msg = new byte[8];
            msg[0] = 65;
            msg[1] = 65;
            msg[2] = 65;
            msg[3] = 65;
            msg[4] = 65;
            msg[5] = 65;
            msg[6] = 65;
            msg[7] = 65;

            var offset = field.Unpack(msg, 2);
            Assert.AreEqual(4, offset, "Offset expected to be 4");
            Assert.AreEqual("4141", field.Value);
        }

        #endregion
    }
}