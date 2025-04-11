using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Serials.Common
{
    public abstract class EncoderBase : IEncoder
    {
        protected string Alphabet { get; private set; }
        protected Regex Validator { get; private set; }

        readonly int _alphabetLength;
        readonly bool _onlyUseUppercase;

        protected EncoderBase(string alphabet, Regex validator)
        {
            Alphabet = alphabet;
            Validator = validator;
            _alphabetLength = Alphabet.Length;
            _onlyUseUppercase = Alphabet == Alphabet.ToUpper();
        }

        public BigInteger Decode(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                throw new ArgumentException("Cannot be null or empty", nameof(encoded));

            var sanitizedInput = (_onlyUseUppercase) ? encoded.ToUpper() : encoded;
            BigInteger result = 0;
            for (int i = sanitizedInput.Length - 1, j = 0; i >= 0; i--, j++)
            {
                var c = sanitizedInput[i];
                var alphabetIndex = Alphabet.IndexOf(c);
                var multiplier = BigInteger.Pow(_alphabetLength, j);
                result += (alphabetIndex * multiplier);
            }
            return result;
        }

        public ulong DecodeULong(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                throw new ArgumentException("Cannot be null or empty", nameof(encoded));

            var sanitizedInput = (_onlyUseUppercase) ? encoded.ToUpper() : encoded;
            ulong result = 0;
            for (int i = sanitizedInput.Length - 1, j = 0; i >= 0; i--, j++)
            {
                var c = sanitizedInput[i];
                var alphabetIndex = (ulong)Alphabet.IndexOf(c);
                var multiplier = (ulong)Math.Pow(_alphabetLength, j);
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
                var index = (int)(currentVal % _alphabetLength);
                result.Push(Alphabet[index]);
                currentVal /= _alphabetLength;
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
                var index = (int)(currentVal % (ulong)_alphabetLength);
                result.Push(Alphabet[index]);
                currentVal /= (ulong)_alphabetLength;
            }

            return new string(result.ToArray());
        }

        public bool CanDecode(string encoded)
        {
            return Validator.IsMatch(encoded);
        }
    }
}
