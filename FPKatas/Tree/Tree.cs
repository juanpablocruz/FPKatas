using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.Tree
{
    public sealed class Tree<T> : IReadOnlyCollection<Tree<T>>
    {
        private readonly IReadOnlyCollection<Tree<T>> children;

        public T Item { get; }

        public Tree(T item, IReadOnlyCollection<Tree<T>> children)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (children == null)
                throw new ArgumentNullException(nameof(children));

            Item = item;
            this.children = children;
        }

        public Tree<TResult> Select<TResult>(Func<T, TResult> selector)
            => Cata<Tree<TResult>>((x, nodes) => new Tree<TResult>(selector(x), nodes));
        

        public TResult Cata<TResult>(Func<T, IReadOnlyCollection<TResult>, TResult> func)
            => func(Item, children.Select(c => c.Cata(func)).ToArray());


        public int Count
        {
            get { return children.Count; }
        }

        public int CountLeaves()
            => Cata<int>((x, xs) => xs.Any() ? xs.Sum() : 1);

        public int MeasureDepth()
            => Cata<int>((x, xs) => xs.Any() ? 1 + xs.Max() : 0);

        public IEnumerator<Tree<T>> GetEnumerator()
            => children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => children.GetEnumerator();

        public override bool Equals(object obj)
        {
            if (!(obj is Tree<T> other))
                return false;

            return Equals(Item, other.Item)
                && Enumerable.SequenceEqual(this, other);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode() ^ children.GetHashCode();
        }
    }


    public static class Tree
    {
        public static Tree<T> Leaf<T>(T item)
            => new Tree<T>(item, new Tree<T>[0]);

        public static Tree<T> Create<T>(T item, params Tree<T>[] children)
            => new Tree<T>(item, children);

        public static int Sum(this Tree<int> tree)
            => tree.Cata<int>((x, xs) => x + xs.Sum());

        public static int Max(this Tree<int> tree)
            => tree.Cata<int>((x, xs) => xs.Any() ? Math.Max(x, xs.Max()) : x);
    }
}
