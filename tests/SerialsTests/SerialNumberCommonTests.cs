using Serials;
using Serials.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SerialsTests
{
    public abstract class SerialNumberCommonTests<TSerialNumber, TNumber> where TSerialNumber: ISerialNumber
    {
        protected IEncoder _encoder;
        protected SerialNumberConfiguration _config;
        protected SerialNumberBase<TNumber> _sut;

        [Theory]
        [InlineData(1, "A12345C")]
        [InlineData(10, "A12345L")]
        public void IncreaseBy_IncrementsValue_WhenCalled(int increaseBy, string expected)
        {
            _sut.IncreaseBy(increaseBy);

            Assert.Equal(expected, _sut.ToString());
        }

        [Theory]
        [InlineData(2, "A123459")]
        [InlineData(20, "A12344R")]
        public void DecreaseBy_DecreasesValue_WhenCalled(int decreaseBy, string expected)
        {
            _sut.DecreaseBy(decreaseBy);

            Assert.Equal(expected, _sut.ToString());
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenTwoValuesAreEqual()
        {
            var serial = Activator.CreateInstance(typeof(TSerialNumber), _sut.NumericValue, _config);

            var result = _sut.Equals(serial);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValuesAreNotEqual()
        {
            var serial = Activator.CreateInstance(typeof(TSerialNumber), 1, _config);

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedValueIsNull()
        {
            TSerialNumber serial = default;

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValue_WhenBothSerialNumbersAreEqual()
        {
            var serial = Activator.CreateInstance(typeof(TSerialNumber),_sut.NumericValue, _config);
            var expected = serial.GetHashCode();

            var result = _sut.GetHashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToString_ReturnsProperLength_WhenMinimumLengthIsSetInConfiguration()
        {
            var config = new SerialNumberConfiguration() { MinimumLength = 10, PadCharacter = '0' };
            var serial = Activator.CreateInstance(typeof(TSerialNumber), 1, config);

            var result = serial.ToString();

            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void ToString_ReturnsUnpaddedString_WhenNoMinimumLengthIsSetInConfiguration()
        {
            var config = new SerialNumberConfiguration();
            var serial = Activator.CreateInstance(typeof(TSerialNumber), 1, config);

            var result = serial.ToString();

            Assert.Equal(1, result.Length);
        }
        
        [Fact]
        public void ToString_ReturnsNumberWithPrefix_WhenPrefixIsPassedInConfig()
        {
            var config = new SerialNumberConfiguration() { Prefix = "PRE-" };
            var serial = Activator.CreateInstance(typeof(TSerialNumber), 1, config);

            var result = serial.ToString();

            Assert.Equal("PRE-1", result);
        }
    }
}
