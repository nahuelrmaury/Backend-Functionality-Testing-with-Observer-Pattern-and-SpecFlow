namespace BackendTests.Utils
{
    public class StringGenerator
    {
        private readonly Random random = new Random();
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericChars = "0123456789";
        private const string SpecialChars = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        public string AlphabetString(int length)
        {
            char[] randomChars = Enumerable.Range(0, length)
                .Select(_ => Alphabet[random.Next(Alphabet.Length)])
                .ToArray();

            return new string(randomChars);
        }

        public string NumericString(int length)
        {
            char[] randomChars = Enumerable.Range(0, length)
                .Select(_ => NumericChars[random.Next(NumericChars.Length)])
                .ToArray();

            return new string(randomChars);
        }

        public string SpecialCharsString(int length)
        {
            char[] randomChars = Enumerable.Range(0, length)
                .Select(_ => SpecialChars[random.Next(SpecialChars.Length)])
                .ToArray();

            return new string(randomChars);
        }
    }
}
