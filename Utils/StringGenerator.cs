namespace BackendTests.Utils
{
    public class StringGenerator
    {
        public string AlphabetString(int length)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] randomChars = new char[length];
            for (int i = 0; i < length; i++)
                randomChars[i] = alphabet[random.Next(alphabet.Length)];

            string randomString = new string(randomChars);

            return randomString;
        }

        public string NumericString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            char[] randomChars = new char[length];
            for (int i = 0; i < length; i++)
                randomChars[i] = chars[random.Next(chars.Length)];

            string randomString = new string(randomChars);

            return randomString;
        }

        public string SpecialCharsString(int length)
        {
            const string chars = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            Random random = new Random();
            char[] randomChars = new char[length];
            for (int i = 0; i < length; i++)
                randomChars[i] = chars[random.Next(chars.Length)];

            string randomString = new string(randomChars);

            return randomString;
        }
    }
}
