using Serials.Common;
using System;
using System.Text.RegularExpressions;

namespace Serials
{
    public class SmallSerialNumber : SerialNumberBase<ulong>
    {
        public SmallSerialNumber(string initialValue) : this(Base36.DecodeLong(initialValue))
        {
            var alphaNumericsOnly = new Regex("^[a-zA-Z0-9]+$");
            if (!alphaNumericsOnly.IsMatch(initialValue))
                throw new ArgumentException("Only alpha-numeric characters are supported");
        }
        public SmallSerialNumber(ulong initialValue) : base(initialValue)
        {
        }

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
            return Base36.Encode(NumericValue);
        }
    }
}
