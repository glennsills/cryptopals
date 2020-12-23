using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoLib
{
    public static class CryptoUtility
    {
        public static byte[] HexDecode (this string hexString)
        {
            var output = new List<byte> ();
            for (var i = 0; i < hexString.Length - 1; i += 2)
            {
                var hexNumber = hexString.Substring (i, 2);
                var number = byte.Parse (hexNumber, System.Globalization.NumberStyles.AllowHexSpecifier);
                output.Add (number);
            }
            return output.ToArray ();
        }

        public static string HexStringToBase64 (this string hexString)
        {
            return Convert.ToBase64String (hexString.HexDecode ());
        }

        public static byte[] XORHexString (this string hexString1, string hexString2)
        {
            if (hexString1.Length != hexString2.Length)
            {
                throw new CryptoLabException ("Strings must be the same length");
            }
            var byteArray1 = hexString1.HexDecode ();
            var byteArray2 = hexString2.HexDecode ();
            for (var i = 0; i < byteArray1.Length; ++i)
            {
                byteArray1[i] = (byte) (byteArray1[i] ^ byteArray2[i]);
            }
            return byteArray1;
        }

        public static string HexEncode (byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder (byteArray.Length * 2);
            foreach (byte b in byteArray)
                hex.AppendFormat ("{0:x2}", b);
            return hex.ToString ();
        }

        public static bool IsPrintable (this char c)
        {
            if (char.IsLetterOrDigit (c) || char.IsSymbol (c) || char.IsPunctuation (c) || char.IsWhiteSpace (c))
            {
                return true;
            }
            return false;
        }

        public static List<string> RepeatingKeyXor (string key, List<string> plainTextList)
        {
            var cryptoList = new List<string> ();

            foreach (var plainTextString in plainTextList)
            {
                var cryptostring = RepeatingKeyXor (key, plainTextString);
                var length = cryptostring.Length;

                cryptoList.Add (cryptostring);
            }
            return cryptoList;
        }

        public static string RepeatingKeyXor (string key, string plainText)
        {
            var plainTextLength = plainText.Length;

            var plainBytes = Encoding.ASCII.GetBytes (plainText);
            var plainBytesLength = plainBytes.Length;

            var keyBytes = Encoding.ASCII.GetBytes (key);
            var keyBytesLength = keyBytes.Length;

            var byteList = new List<byte> ();
            // var keyIndex = 0;
            // foreach (var b in plainBytes)
            // {
            //     byteList.Add ((byte) (keyBytes[keyIndex] ^ b));
            //     if (++keyIndex > key.Length - 1)
            //     {
            //         keyIndex = 0;
            //     }
            // }
            var result = XorByteArray (keyBytes, plainBytes);
            return HexEncode (result);
        }

        public static byte[] XorByteArray (byte[] key, byte[] buffer)
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

        public static int HammingDistance (this string thisString, string stringToCompare, Encoding encoding)
        {
            if (thisString.Length != stringToCompare.Length)
            {
                throw new CryptoLabException ("Strings must be the same length to compute hamming distance");
            }
            var thisByteArray = encoding.GetBytes (thisString);
            var byteArraytoCompare = encoding.GetBytes (stringToCompare);

            return thisByteArray.HammingDistance (byteArraytoCompare);
        }

        public static int HammingDistance (this byte[] thisByteArray, byte[] byteArraytoCompare)
        {
            var hammingDistance = 0;
            for (var i = 0; i < thisByteArray.Length; ++i)
            {
                var xoredByte = thisByteArray[i] ^ byteArraytoCompare[i];
                hammingDistance += xoredByte.CountBits ();
            }
            return hammingDistance;
        }

        public static int CountBits (this int b)
        {
            int count = 0;
            while (b != 0)
            {
                count++;
                b &= (b - 1);
            }
            return count;
        }

    }
}