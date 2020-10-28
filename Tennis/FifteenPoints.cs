using System;
using System.Collections.Generic;
using System.Text;

namespace Tennis
{
    public class FifteenPoints : IPoints
    {
        public override bool Equals(object obj)
        {
            return obj is FifteenPoints;
        }

        public override int GetHashCode()
        {
            return 15;
        }

        public IPoints Accept(IPoints visitor)
        {
            return visitor.WinBall(this);
        }

        public IPoints LoseBall()
        {
            return this;
        }

        public IPoints WinBall(IPoints opponentPoints)
        {
            return new ThirtyPoints();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            return new ThirtyPoints();
        }
    }
}
