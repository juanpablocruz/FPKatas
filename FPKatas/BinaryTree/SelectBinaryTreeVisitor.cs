using System;

namespace FPKatas.BinaryTree
{
    using static BinaryTree;

    public class SelectBinaryTreeVisitor<T, TResult> :
    IBinaryTreeVisitor<T, IBinaryTree<TResult>>
    {
        private readonly Func<T, TResult> selector;

        public SelectBinaryTreeVisitor(Func<T, TResult> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            this.selector = selector;
        }

        public IBinaryTree<TResult> Visit(Leaf<T> leaf)
        {
            var mappedItem = selector(leaf.Item);
            return Leaf(mappedItem);
        }

        public IBinaryTree<TResult> Visit(Node<T> node)
        {
            var mappedItem = selector(node.Item);
            var mappedLeft = node.Left.Accept(this);
            var mappedRight = node.Right.Accept(this);
            return Create(mappedItem, mappedLeft, mappedRight);
        }
    }
}
