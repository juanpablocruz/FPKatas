﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.RoseTree
{
    public sealed class RoseLeaf<N,L>: IRoseTree<N,L>
    {
        private readonly L value;

        public RoseLeaf(L value)
        {
            this.value = value;
        }

        public TResult Match<TResult>(
            Func<N,IEnumerable<IRoseTree<N,L>>, TResult> node,
            Func<L, TResult> leaf)
        {
            return leaf(value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RoseLeaf<N, L> other))
                return false;
            return Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
