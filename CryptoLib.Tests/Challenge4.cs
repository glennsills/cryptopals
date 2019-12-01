using System;
using CryptoLib;
using Xunit;

namespace CryptoLib.Tests
{
    public class SingleByteXORCypher
    {
        [Fact]
        public void CanHackThatCypherText ()
        {
            var expected = "Cooking MC's like a pound of bacon";
            var cypherText = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var cut = new HexXorCracker ();
            var result = cut.Crack (cypherText);
            Assert.Equal (expected, result.plainText);
        }
    }

}