using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Purity
{
    public static class LazyFunction
    {
        public static int impureFunction(Action<string> sideEffectFn)
        {
            sideEffectFn("Launch nuclear missiles!");
            return 0;
        }


        public static Func<int> pureFunction(Action<string> sideEffectFn)
        {
            Func<int> zeroFn = () =>
            {
                sideEffectFn("Launch nuclear missiles!");
                return 0;
            };

            return zeroFn;
        }
    }
}
