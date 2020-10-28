using System;

namespace FPKatas.Helpers
{
    public static class CurryExt
    {
        // function manipulation 
        public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> func)
            => t1 => t2 => func(t1, t2);

        public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> func)
            => t1 => t2 => t3 => func(t1, t2, t3);

        public static Func<T1, Func<T2, Func<T3, Func<T4, R>>>> Curry<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> func)
            => t1 => t2 => t3 => t4 => func(t1, t2, t3, t4);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> Curry<T1, T2, T3, T4, T5, R>(this Func<T1, T2, T3, T4, T5, R> func)
            => t1 => t2 => t3 => t4 => t5 => func(t1, t2, t3, t4, t5);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> Curry<T1, T2, T3, T4, T5, T6, R>(this Func<T1, T2, T3, T4, T5, T6, R> func)
            => t1 => t2 => t3 => t4 => t5 => t6 => func(t1, t2, t3, t4, t5, t6);
    }
}
