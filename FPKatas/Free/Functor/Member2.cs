using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Free
{
    public class Member2<R> : IFaceInstruction<R>
    {
        public string Data { get; }
        public Func<int, R> F { get; }
        public Member2(string data, Func<int, R> f)
        {
            Data = data;
            F = f;
        }
    }
}
