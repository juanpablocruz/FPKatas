namespace FPKatas.Church
{
    public class ChurchNot : IChurchBoolean
    {
        private readonly IChurchBoolean b;

        public ChurchNot(IChurchBoolean b)
        {
            this.b = b;
        }

        public T Match<T>(T trueCase, T falseCase)
            => b.Match(falseCase, trueCase);
    }
}
