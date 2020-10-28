﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.BinaryTree
{
    public sealed class Node<T> : IBinaryTree<T>
    {
        public T Item { get; }
        public IBinaryTree<T> Left { get; }
        public IBinaryTree<T> Right { get; }

        public Node(T item, IBinaryTree<T> left, IBinaryTree<T> right)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));

            Item = item;
            Left = left;
            Right = right;
        }

        public TResult Accept<TResult>(IBinaryTreeVisitor<T, TResult> visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));
            return visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Node<T> other))
                return false;

            return Equals(Item, other.Item)
                && Equals(Left, other.Left)
                && Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            return Item.GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
        }
    }
}
