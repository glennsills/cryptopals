using System.Collections;
using System.Collections.Generic;

namespace CryptoLib
{

    public class FloatReverseComparer : IComparer<float>
    {
        public int Compare (float x, float y)
        {
            if (x > y) return -1;
            if (x < y) return 1;
            return 0;
        }
    }
}