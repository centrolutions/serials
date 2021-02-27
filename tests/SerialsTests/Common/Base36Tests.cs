using Serials.Common;
using System;
using System.Collections.Generic;
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
            ulong result = Base36.Decode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00", 0)]
        [InlineData("10", 36)]
        [InlineData("B01011", 665174629)]
        public void Decode_ReturnsCorrectULong_WhenPassedMultipleCharacterString(string input, ulong expected)
        {
            ulong result = Base36.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ReturnsCorrectULong_RegardlessOfCapitalization()
        {
            ulong capitalResult = Base36.Decode("ABC");
            ulong lowerResult = Base36.Decode("abc");

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
            var result = Base36.Encode(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(36, "10")]
        [InlineData(665174629, "B01011")]
        public void Encode_ReturnsEncodedString_WhenPassedLargerNumbers(ulong input, string expected)
        {
            var result = Base36.Encode(input);

            Assert.Equal(expected, result);
        }
    }
}