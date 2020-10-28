using Tennis;
using Xunit;

namespace Test.Tennis
{
    public class Scenario
    {
        [Fact]
        public void NewGameIsCorrect()
        {
            var game = new Game();

            Assert.Equal(new ZeroPoints(), game.PlayerOneScore);
            Assert.Equal(new ZeroPoints(), game.PlayerTwoScore);
        }

        [Fact]
        public void PlayerOneAchievesTotalVictory()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall();

            Assert.Equal(new GamePoint(), game.PlayerOneScore);
            Assert.Equal(new ZeroPoints(), game.PlayerTwoScore);
        }

        [Fact]
        public void GameIsTiedAtFortyLove()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall();

            Assert.Equal(new FortyPoints(), game.PlayerOneScore);
            Assert.Equal(new FortyPoints(), game.PlayerTwoScore);
        }

        [Fact]
        public void PlayerTwoGetsAdvantage()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall();

            Assert.Equal(new FortyPoints(), game.PlayerOneScore);
            Assert.Equal(new AdvantagePoint(), game.PlayerTwoScore);
        }

        [Fact]
        public void PlayerOneComesBack()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerOneWinsBall();

            Assert.Equal(new FortyPoints(), game.PlayerOneScore);
            Assert.Equal(new FortyPoints(), game.PlayerTwoScore);
        }

        [Fact]
        public void PlayerOneGetsAdvantage()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall();

            Assert.Equal(new AdvantagePoint(), game.PlayerOneScore);
            Assert.Equal(new FortyPoints(), game.PlayerTwoScore);
        }

        [Fact]
        public void PlayerOneWinsAfterHardFight()
        {
            var game = new Game()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerTwoWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall()
                .PlayerOneWinsBall();

            Assert.Equal(new GamePoint(), game.PlayerOneScore);
            Assert.Equal(new FortyPoints(), game.PlayerTwoScore);
        }
    }
}
