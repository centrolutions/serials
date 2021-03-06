using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials.Common
{
    public class AlphabetEncoder: EncoderBase
    {
        public AlphabetEncoder(): base(Alphabets.Letters, new System.Text.RegularExpressions.Regex(Alphabets.LettersRegularExpression))
        {

        }
    }
}
