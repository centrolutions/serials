using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serials.Common
{
    public abstract class EncoderBase : IEncoder
    {
        protected string Alphabet { get; private set; }
        protected Regex Validator { get; private set; }
        private readonly int AlphabetLength;

        protected EncoderBase(string alphabet, Regex validator)
        {
            Alphabet = alphabet;
            Validator = validator;
            AlphabetLength = Alphabet.Length;
        }

        public BigInteger Decode(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                throw new ArgumentException("Cannot be null or empty", nameof(encoded));

            var upperInput = encoded.ToUpper();
            BigInteger result = 0;
            for (int i = upperInput.Length - 1, j = 0; i >= 0; i--, j++)
            {
                var c = upperInput[i];
                var alphabetIndex = Alphabet.IndexOf(c);
                var multiplier = BigInteger.Pow(AlphabetLength, j);
                result += (alphabetIndex * multiplier);
            }
            return result;
        }

        public ulong DecodeULong(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                throw new ArgumentException("Cannot be null or empty", nameof(encoded));

            var upperInput = encoded.ToUpper();
            ulong result = 0;
            for (int i = upperInput.Length - 1, j = 0; i >= 0; i--, j++)
            {
                var c = upperInput[i];
                var alphabetIndex = (ulong)Alphabet.IndexOf(c);
                var multiplier = (ulong)Math.Pow(AlphabetLength, j);
                result += alphabetIndex * multiplier;
            }
            return result;
        }

        public string Encode(BigInteger decoded)
        {
            if (decoded == 0)
                return Alphabet[0].ToString();

            var currentVal = decoded;
            var result = new Stack<char>();
            while (currentVal != 0)
            {
                var index = (int)(currentVal % AlphabetLength);
                result.Push(Alphabet[index]);
                currentVal /= AlphabetLength;
            }

            return new string(result.ToArray());
        }

        public string Encode(ulong decoded)
        {
            if (decoded == 0)
                return Alphabet[0].ToString();

            var currentVal = decoded;
            var result = new Stack<char>();
            while (currentVal != 0)
            {
                var index = (int)(currentVal % (ulong)AlphabetLength);
                result.Push(Alphabet[index]);
                currentVal /= (ulong)AlphabetLength;
            }

            return new string(result.ToArray());
        }

        public bool CanDecode(string encoded)
        {
            return Validator.IsMatch(encoded);
        }
    }
}
