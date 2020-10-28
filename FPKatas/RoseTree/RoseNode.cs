﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.RoseTree
{
    public sealed class RoseNode<N,L> : IRoseTree<N,L>
    {
        private readonly N value;
        private readonly IEnumerable<IRoseTree<N, L>> branches;

        public RoseNode(N value, IEnumerable<IRoseTree<N, L>> branches)
        {
            this.value = value;
            this.branches = branches;
        }

        public TResult Match<TResult>(
            Func<N, IEnumerable<IRoseTree<N, L>>, TResult> node,
            Func<L, TResult> leaf)
        {
            return node(value, branches);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RoseNode<N, L> other))
                return false;
            return Equals(value, other.value)
                && Enumerable.SequenceEqual(branches, other.branches);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ branches.GetHashCode();
        }
    }
}
