using Serials.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace SerialsTests.Common
{
    public class Base36Tests
    {
        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("A", 10)]
        [InlineData("Z", 35)]
        public void Decode_ReturnsCorrectULong_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = Base36.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 36)]
        [InlineData("B01011", 665174629)]
        public void Decode_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = Base36.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ReturnsCorrectULong_RegardlessOfCapitalization()
        {
            BigInteger capitalResult = Base36.Decode("ABC");
            BigInteger lowerResult = Base36.Decode("abc");

            Assert.Equal(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Decode_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => Base36.Decode(input));
        }
        

        [Theory]
        [InlineData(35, "Z")]
        [InlineData(10, "A")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        public void Encode_ReturnsProperString_WhenPassedValueBelow36(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);
            var result = Base36.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(36, "10")]
        [InlineData(665174629, "B01011")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerNumbers(ulong input, string expected)
        {
            var bigInput = new BigInteger(input);
            var result = Base36.Encode(bigInput);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encode_CanEncodeToAString_WhenPassedVeryLargeNumbers()
        {
            var big = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475438");

            var result = Base36.Encode(big);

            Assert.Equal("4BCN8OTY5FAA7CN4TSGTXJ93D0XLNDXFS24MOK9MZOU", result);
        }

        [Fact]
        public void Decode_CanDecodeToBigInteger_WhenPassedVeryLargeSerialNumber()
        {
            var decoded = Base36.Decode("4BCN8OTY5FAA7CN4TSGTXJ93D0XLNDXFS24MOK9MZOV");

            var expected = BigInteger.Parse("999349339222911192293394455594493392229339444955949119293848475439");

            Assert.Equal(expected, decoded);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("A", 10)]
        [InlineData("Z", 35)]
        public void DecodeLong_ReturnsCorrectULong_WhenPassedASingleCharacter(string input, ulong expected)
        {
            BigInteger result = Base36.DecodeLong(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 36)]
        [InlineData("B01011", 665174629)]
        public void DecodeLong_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            BigInteger result = Base36.DecodeLong(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeLong_ReturnsCorrectULong_RegardlessOfCapitalization()
        {
            BigInteger capitalResult = Base36.DecodeLong("ABC");
            BigInteger lowerResult = Base36.DecodeLong("abc");

            Assert.Equal(capitalResult, lowerResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecodeLong_ThrowsArgumentException_WhenPassedEmptyOrNull(string input)
        {
            Assert.Throws<ArgumentException>(() => Base36.DecodeLong(input));
        }

        [Theory]
        [InlineData(35, "Z")]
        [InlineData(10, "A")]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        public void Encode_ReturnsProperString_WhenPassedLongValueBelow36(ulong input, string expected)
        {
            var result = Base36.Encode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(36, "10")]
        [InlineData(665174629, "B01011")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerLongNumbers(ulong input, string expected)
        {
            var result = Base36.Encode(input);

            Assert.Equal(expected, result);
        }
    }
}