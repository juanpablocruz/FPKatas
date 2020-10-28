﻿using System;

namespace FPKatas.BinaryTree
{
    public static class BinaryTree
    {
        public static IBinaryTree<T> Leaf<T>(T item)
            => new Leaf<T>(item);

        public static IBinaryTree<T> Create<T>(
            T item,
            IBinaryTree<T> left,
            IBinaryTree<T> right)
            => new Node<T>(item, left, right);

        public static IBinaryTree<TResult> Select<TResult, T>(
            this IBinaryTree<T> tree,
            Func<T, TResult> selector)
        {
            if (tree == null)
                throw new ArgumentNullException(nameof(tree));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var visitor = new SelectBinaryTreeVisitor<T, TResult>(selector);
            return tree.Accept(visitor);
        }
    }
}
