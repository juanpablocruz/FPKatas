using FPKatas.Church;
using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Linq;
using Unit = System.ValueTuple;

namespace FPKatas.RoseTree
{
    using static F;
    public static class RoseTree
    {
        public static IRoseTree<N, L> Node<N, L>(N value, params IRoseTree<N, L>[] branches)
            => new RoseNode<N, L>(value, branches);

        public static IRoseTree<N, L> Leaf<N, L>(L value)
            => new RoseLeaf<N, L>(value);

        public static IChurchBoolean IsLeaf<N, L>(this IRoseTree<N, L> source)
            => source.Match<IChurchBoolean>(
                node: (_, __) => new ChurchFalse(),
                leaf: _ => new ChurchTrue());

        public static IChurchBoolean IsNode<N, L>(this IRoseTree<N, L> source)
            => new ChurchNot(source.IsLeaf());


        public static TResult Cata<N, L, TResult>(
            this IRoseTree<N, L> tree,
            Func<N, IEnumerable<TResult>, TResult> node,
            Func<L, TResult> leaf)
            => tree.Match(
                node: (n, branches) => node(n, branches.Select(t => t.Cata(node, leaf))),
                leaf: leaf
            );

        private static T Id<T>(T x) => x;

        public static IRoseTree<N1, L1> BiMap<N, N1, L, L1>(
            this IRoseTree<N, L> source,
            Func<N, N1> mapNode,
            Func<L, L1> mapLeaf)
            => source.Cata(
                node: (n, branches) => new RoseNode<N1, L1>(mapNode(n), branches),
                leaf: l => (IRoseTree<N1, L1>)new RoseLeaf<N1, L1>(mapLeaf(l))
            );
        public static IRoseTree<N1, L> MapNode<N, N1, L>(
            this IRoseTree<N, L> source,
            Func<N, N1> func)
            => source.BiMap(func, l => l);

        public static IRoseTree<N, L1> MapLeaf<N, L, L1>(
            this IRoseTree<N, L> source,
            Func<L, L1> func)
            => source.BiMap(n => n, func);

        public static IRoseTree<N, L1> Map<N, L, L1>(
            this IRoseTree<N, L> source,
            Func<L, L1> fn)
            => source.MapLeaf(fn);

        public static IRoseTree<N, L1> Select<N, L, L1>(
            this IRoseTree<N, L> source,
            Func<L, L1> fn)
            => source.MapLeaf(fn);

        public static int Sum(this IRoseTree<int, int> source)
            => source.Cata((x, xs) => x + xs.Sum(), l => l);

        public static int Max(this IRoseTree<int, int> source)
            => source.Cata((x, xs) => xs.Any() ? Math.Max(x, xs.Max()) : x, l => l);

        public static int CountLeaves<N,L>(this IRoseTree<N,L> source)
            => source.Cata(
                node: (x, xs) => xs.ToList().FindAll(x => x == 1).Count(),
                leaf: l => 1
                );

        public static Maybe<L> Find<N, L>(this IRoseTree<N, L> source, Func<L,bool> f)
            => source.Cata(
                node: (x, xs) => xs.ToList().Find(x => f(x)),
                leaf: l => l
            );

        public static TResult Fold<N, L, TResult>(
            this IRoseTree<N, L> source, 
                Func<TResult, L,TResult> g,
                TResult acc
            )
            => source.Match(
                node: (x, xs) => xs.ToList().Aggregate(acc, (ac, t) =>  t.Fold(g, ac)),
                leaf: l => g(acc, l) 
            );

        public static IEnumerable<L> FindAll<N, L>(this IRoseTree<N, L> source, Func<L, bool> f)
            => source.Match(
                node: (x, xs) => xs.ToList().Aggregate(new List<L>(),(acc,t) => acc.Concat(t.FindAll(f)).ToList()),
                leaf: l => f(l) ? new List<L>() { l } : new List<L>() 
            );

        public static Maybe<IRoseTree<N, L>> Filter<N, L>(this IRoseTree<N, L> source, Func<L, bool> f)
            => source.Choose(x => f(x) ? Just(x) : Nothing);

        public static Maybe<IRoseTree<N, L1>> Choose<N, L, L1>(
            this IRoseTree<N, L> source,
            Func<L, Maybe<L1>> func)
            => source.Cata(
                node: (x, xs) => xs.ToList().FindAll(n => n.IsJust())
                                    .Select(n => n.Match(Nothing: () => default, Just: m => m))
                                    .Pipe(newbranch => Just(Node(x, newbranch.ToArray()))),
                leaf: l => func(l).Map(m => Leaf<N,L1>(m)));


        public static Unit Iter<N, L>(
            this IRoseTree<N, L> source,
            Func<L, Unit> f)
            => source.Fold((acc,x) => f(x), new Unit());
    }
}
