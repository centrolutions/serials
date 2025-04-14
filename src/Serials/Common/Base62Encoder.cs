namespace Serials.Common
{
    public class Base62Encoder : EncoderBase
    {
        public Base62Encoder() : base(Alphabets.AlphaNumericPlusLowercase, new System.Text.RegularExpressions.Regex(Alphabets.AlphaNumericPlusLowercaseRegularExpression))
        {
        }
    }
}
