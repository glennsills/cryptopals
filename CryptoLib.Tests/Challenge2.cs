using System;
using CryptoLib;
using Xunit;

namespace CryptoLib.Tests
{
    public class FixXORTests
    {

        [Fact]
        public void XORReturnsTheCorrectResult ()
        {
            var expected = "746865206b696420646f6e277420706c6179";
            var string1 = "1c0111001f010100061a024b53535009181c";
            var string2 = "686974207468652062756c6c277320657965";
            byte[] output = string1.XORHexString (string2);
            Assert.Equal (expected, CryptoUtility.HexEncode (output));
        }
    }
}