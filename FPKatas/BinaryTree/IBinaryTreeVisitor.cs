using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.BinaryTree
{
    public interface IBinaryTreeVisitor<T, TResult>
    {
        TResult Visit(Node<T> node);

        TResult Visit(Leaf<T> leaf);
    }
}
