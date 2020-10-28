using System;

namespace Tennis
{
    public class AdvantagePoint : IPoints
    {
        public IPoints Accept(IPoints visitor)
        {
            return visitor.WinBall(this);
        }

        public IPoints LoseBall()
        {
            return new FortyPoints();
        }

        public IPoints WinBall(IPoints opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            return new GamePoint();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public override bool Equals(object obj)
        {
            return obj is AdvantagePoint;
        }

        public override int GetHashCode()
        {
            return 41;
        }
    }
}
