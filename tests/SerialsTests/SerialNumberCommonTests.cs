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

        [Fact]
        public void Constructor_InitializedWithString_SetsNumericValue()
        {
            var initialValue = "N4434";
            var expected = _encoder.Decode(initialValue);
            var sut = new SerialNumber(initialValue, _config);

            Assert.Equal(expected, sut.NumericValue);
        }

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
    }
}
