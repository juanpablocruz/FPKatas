using System;
using System.Collections.Generic;
using System.Text;

namespace Tennis
{
    public interface IPoints
    {
        IPoints Accept(IPoints visitor);
        IPoints LoseBall();
        IPoints WinBall(IPoints opponentPoints);
        IPoints WinBall(AdvantagePoint opponentPoints);
        IPoints WinBall(FortyPoints oppenentPoints);
    }
}
