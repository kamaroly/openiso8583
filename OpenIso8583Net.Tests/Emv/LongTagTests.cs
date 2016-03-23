namespace OpenIso8583Net.Tests.Emv
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenIso8583Net.Emv;

    /// <summary>
    ///     Tests for long tags
    /// </summary>
    [TestClass]
    public class LongTagTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Test packing a long tag
        /// </summary>
        [TestMethod]
        public void TestPack()
        {
            var content =
                "9f02060000000010009f03060000000000004f07a0000000041010820258009f360200019f2608e4539ffffaa341db9f2701808e100000000000000000420341031e031f009f34034203009f10120212a0000f240000dac000000000000000ff9f3303e0f0c89f1a0202889f350122950500000480009f420208409a031106239f4104000000019c01009f37048202d9dc";
            var tags = new EmvTags { { Tag.icc_request, content.ToByteArray() } };
            var packed = tags.Pack();
            var actual = packed.ToHex().Substring(0, 12);
            var expected = "FF2081919F02";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test unpack a single long tag
        /// </summary>
        [TestMethod]
        public void TestUnpack()
        {
            var fieldData =
                "FF2081919F02060000000010009F03060000000000004F07A0000000041010820258009F360200019F2608E4539FFFFAA341DB9F2701808E100000000000000000420341031E031F009F34034203009F10120212A0000F240000DAC000000000000000FF9F3303E0F0C89F1A0202889F350122950500000480009F420208409A031106239F4104000000019C01009F37048202D9DC";
            var tagData = fieldData.ToByteArray();
            var tags = EmvUtils.UnpackEmvTags(tagData);
            Assert.AreEqual(1, tags.Count);
        }

        #endregion
    }
}