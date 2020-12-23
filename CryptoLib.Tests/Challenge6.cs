using System;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace CryptoLib.Tests
{
    public class Challenge6
    {

        public Challenge6 () { }

        [Fact]
        public void CanComputeHammingDistanceForStrings ()
        {
            var string1 = "this is a test";
            var string2 = "wokka wokka!!!";
            var expected = 37;
            var result = string1.HammingDistance (string2, Encoding.ASCII);
            Assert.Equal (expected, result);
        }

        [Fact]
        public void CanHamForByteArray ()
        {
            var array1 = new byte[] { 0x01, 0x01, 0x01, 0x01 };
            var array2 = new byte[] { 0x01, 0x01, 0x01, 0x00 };
            var expected = 1;

            var result = array1.HammingDistance (array2);

            Assert.Equal (expected, result);

        }

        [Fact]
        public void GetKeySizeOfBytesReturnsCorrectValue ()
        {
            var input = new byte[] { 0x01, 0x22, 0x33, 0x44, 0x99, 0x66 };
            var expected1 = new byte[] { 0x01, 0x22, 0x33 };
            var expected2 = new byte[] { 0x44, 0x99, 0x66 };
            var cut = new HexXorCracker ();
            var result1 = cut.GetKeysizeOfBytes (input, 3, 0);
            var result2 = cut.GetKeysizeOfBytes (input, 3, 3);
            Assert.Equal (expected1, result1);
            Assert.Equal (expected2, result2);

        }

        [Fact]
        public void CanNormalizeByKeySize ()
        {
            var cryptoText = File.ReadAllText (GetTestFilePath ("../../../challenge6.txt"));
            var cut = new HexXorCracker ();
            var result = cut.GetKeySize (cryptoText, Encoding.ASCII);
            Assert.Equal(39, result.Count);
        }

        public string GetTestFilePath (string relativePath)
        {
            var codeBaseUrl = new Uri (Assembly.GetExecutingAssembly ().CodeBase);
            var codeBasePath = Uri.UnescapeDataString (codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName (codeBasePath);
            return Path.Combine (dirPath, relativePath);
        }
    }
}