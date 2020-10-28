namespace FPKatas.Church
{
    public static class NaturalNumbers
    {
        public static int Count(this INaturalNumber n)
        {
            return n.Match(
                zero: 0,
                succ: p => 1 + p.Count());
        }

        public static INaturalNumber Add(this INaturalNumber x, INaturalNumber y)
            => x.Match(
                zero: y,
                succ: p => new Successor(p.Add(y)));

        public static IChurchBoolean IsZero(this INaturalNumber n)
            => n.Match<IChurchBoolean>(
                zero: new ChurchTrue(),
                succ: _ => new ChurchFalse());

        public static IChurchBoolean IsEven(this INaturalNumber n)
            => n.Match(
                zero: new ChurchTrue(),
                succ: p1 => p1.Match(
                    zero: new ChurchFalse(),    // If 0 then successor was 1
                    succ: p2 => p2.IsEven()));  // Eval previous' previous

        public static IChurchBoolean IsOff(this INaturalNumber n)
            => new ChurchNot(n.IsEven());


    }
}
