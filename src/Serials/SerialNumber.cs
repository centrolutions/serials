using Serials.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials
{
    public class SerialNumber: ISerialNumber
    {
        public ulong NumericValue { get; private set; }

        public SerialNumber(string initialValue)
        {
            NumericValue = Base36.Decode(initialValue);
        }
        public SerialNumber(ulong initialValue)
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

        public static SerialNumber operator+ (SerialNumber a, SerialNumber b)
        {
            var result = new SerialNumber((a?.NumericValue ?? 0) + (b?.NumericValue ?? 0));
            return result;
        }

        public static SerialNumber operator+ (SerialNumber a, int b)
        {
            var result = new SerialNumber((a?.NumericValue ?? 0) + (ulong)b);
            return result;
        }

        public static SerialNumber operator- (SerialNumber a, SerialNumber b)
        {
            if ((b?.NumericValue ?? 0) > (a?.NumericValue ?? 0))
                throw new OverflowException("Negative serial numbers are not supported.");

            var result = new SerialNumber((a?.NumericValue ?? 0) - (b?.NumericValue ?? 0));
            return result;
        }

        public static SerialNumber operator- (SerialNumber a, int b)
        {
            if ((ulong)b > (a?.NumericValue ?? 0))
                throw new OverflowException("Negative serial numbers are not supported.");

            var numericValue = (a?.NumericValue ?? 0) - (ulong)b;
            var result = new SerialNumber(numericValue);
            return result;
        }
    }
}
