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
    }
}
