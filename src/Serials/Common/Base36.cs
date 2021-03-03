using System;
using System.Collections.Generic;
using System.Numerics;

namespace Serials.Common
{
    public static class Base36
	{
		private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static BigInteger Decode(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("Cannot be null or empty", nameof(input));

			var upperInput = input.ToUpper();
			BigInteger result = 0;
			for (int i = upperInput.Length - 1, j = 0; i >= 0; i--, j++)
			{
				var c = upperInput[i];
				var alphabetIndex = Alphabet.IndexOf(c);
				var multiplier = BigInteger.Pow(36, j);
				result += (alphabetIndex * multiplier);
			}
			return result;
		}

		public static string Encode(BigInteger input)
		{
			if (input == 0)
				return "0";

			var currentVal = input;
			var result = new Stack<char>();
			while (currentVal != 0)
			{
				var index = (int)(currentVal % 36);
				result.Push(Alphabet[index]);
				currentVal /= 36;
			}

			return new string(result.ToArray());
		}

		public static ulong DecodeLong(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("Cannot be null or empty", nameof(input));

			var upperInput = input.ToUpper();
			ulong result = 0;
			for (int i = upperInput.Length - 1, j = 0; i >= 0; i--, j++)
			{
				var c = upperInput[i];
				var alphabetIndex = (ulong)Alphabet.IndexOf(c);
				var multiplier = (ulong)Math.Pow(36, j);
				result += alphabetIndex * multiplier;
			}
			return result;
		}

		public static string Encode(ulong input)
		{
			if (input == 0)
				return "0";

			var currentVal = input;
			var result = new Stack<char>();
			while (currentVal != 0)
			{
				var index = (int)(currentVal % 36);
				result.Push(Alphabet[index]);
				currentVal /= 36;
			}

			return new string(result.ToArray());
		}
	}
}
