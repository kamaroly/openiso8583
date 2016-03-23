// --------------------------------------------------------------------------------------------------------------------
// <copyright company="John Oxley" file="BitmapTests.cs">
//   2012
// </copyright>
// <summary>
//   Bitmap Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenIso8583Net.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Bitmap tests
    /// </summary>
    [TestClass]
    public class BitmapTests
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Test the bitmap pack method with an extended bitmap
        /// </summary>
        [TestMethod]
        public void TestBitmapExtendedToMsg()
        {
            var bitmap = new Bitmap();
            bitmap[2] = true;
            bitmap[64] = true;
            bitmap[65] = true;
            bitmap[128] = true;
            var data = bitmap.ToMsg();
            var expected = new byte[data.Length];
            expected[0] = 192;
            expected[7] = 1;
            expected[8] = 128;
            expected[15] = 1;
            for (var i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(expected[i], data[i], "Error in field " + (i + 1));
            }
        }

        /// <summary>
        /// Test the bitmap pack method
        /// </summary>
        [TestMethod]
        public void TestBitmapToMsg()
        {
            var bitmap = new Bitmap();
            bitmap[2] = true;
            bitmap[64] = true;
            var data = bitmap.ToMsg();
            var expected = new byte[data.Length];
            expected[0] = 64;
            expected[7] = 1;
            var equal = true;
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] != expected[i])
                {
                    equal = false;
                }
            }

            Assert.IsTrue(equal);
        }

        /// <summary>
        /// Create a new bitmap and set a field under 64 to true. Check that the bitmap is not extended
        /// </summary>
        [TestMethod]
        public void TestNewBitmapNotExtended()
        {
            var bitmap = new Bitmap();
            bitmap[2] = true;
            bitmap[64] = true;
            Assert.IsFalse(bitmap.IsExtendedBitmap);
        }

        /// <summary>
        /// Create a new bitmap, set a field under 64 to true and one over 64 to true. Then set that guy to false. Check that the bitmap is NOT extended
        /// </summary>
        [TestMethod]
        public void TestNewBitmapThatIsExtendedAndThenUnextended()
        {
            var bitmap = new Bitmap();
            bitmap[2] = true;
            bitmap[64] = true;
            bitmap[65] = true;
            bitmap[65] = false;
            Assert.IsFalse(bitmap.IsExtendedBitmap);
        }

        /// <summary>
        /// Create a new bitmap, set a field under 64 to true and one over 64 to true. Check that the bitmap is extended
        /// </summary>
        [TestMethod]
        public void TestNewBitmapThenExtended()
        {
            var bitmap = new Bitmap();
            bitmap[2] = true;
            bitmap[64] = true;
            bitmap[65] = true;
            Assert.IsTrue(bitmap.IsExtendedBitmap);
        }

        /// <summary>
        /// The test unpack bitma extended.
        /// </summary>
        [TestMethod]
        public void TestUnpackBitmapExtended()
        {
            var bitmap = new Bitmap();
            var input = new byte[22];
            input[4] = 192;
            input[11] = 1;
            input[12] = 128;
            input[19] = 1;
            var offset = bitmap.Unpack(input, 4);
            Assert.AreEqual(20, offset, "Offset expected to be 20");
            Assert.AreEqual(true, bitmap[2], "Field 2 expected to be set");
            Assert.AreEqual(true, bitmap[64], "Field 64 expected to be set");
            Assert.AreEqual(true, bitmap[65], "Field 65 expected to be set");
            Assert.AreEqual(true, bitmap[128], "Field 128 expected to be set");
            Assert.AreEqual(true, bitmap.IsExtendedBitmap, "This is an extended bitmap");
            Assert.AreEqual(false, bitmap[63], "Field 63 expected to be off");
        }

        /// <summary>
        /// The test unpack bitmap not extended.
        /// </summary>
        [TestMethod]
        public void TestUnpackBitmapNotExtended()
        {
            var bitmap = new Bitmap();
            var input = new byte[16];
            input[4] = 64;
            input[11] = 1;
            var offset = bitmap.Unpack(input, 4);
            Assert.AreEqual(12, offset, "Offset expected to be 12");
            Assert.AreEqual(true, bitmap[2], "Field 2 expected to be set");
            Assert.AreEqual(true, bitmap[64], "Field 64 expected to be set");
            Assert.AreEqual(false, bitmap.IsExtendedBitmap, "This is not an extended bitmap");
            Assert.AreEqual(false, bitmap[63], "Field 63 expected to be off");
        }

        #endregion
    }
}