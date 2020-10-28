namespace FPKatas.Free
{
    public class Free<T> : IFaceProgram<T>
    {
        public IFaceInstruction<IFaceProgram<T>> Functor { get; }
        public Free(IFaceInstruction<IFaceProgram<T>> functorVal)
        {
            Functor = functorVal;
        }
    }
}
