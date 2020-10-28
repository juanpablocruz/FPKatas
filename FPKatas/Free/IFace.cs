namespace FPKatas.Free
{
    public interface IFace<In1,Out1,In2, Out2>
    {
        Out1 Member1(In1 input);
        Out2 Member2(In2 input);
    }
}
