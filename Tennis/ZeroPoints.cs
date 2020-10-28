using System;

namespace Tennis
{
    public class ZeroPoints : IPoints
    {
        public IPoints WinBall(IPoints opponentPoints)
        {
            return new FifteenPoints();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            return new FifteenPoints();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints LoseBall()
        {
            return this;
        }

        public IPoints Accept(IPoints visitor)
        {
            return visitor.WinBall(this);
        }

        public override bool Equals(object obj)
        {
            return obj is ZeroPoints;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
