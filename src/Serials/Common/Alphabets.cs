using System;
using System.Collections.Generic;
using System.Text;

namespace Serials.Common
{
    internal static class Alphabets
    {
        public const string AlphaNumeric = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const string AlphaNumericRegularExpression = "^[a-zA-Z0-9]+$";
        public const string LettersRegularExpression = "^[a-zA-Z]+$";
    }
}
