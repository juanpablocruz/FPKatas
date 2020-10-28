using System;
using System.Collections.Generic;
using System.Text;

namespace Tennis
{
    public class ThirtyPoints : IPoints
    {
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
            return new FortyPoints();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            throw new InvalidOperationException();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            return new FortyPoints();
        }

        public override bool Equals(object obj)
        {
            return obj is ThirtyPoints;
        }

        public override int GetHashCode()
        {
            return 30;
        }
    }
}
