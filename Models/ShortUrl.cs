using System.ComponentModel;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        private static int _cipherLastDigit = 4;
        private static string[] _ciphers = {
            "31714",
            "92134",
            "30304",
            "20424",
            "07334",
            "13844",
            "07804",
            "46754",
            "22954",
            "44864"
        };


        public int Id { get; set; }
        public string Url { get; set; } = "";

        [DisplayName("Url Short Code")]
        public string UrlShortCode
        {
            get
            {
                return IdToUrlKey(this.Id);
            }
        }

        public static int UrlKeyToId(string urlKey)
        {
            try
            {
                int cipherId = ((int)char.GetNumericValue(urlKey[urlKey.Length - 1]) + 10 - _cipherLastDigit) % 10;
                string cipherCode = _ciphers[cipherId];
                string outputDigits = "";
                for (int i = 0; i < urlKey.Length; i++)
                {
                    // working left-to-right
                    int urlKeyDigit = (int)char.GetNumericValue(urlKey[i]);
                    int cipherDigit = 0;
                    if (urlKey.Length - i <= cipherCode.Length)
                    {
                        int indexFromRight = urlKey.Length - i - 1;
                        int cipherIndex = cipherCode.Length - indexFromRight - 1;
                        cipherDigit = (int)char.GetNumericValue(cipherCode[cipherIndex]);
                    }
                    outputDigits += ((urlKeyDigit - cipherDigit + 10) % 10).ToString();
                }
                return int.Parse(outputDigits);
            }
            catch { return -1; }
        }

        public static string IdToUrlKey(int id)
        {
            string output = "";
            string stringId = id.ToString("00000");
            string cipherCode = _ciphers[id % 10];

            for (int i = 0; i < stringId.Length; i++)
            {
                // working right-to-left
                int idDigit = (int)char.GetNumericValue(stringId[stringId.Length - 1 - i]);
                int cipherDigit = 0;
                if (i < cipherCode.Length) { cipherDigit = (int)char.GetNumericValue(cipherCode[cipherCode.Length - 1 - i]); }
                int scrambledDigit = (idDigit + cipherDigit) % 10;
                output = scrambledDigit + output;
            }

            return output;
        }
    }
}
