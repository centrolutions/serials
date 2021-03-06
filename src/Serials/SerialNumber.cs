using Serials.Common;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Serials
{
    public class SerialNumber: SerialNumberBase<BigInteger>
    {
        public SerialNumber(string initialValue, SerialNumberConfiguration configuration): this(Decode(initialValue, configuration), configuration)
        {
            initialValue = RemovePrefix(initialValue, configuration);
            if (!configuration.Encoder.CanDecode(initialValue))
                throw new ArgumentException($"Invalid characters were found in the {nameof(initialValue)} argument '{initialValue}' that the selected encoder cannot decode.");
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
            var encoded = Configuration.Encoder.Encode(NumericValue);
            encoded = ApplyMinimumLength(encoded);
            encoded = ApplyPrefix(encoded);

            return encoded;
        }


        private static BigInteger Decode(string initialValue, SerialNumberConfiguration config)
        {
            initialValue = RemovePrefix(initialValue, config);
            return config.Encoder.Decode(initialValue);
        }
    }
}
