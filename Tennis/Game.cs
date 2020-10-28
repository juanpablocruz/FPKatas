using System;
using System.Collections.Generic;
using System.Text;

namespace Tennis
{
    public class Game
    {
        private readonly IPoints playerOneScore;
        private readonly IPoints playerTwoScore;

        public Game()
            : this(new ZeroPoints(), new ZeroPoints())
        { }

        public Game(IPoints playerOneScore, IPoints playerTwoScore)
        {
            this.playerOneScore = playerOneScore;
            this.playerTwoScore = playerTwoScore;
        }

        public IPoints PlayerOneScore
        {
            get { return this.playerOneScore; }
        }

        public IPoints PlayerTwoScore
        {
            get { return this.playerTwoScore; }
        }

        public Game PlayerOneWinsBall()
        {
            var newPlayerOnePoints = this.PlayerTwoScore
                .Accept(this.PlayerOneScore);

            var newPlayerTwoPoints = this.PlayerTwoScore.LoseBall();

            return new Game(newPlayerOnePoints, newPlayerTwoPoints);
        }

        public Game PlayerTwoWinsBall()
        {
            var newPlayerOnePoints = this.PlayerOneScore.LoseBall();
            var newPlayerTwoPoints = this.PlayerOneScore.Accept(this.playerTwoScore);
            return new Game(newPlayerOnePoints, newPlayerTwoPoints);
        }
    }
}
