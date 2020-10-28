namespace FPKatas.Church
{
    public class ChurchAnd : IChurchBoolean
    {
        private readonly IChurchBoolean x;
        private readonly IChurchBoolean y;

        public ChurchAnd(IChurchBoolean x, IChurchBoolean y)
        {
            this.x = x;
            this.y = y;
        }

        public T Match<T>(T trueCase, T falseCase)
            => x.Match(y.Match(trueCase, falseCase), falseCase);
    }
}
