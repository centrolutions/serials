using Serials.Common;
using System;
using System.Text.RegularExpressions;

namespace Serials
{
    public class SmallSerialNumber : SerialNumberBase<ulong>
    {
        public SmallSerialNumber(string initialValue, SerialNumberConfiguration configuration) : this(configuration.Encoder.DecodeULong(initialValue), configuration)
        {
            if (!configuration.Encoder.CanDecode(initialValue))
                throw new ArgumentException($"Invalid characters were found in the {nameof(initialValue)} argument that the selected encoder cannot decode.");
        }
        public SmallSerialNumber(ulong initialValue, SerialNumberConfiguration configuration) : base(initialValue, configuration)
        {
        }
        public SmallSerialNumber(int initialValue, SerialNumberConfiguration configuration): this((ulong)initialValue, configuration) { }

        public override void IncreaseBy(int increase)
        {
            NumericValue += (ulong)increase;
        }

        public override void DecreaseBy(int decrease)
        {
            NumericValue -= (ulong)decrease;
        }

        public override string ToString()
        {
            return Configuration.Encoder.Encode(NumericValue);
        }
    }
}
