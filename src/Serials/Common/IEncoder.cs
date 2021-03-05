using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Serials.Common
{
    public interface IEncoder
    {
        BigInteger Decode(string encoded);
        ulong DecodeULong(string encoded);
        bool CanDecode(string encoded);
        string Encode(BigInteger decoded);
        string Encode(ulong decoded);
    }
}
