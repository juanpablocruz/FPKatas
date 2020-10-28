namespace FPKatas.BinaryTree
{
    public interface IBinaryTree<T>
    {
        TResult Accept<TResult>(IBinaryTreeVisitor<T, TResult> visitor);
    }
}
