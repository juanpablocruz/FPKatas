namespace FPKatas.Church
{
    public class ChurchTrue : IChurchBoolean
    {
        public T Match<T>(T trueCase, T falseCase)
            => trueCase;
    }
}
