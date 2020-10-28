using FPKatas.Hull;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test.GeometryTest
{
    using static Geometry;

    public class GeometryTest
    {
        [Theory]
        [InlineData(0, 0, 1, 0, 1, 1, Direction.Left)]
        [InlineData(0, 0, -1, -1, 1, -1, Direction.Left)]
        [InlineData(1, 1, 2, 2, 3, 2, Direction.Right)]
        [InlineData(-2, -3, 2, 2, -3, -9, Direction.Right)]
        [InlineData(1, 1, 2, 2, 3, 3, Direction.Straight)]
        [InlineData(-2, 0, 0, 1, 2, 2, Direction.Straight)]
        public void TurnReturnsCorrectResult(
            float x1, 
            float y1, 
            float x2, 
            float y2,
            float x3,
            float y3,
            Direction expected)
        {
            var actual = turn(
                new Point { x = x1, y = y1 },
                new Point { x = x2, y = y2 },
                new Point { x = x3, y = y3 }
                );
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(HullData))]
        public void HullReturnsCorrectResults(
            IEnumerable<Point> points,
            Hull expected)
        {
            Hull actual = hull(points);
            Assert.Equal(expected.data, actual.data);
        }

        private Point[] GenRandomData()
        {
            Random rnd = new Random();

            var x = new List<Point>();
            for (var i = 0; i < 100; i++)
                x.Add((rnd.Next(-100, 100), rnd.Next(-100, 100)));

            return x.ToArray();
        }

        [Fact]
        public void HullAdditionIsAssociative()
        {
            var (x, y, z) = (GenRandomData(), GenRandomData(), GenRandomData());
            Assert.Equal(
                ((hull(x) + hull(y)) + hull(z)).data,
                (hull(x) + (hull(y) + hull(z))).data
            );
        }

        [Fact]
        public void HullAdditionHasIdentity()
        {
            var x = GenRandomData();

            Assert.True(Identity + hull(x) == hull(x) + Identity &&
                                hull(x) + Identity == hull(x));
        }
    }
}
