using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials.Common
{
    public class Base36Encoder : EncoderBase
    {
        public Base36Encoder() : base(Alphabets.AlphaNumeric, new System.Text.RegularExpressions.Regex(Alphabets.AlphaNumericRegularExpression))
        {
        }
    }
}
