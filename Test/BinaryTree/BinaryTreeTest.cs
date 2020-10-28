using FPKatas.BinaryTree;
using System.Collections.Generic;
using Xunit;

namespace Test.BinaryTreeTest
{
    public class BinaryTreeTest
    {
        public static IEnumerable<object[]> Trees
        {
            get
            {
                yield return new[] { BinaryTree.Leaf(0) };
                yield return new[] {
            BinaryTree.Create(-3,
                BinaryTree.Leaf(2),
                BinaryTree.Leaf(99)) };
                yield return new[] {
            BinaryTree.Create(42,
                BinaryTree.Create(1337,
                    BinaryTree.Leaf(0),
                    BinaryTree.Leaf(-22)),
                BinaryTree.Leaf(100)) };
                yield return new[] {
            BinaryTree.Create(-927,
                BinaryTree.Leaf(2),
                BinaryTree.Create(211,
                    BinaryTree.Leaf(88),
                    BinaryTree.Leaf(132))) };
                yield return new[] {
            BinaryTree.Create(111,
                BinaryTree.Create(-336,
                    BinaryTree.Leaf(113),
                    BinaryTree.Leaf(-432)),
                BinaryTree.Create(1299,
                    BinaryTree.Leaf(-32),
                    BinaryTree.Leaf(773))) };
            }
        }

        [Theory, MemberData(nameof(Trees))]
        public void FirstFunctorLaw(IBinaryTree<int> tree)
        {
            Assert.Equal(tree, tree.Select(x => x));
        }

        [Theory, MemberData(nameof(Trees))]
        public void SecondFunctorLaw(IBinaryTree<int> tree)
        {
            string g(int i) => i.ToString();
            bool f(string s) => s.Length % 2 == 0;

            Assert.Equal(tree.Select(g).Select(f), tree.Select(i => f(g(i))));
        }


    }
}
