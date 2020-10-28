using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Purity
{
    public class Effect<T>
    {
        Func<T> f;
        public Effect(Func<T> f)
        {
            this.f = f;
        }

        public static Effect<T1> Of<T1>(Func<T1> f)
            => new Effect<T1>(f);

        public Effect<R1> Map<R1>(Func<T,R1> g)
        {
            return new Effect<R1>(() => g(f()));
        }

        public T Run()
        {
            return f();
        }

        public T Join()
        {
            return f();
        }

        public Effect<R1> Chain<R1>(Func<T, Effect<R1>> g)
        {
            return new Effect<R1>(() => g(f()).Join());
        }
    }
}
