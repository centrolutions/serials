using Serials;
using Serials.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SerialsTests
{
    public class SerialNumberTests
    {
        private readonly SerialNumber _sut = new SerialNumber("A12345B");

        [Fact]
        public void Constructor_InitializedWithString_SetsNumericValue()
        {
            var initialValue = "N4434";
            var expected = Base36.Decode(initialValue);
            var sut = new SerialNumber(initialValue);

            Assert.Equal(expected, sut.NumericValue);
        }

        [Fact]
        public void IncreaseBy_IncrementsValue_WhenCalled()
        {
            _sut.IncreaseBy(1);

            Assert.Equal("A12345C", _sut.ToString());
        }

        [Fact]
        public void DecreaseBy_DecreasesValue_WhenCalled()
        {
            _sut.DecreaseBy(2);

            Assert.Equal("A123459", _sut.ToString());
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenTwoValuesAreEqual()
        {
            var serial = new SerialNumber(_sut.NumericValue);

            var result = _sut.Equals(serial);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValuesAreNotEqual()
        {
            var serial = new SerialNumber(1);

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
            var serial = new SerialNumber(_sut.NumericValue);
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

        [Fact]
        public void AddOperator_IncrementsValue_WhenAddedToAnotherObject()
        {
            var serial1 = new SerialNumber(20);
            var serial2 = new SerialNumber(5);

            var result = serial1 + serial2;

            Assert.Equal((ulong)25, result.NumericValue);
        }

        [Fact]
        public void AddOperator_TreatsOneSideAsZero_WhenThatSideIsNull()
        {
            SerialNumber serial1 = null;
            SerialNumber serial2 = new SerialNumber(5);

            var result = serial1 + serial2;

            Assert.Equal((ulong)5, result.NumericValue);
        }

        [Fact]
        public void AddOperator_IncrementsValue_WhenAddedToInteger()
        {
            var serial1 = new SerialNumber(20);
            int increaseBy = 1;

            var result = serial1 + increaseBy;

            Assert.Equal((ulong)21, result.NumericValue);
        }

        [Fact]
        public void SubtractOperator_DecrementsValue_WhenAddedToAnotherObject()
        {
            var serial1 = new SerialNumber(20);
            var serial2 = new SerialNumber(5);

            var result = serial1 - serial2;

            Assert.Equal((ulong)15, result.NumericValue);
        }

        [Fact]
        public void SubtractOperator_TreatsOneSideAsZero_WhenThatSideIsNull()
        {
            SerialNumber serial1 = null;
            SerialNumber serial2 = new SerialNumber(5);

            var result = serial2 - serial1;

            Assert.Equal((ulong)5, result.NumericValue);
        }

        [Fact]
        public void SubtractOperator_IncrementsValue_WhenSubtractingAnInteger()
        {
            var serial1 = new SerialNumber(20);
            int decreaseBy = 1;

            var result = serial1 - decreaseBy;

            Assert.Equal((ulong)19, result.NumericValue);
        }

        [Fact]
        public void SubtractOperator_ThrowsOverflowException_WhenSubtractingIntWouldBeNegative()
        {
            var serial1 = new SerialNumber(1);
            int decreaseBy = 2;

            Assert.Throws<OverflowException>(() => serial1 - decreaseBy);
        }

        [Fact]
        public void SubtractOperator_ThrowsOverflowException_WhenSubtractingWouldBeNegative()
        {
            var serial1 = new SerialNumber(1);
            var serial2 = new SerialNumber(2);

            Assert.Throws<OverflowException>(() => serial1 - serial2);
        }


    }
}
