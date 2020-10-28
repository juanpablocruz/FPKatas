using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Free
{
    public class Member1<T> : IFaceInstruction<T>
    {
        public string Data { get; }
        public Func<int, T> F { get; }
        public Member1(string data, Func<int, T> f)
        {
            Data = data;
            F = f;
        }
    }
}
