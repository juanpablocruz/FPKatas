using System;

namespace FPKatas.Free
{
    public interface IFaceProgram<T>
    {
    }


    public static class FaceProgramExt
    {
        public static Func<IFaceProgram<T>, IFaceProgram<R>> Bind<T, R>(Func<T, IFaceProgram<R>> f)
            => program
            =>
        {
            return program switch
            {
                Free<T> free => new Free<R>(free.Functor.MapI(Bind(f))),
                Pure<T> x => f(x.Value),
                _ => default
            };
        };
    }


    public class FaceBuilder
    {
        public IFaceProgram<R> Bind<T, R>(IFaceProgram<T> program, Func<T, IFaceProgram<R>> func)
            => FaceProgramExt.Bind(func)(program);

        public IFaceProgram<T> Return<T>(T x)
            => new Pure<T>(x);

        public IFaceProgram<T> Zero<T>()
            => new Pure<T>(default);
    }

    public static class FaceBuilderMethods
    {
        public static IFaceProgram<int> member1<T>(string inpt)
            => new Free<int>(new Member1<IFaceProgram<int>>(inpt, x => new Pure<int>(x)));

        public static IFaceProgram<int> member2<T>(string inpt)
            => new Free<int>(new Member2<IFaceProgram<int>>(inpt, x => new Pure<int>(x)));

        public static T interpret<T>(IFaceProgram<T> program)
        {
            Func<string, int> imp1 = x => {
                var b = int.TryParse(x, out var result);
                if (b)
                    return result;
                return default;
            };


            Func<Free<Member1<IFaceProgram<T>>>, T> interpretM1 = f =>
            {
                var m1 = ((Member1<IFaceProgram<T>>)f.Functor);
                return interpret(m1.F(imp1(m1.Data)));
            };

            Func<Free<Member2<IFaceProgram<T>>>, T> interpretM2 = f =>
            {
                var m2 = ((Member2<IFaceProgram<T>>)f.Functor);
                return interpret(m2.F(imp1(m2.Data)));
            };

            return program switch
            {
                Pure<T> p => p.Value,
                Free<Member1<IFaceProgram<T>>> m1 => interpretM1(m1),
                Free<Member2<IFaceProgram<T>>> m2 => interpretM2(m2),
                _ => default
            };
        }
    }
}
