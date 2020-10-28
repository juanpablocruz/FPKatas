namespace FPKatas.Immutability
{
    public interface IEndomorphism<T>
    {
        T Run(T x);
    }
}
