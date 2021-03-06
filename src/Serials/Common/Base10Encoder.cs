using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials.Common
{
    public class Base10Encoder: EncoderBase
    {
        public Base10Encoder() : base(Alphabets.Numbers, new System.Text.RegularExpressions.Regex(Alphabets.NumbersRegularExpression))
        {
        }
    }
}
