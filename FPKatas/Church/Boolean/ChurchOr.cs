namespace FPKatas.Church
{
    public class ChurchOr : IChurchBoolean
    {
        private readonly IChurchBoolean x;
        private readonly IChurchBoolean y;

        public ChurchOr(IChurchBoolean x, IChurchBoolean y)
        {
            this.x = x;
            this.y = y;
        }

        public T Match<T>(T trueCase, T falseCase)
            => x.Match(trueCase, y.Match(trueCase, falseCase));
    }
}
