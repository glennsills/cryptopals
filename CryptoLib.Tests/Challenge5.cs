using System;
using System.Collections.Generic;
using CryptoLib;
using Xunit;

namespace CryptoLib.Tests
{

    public class RepeatingKeyXor
    {
        [Fact]
        public void ThreeLetterKeyEncrypts ()
        {
            var input = @"Burning 'em, if you ain't quick and nimble
I go crazy when I hear a cymbal";

            var key = "ICE";
            var expected = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";

            var result = CryptoUtility.RepeatingKeyXor (key, input);
            Assert.Equal (expected, result);
        }
    }
}