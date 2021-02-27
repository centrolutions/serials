using System;
using System.Collections.Generic;
using System.Text;

namespace Serials.Common
{
    public class Base36
    {
        private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static ulong Decode(string input)
        {
            ulong result = 0;
            for (int i = input.Length - 1, j = 0; i >= 0; i--, j++)
            {
                var c = input[i];
                var alphabetIndex = (ulong)Alphabet.IndexOf(c);
                var multiplier = (ulong)Math.Pow(36, j);
                result += alphabetIndex * multiplier;
            }
            return result;
        }
    }
}
