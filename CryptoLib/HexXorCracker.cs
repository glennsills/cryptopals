using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoLib
{
    public class HexXorCracker
    {
        public (string key, string plainText) Crack (string cypherText)
        {
            var sortedListOfResults = new SortedList < float,
                (string key, string plainText) > (new FloatReverseComparer ());

            var cypherBytes = cypherText.HexDecode ();

            byte key = 0;
            var topScore = 0;
            do
            {
                ++key;
                var result = XorWithKey (key, cypherBytes);
                if (result.score > topScore)
                {
                    topScore = result.score;
                }
                if (!sortedListOfResults.ContainsKey (result.score))
                {
                    sortedListOfResults.Add (result.score,
                        (string.Format ("{0:x2}", result.key), result.plainText));
                }
            } while (key != 255);

            return sortedListOfResults[topScore];
        }

        public (string key, string clearText) Crack (string cryptoText, Encoding encoding, int keySize)
        {
            throw new NotImplementedException ();
        }

        public IList<Tuple<int, float>> GetKeySize (string inputFileData, Encoding encoding)
        {
            var keySizeList = new List<Tuple<int, float>> ();
            var bytes = encoding.GetBytes (inputFileData);
            for (var keySize = 2; keySize <= 40; ++keySize)
            {
                var firstKeysizeOfBytes = GetKeysizeOfBytes (bytes, keySize, 0);
                var secondKeysizeOfBytes = GetKeysizeOfBytes (bytes, keySize, keySize);
                var hammingDistance = firstKeysizeOfBytes.HammingDistance (secondKeysizeOfBytes);
                float normalizedHammingDistance = hammingDistance / (float) keySize;
                keySizeList.Add (new Tuple<int, float> (keySize, normalizedHammingDistance));
            }
            return keySizeList.OrderBy (i => i.Item2).ToList ();
        }

        public byte[] GetKeysizeOfBytes (byte[] bytes, int keysize, int offset)
        {
            var output = new byte[keysize];
            for (var i = 0 + offset; i < keysize + offset; ++i)
            {
                output[i - offset] = bytes[i];
            }
            return output;
        }

        private (int score, byte key, string plainText) XorWithKey (byte key, byte[] cypherBytes)
        {
            var trialPlainText = XorByteArrayToString (key, cypherBytes);
            var score = scoreCharacters (trialPlainText);
            return (score, key, trialPlainText);
        }

        private (int score, byte[] key, string plainText) XorWithKey (byte[] key, byte[] cypherBytes)
        {
            for (var i = 0; i < cypherBytes.Length / key.Length; i += key.Length)
            {
                for (var j = 0; j < key.Length; ++j)
                {

                }

            }
            throw new NotImplementedException ();
        }

        private int scoreCharacters (string trialPlainText)
        {
            var total = 0;
            foreach (var c in trialPlainText.ToCharArray ())
            {

                total += EnglishCharacterData.FrequencyScoreChar (c);
            }
            return total;
        }

        private string XorByteArrayToString (byte key, byte[] cypherBtyes)
        {
            var outputBuilder = new StringBuilder ();

            foreach (byte b in cypherBtyes)
            {
                outputBuilder.Append (Convert.ToChar (b ^ key));
            }
            return outputBuilder.ToString ();;
        }

        private byte[] XorByteArray (byte[] key, byte[] buffer)
        {
            var output = new byte[buffer.Length];
            var i = 0;
            for (var j = 0; j < buffer.Length; ++j)
            {
                output[j] = (byte) (buffer[j] ^ key[i]);
                i++;
                if (i == key.Length)
                {
                    i = 0;
                }
            }
            return output;
        }
    }
}