using System;

namespace Tennis
{
    public class GamePoint : IPoints
    {
        public IPoints Accept(IPoints visitor)
        {
            throw new InvalidOperationException();
        }

        public IPoints LoseBall()
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(IPoints opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public override bool Equals(object obj)
        {
            return obj is GamePoint;
        }

        public override int GetHashCode()
        {
            return 42;
        }
    }
}
