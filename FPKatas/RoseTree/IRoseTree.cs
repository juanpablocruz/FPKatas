using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.RoseTree
{
    public interface IRoseTree<N, L>
    {
        TResult Match<TResult>(
            Func<N, IEnumerable<IRoseTree<N, L>>, TResult> node,
            Func<L, TResult> leaf);
    }
}
