using System;
using System.Text;

namespace XORCaesar
{
    class Program
    {
        static void Main(string[] args)
        {
            int caeKey = 7;
            string xorKey = "PRAVDA";

            string caesar = Cipher.Caesar("Pokud hledáte pomocnou ruku, najdete ji na konci své paže", caeKey);
            string xor = Cipher.XOR("Pokud hledáte pomocnou ruku, najdete ji na konci své paže", xorKey);

            Console.WriteLine(caesar);
            Console.WriteLine(xor);

            Console.WriteLine(Cipher.DecryptCaesar(caesar, caeKey));
            Console.WriteLine(Cipher.DecryptXOR(xor, xorKey));

            Console.Read();
        }
    }

    class Cipher
    {
        #region Caesar Cipher
        private static char CaesarEncrypt(char ch, int key)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)(((ch + key - offset) % 26) + offset);
        }

        public static string Caesar(string input, int key)
        {
            string output = string.Empty;

            input = RemoveDiacritism(input);

            foreach (char ch in input)
                output += CaesarEncrypt(ch, key);


            return output;
        }

        public static string DecryptCaesar(string input, int key)
        {
            return Caesar(input, 26 - key);
        }

        #endregion

        #region XOR Cipher
        public static string XOR(string input, string key)
        {
            char[] output = new char[input.Length];

            input = RemoveDiacritism(input);

            for (int i = 0; i < input.Length; ++i)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        public static string DecryptXOR(string input, string key)
        {
            return XOR(input, key);
        }

        #endregion

        public static string RemoveDiacritism(string Text)
        {
            string stringFormD = Text.Normalize(NormalizationForm.FormD);
            StringBuilder retVal = new StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
