using Serials.Common;
using System;

namespace Serials
{
    public class SmallSerialNumber : ISerialNumber
    {
        public ulong NumericValue { get; private set; }

        public SmallSerialNumber(string initialValue)
        {
            NumericValue = Base36.DecodeLong(initialValue);
        }
        public SmallSerialNumber(ulong initialValue)
        {
            NumericValue = initialValue;
        }

        public void IncreaseBy(int increase)
        {
            NumericValue += (ulong)increase;
        }

        public void DecreaseBy(int decrease)
        {
            NumericValue -= (ulong)decrease;
        }

        public override bool Equals(object obj)
        {
            var serial = obj as SerialNumber;
            if (obj == null)
                return false;

            return this.NumericValue.Equals(serial.NumericValue);
        }

        public override int GetHashCode()
        {
            return NumericValue.GetHashCode();
        }

        public override string ToString()
        {
            return (string)Base36.Encode(NumericValue);
        }

        public static SmallSerialNumber operator +(SmallSerialNumber a, SmallSerialNumber b)
        {
            var result = new SmallSerialNumber((a?.NumericValue ?? 0) + (b?.NumericValue ?? 0));
            return result;
        }

        public static SmallSerialNumber operator +(SmallSerialNumber a, int b)
        {
            var result = new SmallSerialNumber((a?.NumericValue ?? 0) + (ulong)b);
            return result;
        }

        public static SmallSerialNumber operator -(SmallSerialNumber a, SmallSerialNumber b)
        {
            if ((b?.NumericValue ?? 0) > (a?.NumericValue ?? 0))
                throw new OverflowException("Negative serial numbers are not supported.");

            var result = new SmallSerialNumber((a?.NumericValue ?? 0) - (b?.NumericValue ?? 0));
            return result;
        }

        public static SmallSerialNumber operator -(SmallSerialNumber a, int b)
        {
            if ((ulong)b > (a?.NumericValue ?? 0))
                throw new OverflowException("Negative serial numbers are not supported.");

            var numericValue = (a?.NumericValue ?? 0) - (ulong)b;
            var result = new SmallSerialNumber(numericValue);
            return result;
        }
    }
}
