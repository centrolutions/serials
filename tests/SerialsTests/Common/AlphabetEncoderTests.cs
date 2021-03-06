using Serials.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace SerialsTests.Common
{
    public class AlphabetEncoderTests
    {
        private readonly AlphabetEncoder _sut = new AlphabetEncoder();

        [Theory]
        [InlineData("A", 0)]
        [InlineData("B", 1)]
        [InlineData("K", 10)]
        [InlineData("Z", 25)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AA", 0)]
        [InlineData("BA", 26)]
        [InlineData("ZABABB", 297052003)]
        public void Decode_ReturnsCorrectBigInteger_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ReturnsCorrectBigInteger_RegardlessOfCapitalization()
        {
            BigInteger capitalResult = _sut.Decode("ABC");
            BigInteger lowerResult = _sut.Decode("abc");

            Assert.Equal(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Decode_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.Decode(input));
        }


        [Theory]
        [InlineData(25, "Z")]
        [InlineData(0, "A")]
        [InlineData(1, "B")]
        public void Encode_ReturnsProperString_WhenPassedValueBelow26(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);

            var result = _sut.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(26, "BA")]
        [InlineData(665174629, "CDZPQDP")]
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

            Assert.Equal("IDURTFPLWMLOKNSQWSPJSUJARYERKCFWWDYKKESWNYVCCKC", result);
        }

        [Fact]
        public void Decode_CanDecodeToBigInteger_WhenPassedVeryLargeSerialNumber()
        {
            var expected = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475439");

            var decoded = _sut.Decode("IDURTFPLWMLOKNSQWSPJSUJARYERKCFWWDYKKESWNYVCCKD");

            Assert.Equal(expected, decoded);
        }

        [Theory]
        [InlineData("A", 0)]
        [InlineData("B", 1)]
        [InlineData("K", 10)]
        [InlineData("Z", 25)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AA", 0)]
        [InlineData("BA", 26)]
        [InlineData("ZABABB", 297052003)]
        public void DecodeULong_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = _sut.DecodeULong(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeULong_ReturnsCorrectULong_RegardlessOfCapitalization()
        {
            BigInteger capitalResult = _sut.DecodeULong("ABC");
            BigInteger lowerResult = _sut.DecodeULong("abc");

            Assert.Equal(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecodeULong_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => _sut.DecodeULong(input));
        }

        [Theory]
        [InlineData(25, "Z")]
        [InlineData(10, "K")]
        [InlineData(1, "B")]
        [InlineData(0, "A")]
        public void Encode_ReturnsProperString_WhenPassedLongValueBelow26(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(26, "BA")]
        [InlineData(297052003, "ZABABB")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerLongNumbers(ulong input, string expected)
        {
            var result = _sut.Encode(input);

            Assert.Equal(expected, result);
        }
    }
}
