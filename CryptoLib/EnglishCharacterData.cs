using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CryptoLib
{
    public class EnglishCharacterData
    {
        public static readonly ReadOnlyDictionary<char, int> Frequency;
        private static string _charactersOrderedByFrequency = " etaoinsrhldcumfgpyw\r\nb,.vk-\"_'x)(;0j1q=2:z/*!?$35>{}49[]867\\+|&<%@#^`~";

        static EnglishCharacterData ()
        {
            var readWriteDictionary = new Dictionary<char, int> ();

            for (int i = _charactersOrderedByFrequency.Length - 1; i >= 0; --i)
            {
                readWriteDictionary
                .Add (_charactersOrderedByFrequency[i], _charactersOrderedByFrequency.Length -i);
            }

            Frequency = new ReadOnlyDictionary<char, int> (readWriteDictionary);
        }

        public static int FrequencyScoreChar (char byteChar)
        {

            var c = char.ToLower (Convert.ToChar (byteChar));
            if (Frequency.ContainsKey (c))
            {
                return Frequency[c];
            }
            return 0;
        }
    }
}