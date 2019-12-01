using System;
using Xunit;
using CryptoLib;

namespace CryptoLib.Tests
{
    public class ConvertHextToBase64
    {
        [Fact]
        public void HexDecode()
        {
            var expected = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
            var hexString = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            byte[] byArray = hexString.HexDecode();
            Assert.Equal(expected, Convert.ToBase64String(byArray));
        }

        [Fact]
        public void HexToBase64()
        {
            var expected = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
            var hexString = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";

            Assert.Equal(expected, hexString.HexStringToBase64());
        }
    }
}
