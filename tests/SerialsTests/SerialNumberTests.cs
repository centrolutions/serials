using Serials;
using Serials.Common;
using System;
using Xunit;

namespace SerialsTests
{
    public class SerialNumberTests
    {
        private readonly IEncoder _encoder;
        private readonly SerialNumberConfiguration _config;
        private readonly SerialNumber _sut;

        public SerialNumberTests()
        {
            _encoder = new Base36Encoder();
            _config = new SerialNumberConfiguration(_encoder);
            _sut = new SerialNumber("A12345B", _config);
        }

        [Fact]
        public void Constructor_InitializedWithString_SetsNumericValue()
        {
            var initialValue = "N4434";
            var expected = _encoder.Decode(initialValue);
            var sut = new SerialNumber(initialValue, _config);

            Assert.Equal(expected, sut.NumericValue);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenInvalidCharactersArePassed()
        {
            Assert.Throws<ArgumentException>(() => new SerialNumber("ABC-123", _config));
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
            var serial = new SerialNumber(_sut.NumericValue, _config);

            var result = _sut.Equals(serial);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValuesAreNotEqual()
        {
            var serial = new SerialNumber(1, _config);

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedValueIsNull()
        {
            SerialNumber serial = null;

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValue_WhenBothSerialNumbersAreEqual()
        {
            var serial = new SerialNumber(_sut.NumericValue, _config);
            var expected = serial.GetHashCode();

            var result = _sut.GetHashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToString_ReturnsBase36String_WhenCalled()
        {
            var expected = _encoder.Encode(_sut.NumericValue);

            Assert.Equal(expected, _sut.ToString());
        }
    }
}
