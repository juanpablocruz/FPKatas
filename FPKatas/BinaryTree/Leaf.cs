using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.BinaryTree
{
    public sealed class Leaf<T> : IBinaryTree<T>
    {
        public T Item { get; }
        public Leaf(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Item = item;
        }

        public TResult Accept<TResult>(IBinaryTreeVisitor<T, TResult> visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));
            return visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Leaf<T> other))
                return false;
            return Equals(Item, other.Item);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode();
        }
    }
}
