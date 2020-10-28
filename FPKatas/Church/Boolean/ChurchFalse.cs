namespace FPKatas.Church
{
    public class ChurchFalse : IChurchBoolean
    {
        public T Match<T>(T trueCase, T falseCase)
            => falseCase;
    }
}
