namespace OpenIso8583Net.Tests.Emv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIso8583Net.Emv;
    using OpenIso8583Net;

    /// <summary>
    ///     Test sensitive information is masked in a .Tostring()
    /// </summary>
    [TestClass]
    public class TagMaskingTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Test pan masked.
        /// </summary>
        [TestMethod]
        public void TestApplPanMasked()
        {
            var tags = new EmvTags { { Tag.appl_pan, "1234567890123456".ToByteArray() } };
            var actual = tags.ToString();
            var expected = "0x5a   'appl_pan            ' = [****************]";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test track 2 masked.
        /// </summary>
        [TestMethod]
        public void TestTrack2Masked()
        {
            var tags = new EmvTags { { Tag.track2_eq_data, "4658545742527290d130520132101f".ToByteArray() } };
            var actual = tags.ToString();
            var expected = "0x57   'track2_eq_data      ' = [******************************]";
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}