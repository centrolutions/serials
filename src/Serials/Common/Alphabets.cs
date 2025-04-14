namespace Serials.Common
{
    internal static class Alphabets
    {
        public const string AlphaNumeric = Numbers + Letters;
        public const string AlphaNumericPlusLowercase = Numbers + Letters + LettersLowercase;
        public const string Numbers = "0123456789";
        public const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LettersLowercase = "abcdefghijklmnopqrstuvwxyz";

        public const string AlphaNumericRegularExpression = "^[a-zA-Z0-9]+$";
        public const string AlphaNumericPlusLowercaseRegularExpression = "^[a-zA-Z0-9]+$";
        public const string LettersRegularExpression = "^[a-zA-Z]+$";
        public const string NumbersRegularExpression = "^[0-9]+$";
    }
}
