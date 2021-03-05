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
