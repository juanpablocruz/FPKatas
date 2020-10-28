namespace FPKatas.Church
{
    public interface IChurchBoolean
    {
        T Match<T>(T trueCase, T falseCase);
    }
}
