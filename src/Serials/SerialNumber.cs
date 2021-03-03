using Serials.Common;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Serials
{
    public class SerialNumber: SerialNumberBase<BigInteger>
    {
        public SerialNumber(string initialValue): this(Base36.Decode(initialValue))
        {
            var alphaNumericsOnly = new Regex("^[a-zA-Z0-9]+$");
            if (!alphaNumericsOnly.IsMatch(initialValue))
                throw new ArgumentException("Only alpha-numeric characters are supported");
        }
        public SerialNumber(BigInteger initialValue): base(initialValue)
        {
        }

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
            return Base36.Encode(NumericValue);
        }
    }
}
