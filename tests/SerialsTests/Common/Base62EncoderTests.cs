using Serials.Common;
using System;
using System.Numerics;
using Xunit;

namespace SerialsTests.Common
{
    public class Base62EncoderTests
    {
        private readonly Base62Encoder _sut = new Base62Encoder();

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("A", 10)]
        [InlineData("Z", 35)]
        [InlineData("a", 36)]
        [InlineData("z", 61)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("1R", 89)]
        [InlineData("j10J3", 665174629)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ReturnsDifferentBigInteger_BecauseOfCapitalization()
        {
            BigInteger capitalResult = _sut.Decode("ABC");
            BigInteger lowerResult = _sut.Decode("abc");

            Assert.NotEqual(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Decode_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.Decode(input));
        }


        [Theory]
        [InlineData(35, "Z")]
        [InlineData(10, "A")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(36, "a")]
        [InlineData(61, "z")]
        public void Encode_ReturnsProperString_WhenPassedValueBelow62(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);

            var result = _sut.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(62, "10")]
        [InlineData(665174629, "j10J3")]
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

            Assert.Equal("Tl378UIZHzrJsVeygqelLiLyJceGHOxAHG2P8", result);
        }

        [Fact]
        public void Decode_CanDecodeToBigInteger_WhenPassedVeryLargeSerialNumber()
        {
            var expected = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475439");

            var decoded = _sut.Decode("Tl378UIZHzrJsVeygqelLiLyJceGHOxAHG2P9");

            Assert.Equal(expected, decoded);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("A", 10)]
        [InlineData("Z", 35)]
        [InlineData("a", 36)]
        [InlineData("z", 61)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 62)]
        [InlineData("j10J3", 665174629)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeULong_ReturnsDifferentULong_BecauseOfCapitalization()
        {
            BigInteger capitalResult = _sut.DecodeULong("ABC");
            BigInteger lowerResult = _sut.DecodeULong("abc");

            Assert.NotEqual(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecodeULong_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.DecodeULong(input));
        }

        [Theory]
        [InlineData(61, "z")]
        [InlineData(36, "a")]
        [InlineData(35, "Z")]
        [InlineData(10, "A")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        public void Encode_ReturnsProperString_WhenPassedLongValueBelow36(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(62, "10")]
        [InlineData(665174629, "j10J3")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerLongNumbers(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }
    }
}
