using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.BinaryTree
{
    public static class Example
    {
        public static void Test()
        {
            var source = BinaryTree.Create(42,
                BinaryTree.Create(1337,
                    BinaryTree.Leaf(0),
                    BinaryTree.Leaf(-22)),
                BinaryTree.Leaf(100));

            IBinaryTree<string> dest = source.Select(i => i.ToString());
            IBinaryTree<string> dest2 = from i in source select i.ToString();
        }
    }
}
