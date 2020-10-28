﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tennis
{
    public class FortyPoints : IPoints
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
            return new GamePoint();
        }

        public IPoints WinBall(FortyPoints opponentPoints)
        {
            return new AdvantagePoint();
        }

        public IPoints WinBall(AdvantagePoint opponentPoints)
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is FortyPoints;
        }

        public override int GetHashCode()
        {
            return 40;
        }
    }
}
