using System;
using System.Collections.Generic;
using System.Text;

namespace AreaLib
{
    class Staff
    {
        public const double Accuracy = 1e-7;

        public void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
