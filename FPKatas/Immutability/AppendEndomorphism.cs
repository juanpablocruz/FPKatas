using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Immutability
{
    public class AppendEndomorphism<T> : IEndomorphism<T>
    {
        private readonly IEndomorphism<T> morphism1;
        private readonly IEndomorphism<T> morphism2;

        public AppendEndomorphism(IEndomorphism<T> morphism1, IEndomorphism<T> morphism2)
        {
            this.morphism1 = morphism1;
            this.morphism2 = morphism2;
        }

        public T Run(T x)
        {
            return morphism2.Run(morphism1.Run(x));
        }
    }
}
