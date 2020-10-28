using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Free
{
    public interface IFaceInstruction<T>
    {

    }


    public static class IFaceInstructionExt
    {
        public static void Deconstruct<T>(this Member1<T> m1, out string data, out Func<int, T> f)
        {
            data = m1.Data;
            f = m1.F;
        }

        public static void Deconstruct<T>(this Member2<T> m1, out string data, out Func<int, T> f)
        {
            data = m1.Data;
            f = m1.F;
        }



        public static IFaceInstruction<R> MapI<T, R>(this IFaceInstruction<T> instruction, Func<T, R> f)
        {
            Func<Member1<T>, Func<T, R>, Member1<R>> mapM1 = (m1, g) =>
            {
                (var data, var f) = m1;
                return new Member1<R>(data, x => g(f(x)));
            };

            Func<Member2<T>, Func<T, R>, Member2<R>> mapM2 = (m2, g) =>
            {
                (var data, var f) = m2;
                return new Member2<R>(data, x => g(f(x)));
            };

            return instruction switch
            {
                Member1<T> m1 => mapM1(m1, f),
                Member2<T> m2 => mapM2(m2, f),
                _ => default
            };
        }


    }
}
