using Serials;
using Serials.Common;
using System;
using System.Numerics;
using Xunit;

namespace SerialsTests
{
    public class SerialNumberTests: SerialNumberCommonTests<SerialNumber, BigInteger>
    {
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
        public void Constructor_InitializedWithPrefixedString_SetsNumericValue()
        {
            var initialValue = "PRE-N4434";
            var config = new SerialNumberConfiguration() { Prefix = "PRE-" };
            var expected = config.Encoder.Decode("N4434");
            var sut = new SerialNumber(initialValue, config);

            Assert.Equal(expected, sut.NumericValue);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenInvalidCharactersArePassed()
        {
            Assert.Throws<ArgumentException>(() => new SerialNumber("ABC-123", _config));
        }

        [Fact]
        public void ToString_ReturnsBase36String_WhenCalled()
        {
            var expected = _encoder.Encode(_sut.NumericValue);

            Assert.Equal(expected, _sut.ToString());
        }
    }
}
