using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tennis;
using Xunit;

namespace Test.Tennis
{
    public class GameFacts
    {
        [Theory, AutoData]
        public void InitialPlayerOneScoreIsCorrect(Game sut)
        {
            Assert.IsAssignableFrom<ZeroPoints>(sut.PlayerOneScore);
        }

        [Theory, AutoData]
        public void InitialPlayerTwoScoreisCorrect(Game sut)
        {
            Assert.IsAssignableFrom<ZeroPoints>(sut.PlayerTwoScore);
        }

        [Theory, AutoData]
        public void PlayerOneWinsFirstBallReturnsSutWithCorrectScoreForPlayerOne(Game sut)
        {
            Game result = sut.PlayerOneWinsBall();
            Assert.IsAssignableFrom<FifteenPoints>(result.PlayerOneScore);
        }

        [Theory, AutoData]
        public void PlayerOneWinsFirstBallReturnsSutWithCorrectScoreForPlayerTwo(Game sut)
        {
            var expectedScore = sut.PlayerTwoScore;
            var result = sut.PlayerOneWinsBall();
            Assert.Equal(expectedScore, result.PlayerTwoScore);
        }

        [Theory, AutoData]
        public void PlayerOneWinsFirstBallDoesNotModifyOriginalSut(Game sut)
        {
            var expectedScore = sut.PlayerOneScore;
            sut.PlayerOneWinsBall();
            Assert.Equal(expectedScore, sut.PlayerOneScore);
        }

        [Theory, AutoData]
        public void PlayerTwoWinsFirstBallReturnsSutWithCorrectScoreForPlayerTwo(Game sut)
        {
            Game result = sut.PlayerTwoWinsBall();
            Assert.IsAssignableFrom<FifteenPoints>(result.PlayerTwoScore);
        }

        [Theory, AutoData]
        public void PlayerTwoWinsFirstBallReturnsSutWithCorrectScoreForPlayerOne(Game sut)
        {
            var expectedScore = sut.PlayerOneScore;
            var result = sut.PlayerTwoWinsBall();
            Assert.Equal(expectedScore, result.PlayerOneScore);
        }

        [Theory, AutoData]
        public void PlayerTwoWinsFirstBallDoesNotModifyOriginalSut(Game sut)
        {
            var expectedScore = sut.PlayerTwoScore;
            sut.PlayerTwoWinsBall();
            Assert.Equal(expectedScore, sut.PlayerTwoScore);
        }

        [Theory, AutoTennisData]
        public void PlayerOneWinsReturnsResultWithCorrectScoreForPlayerOne([Greedy]Game sut, IPoints newPoints)
        {
            Mock.Get(sut.PlayerTwoScore).Setup(p => p.Accept(sut.PlayerOneScore)).Returns(newPoints);
            var result = sut.PlayerOneWinsBall();
            Assert.Equal(newPoints, result.PlayerOneScore);
        }

        [Theory, AutoTennisData]
        public void PlayerOneWinsReturnsResultWithCorrectScoreForPlayerTwo([Greedy]Game sut, IPoints newPoints)
        {
            Mock.Get(sut.PlayerTwoScore).Setup(p => p.LoseBall()).Returns(newPoints);
            var result = sut.PlayerOneWinsBall();
            Assert.Equal(newPoints, result.PlayerTwoScore);
        }

        [Theory, AutoTennisData]
        public void PlayerTwoWinsReturnsResultWithCorrectScoreForPlayerTwo([Greedy]Game sut, IPoints newPoints)
        {
            Mock.Get(sut.PlayerOneScore).Setup(p => p.Accept(sut.PlayerTwoScore)).Returns(newPoints);
            var result = sut.PlayerTwoWinsBall();
            Assert.Equal(newPoints, result.PlayerTwoScore);
        }

        [Theory, AutoTennisData]
        public void PlayerTwoWinsReturnsResultWithCorrectScoreForPlayerOne([Greedy]Game sut, IPoints newPoints)
        {
            Mock.Get(sut.PlayerOneScore).Setup(p => p.LoseBall()).Returns(newPoints);
            var result = sut.PlayerTwoWinsBall();
            Assert.Equal(newPoints, result.PlayerOneScore);
        }
    }
}
