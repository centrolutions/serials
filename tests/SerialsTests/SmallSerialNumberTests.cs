using Serials;
using Serials.Common;
using System;
using Xunit;

namespace SerialsTests
{
    public class SmallSerialNumberTests
    {
        private readonly SmallSerialNumber _sut = new SmallSerialNumber("A12345B");

        [Fact]
        public void Constructor_InitializedWithString_SetsNumericValue()
        {
            var initialValue = "N4434";
            var expected = Base36.Decode(initialValue);
            var sut = new SmallSerialNumber(initialValue);

            Assert.Equal(expected, sut.NumericValue);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenInvalidCharactersArePassed()
        {
            Assert.Throws<ArgumentException>(() => new SmallSerialNumber("ABC-123"));
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
            var serial = new SmallSerialNumber(_sut.NumericValue);

            var result = _sut.Equals(serial);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValuesAreNotEqual()
        {
            var serial = new SmallSerialNumber(1);

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedValueIsNull()
        {
            SmallSerialNumber serial = null;

            var result = _sut.Equals(serial);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValue_WhenBothSmallSerialNumbersAreEqual()
        {
            var serial = new SmallSerialNumber(_sut.NumericValue);
            var expected = serial.GetHashCode();

            var result = _sut.GetHashCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToString_ReturnsBase36String_WhenCalled()
        {
            var expected = Base36.Encode(_sut.NumericValue);

            Assert.Equal(expected, _sut.ToString());
        }
    }
}
