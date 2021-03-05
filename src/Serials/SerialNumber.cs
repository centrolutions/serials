using Serials.Common;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Serials
{
    public class SerialNumber: SerialNumberBase<BigInteger>
    {
        public SerialNumber(string initialValue, SerialNumberConfiguration configuration): this(configuration.Encoder.Decode(initialValue), configuration)
        {
            if (!configuration.Encoder.CanDecode(initialValue))
                throw new ArgumentException($"Invalid characters were found in the {nameof(initialValue)} argument that the selected encoder cannot decode.");
        }
        public SerialNumber(BigInteger initialValue, SerialNumberConfiguration configuration): base(initialValue, configuration)
        {
        }
        public SerialNumber(int initialValue, SerialNumberConfiguration configuration) : this((BigInteger)initialValue, configuration) { }
        

        public override void IncreaseBy(int increase)
        {
            NumericValue += increase;
        }

        public override void DecreaseBy(int decrease)
        {
            NumericValue -= decrease;
        }

        public override string ToString()
        {
            return Configuration.Encoder.Encode(NumericValue);
        }
    }
}
