using Serials.Common;
using System;
using System.Numerics;
using Xunit;

namespace SerialsTests.Common
{
    public class Base10EncoderTests
    {
        private readonly Base10Encoder _sut = new Base10Encoder();

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("10", 10)]
        [InlineData("35", 35)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 10)]
        [InlineData("01011", 1011)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Decode_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.Decode(input));
        }


        [Theory]
        [InlineData(35, "35")]
        [InlineData(10, "10")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        public void Encode_ReturnsProperString_WhenPassedValueBelow36(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);

            var result = _sut.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(36, "36")]
        [InlineData(665174629, "665174629")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerNumbers(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);

            var result = _sut.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encode_CanEncodeToAString_WhenPassedVeryLargeNumbers()
        {
            var big = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475438");

            var result = _sut.Encode(big);

            Assert.Equal("999349339222911192293394455594493392229339444955949119293848475438", result);
        }

        [Fact]
        public void Decode_CanDecodeToBigInteger_WhenPassedVeryLargeSerialNumber()
        {
            var expected = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475439");

            var decoded = _sut.Decode("999349339222911192293394455594493392229339444955949119293848475439");

            Assert.Equal(expected, decoded);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("10", 10)]
        [InlineData("35", 35)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 10)]
        [InlineData("01011", 1011)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecodeULong_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.DecodeULong(input));
        }

        [Theory]
        [InlineData(35, "35")]
        [InlineData(10, "10")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        public void Encode_ReturnsProperString_WhenPassedLongValueBelow36(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(36, "36")]
        [InlineData(665174629, "665174629")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerLongNumbers(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }
    }
}
