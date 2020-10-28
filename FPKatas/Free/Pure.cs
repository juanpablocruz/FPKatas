namespace FPKatas.Free
{
    public class Pure<T> : IFaceProgram<T>
    {
        public T Value { get; }

        public Pure(T value)
        {
            Value = value;
        }
    }
}
