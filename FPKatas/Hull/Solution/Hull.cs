using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.Hull
{
    using static F;
    public enum Direction { Left = -1, Straight = 0, Right = 1};


    public struct Point
    {
        public float x { get; set; }
        public float y { get; set; }

        public static implicit operator Point ((int, int) data)
            => new Point() { x = data.Item1, y = data.Item2};

        public static implicit operator Point((float, float) data)
            => new Point() { x = data.Item1, y = data.Item2 };
    }

    public struct Hull
    {
        public IEnumerable<Point> data { get; set; }

        public static implicit operator Hull(List<Point> points)
            => new Hull { data = points};

        public static implicit operator Hull(Point[] points)
            => new Hull { data = points };

        public static implicit operator Point[](Hull points)
            => points.data.ToArray();

        public static Hull operator + (Hull a, Hull b)
        {
            return Geometry.hull(a.data.Concat(b.data));
        }

        public static bool operator == (Hull a, Hull b)
        {
            return a.data.SequenceEqual(b.data);
        }

        public static bool operator !=(Hull a, Hull b)
        {
            return !(a == b);
        }
    }

    public static class Geometry
    {

        public static Hull Identity = new Hull { data = new List<Point> { } };

        public static Direction turn(Point p1, Point p2, Point p3)
        {
            var prod = (p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x);

            if (prod > 0)
                return Direction.Left;
            else if (prod < 0)
                return Direction.Right;
            else
                return Direction.Straight;
        }


        public static Hull hull(IEnumerable<Point> points)
        {
            

            int Compare(Point p1, Point p2)
            {
                if (p1.x > p2.x)
                    return 1;
                if (p1.x < p2.x)
                    return -1;
                return 0;
            }

            int CompareLexigraphic(Point p1, Point p2)
            {
                return Compare( (p1.y, p1.x), (p2.y, p2.x));
            }
            
            int ComparePolar(Point p1, Point p2, Point p3)
                 => (int) turn(p1, p2, p3);

            Maybe<IEnumerable<Point>> TryDiscard(IEnumerable<Point> points)
            {
                Func<IEnumerable<Point>, IEnumerable<Point>> TryDiscardImp(IEnumerable<Point> acc)
                    => (val) => {
                        var list = val.ToArray();

                        if (list.Count() == 3)
                        {
                            return (turn(list[0], list[1], list[2]) == Direction.Right) 
                                ? (new List<Point> { list[2], list[0] }).Concat(acc)
                                : (new List<Point> { list[2], list[1], list[0] }).Concat(acc);
                        }

                        if (val.Count() == 0)
                            return acc;

                        return TryDiscardImp((new List<Point> { list[0] }).Concat(acc))(list[1..]);
                    };

                return TryDiscardImp(Identity.data)(points).Reverse()
                    .Pipe(newPoints => (newPoints.Count() != points.Count()) 
                            ? Just(newPoints) 
                            : Nothing
                        );
            };

            IEnumerable<Point> DiscardFrom(IEnumerable<Point> candidates)
                => TryDiscard(candidates).Match(
                    Nothing: () => candidates,
                    Just: newCandidates => DiscardFrom(newCandidates)
                    );

            Func<Point[], IEnumerable<Point>> HullPoints(IEnumerable<Point> candidates)
                => (newPoints) 
                => (newPoints.Count() == 0)
                    ? candidates
                    : HullPoints(DiscardFrom(candidates.Concat(new[] { newPoints[0] })))(newPoints[1..]);

            if (points.Count() == 0)
                return Identity;
            
            var asList = points.ToList();
            asList.Sort(CompareLexigraphic);

            var p0 = asList[0];

            int Cmp(Point p1, Point p2)
                => ComparePolar(p0, p1, p2)
                    .Pipe(polar => 
                        polar == 0 ? CompareLexigraphic(p1, p2) 
                        : polar
                    );
            
            var sortedPoints = points.ToList();
            sortedPoints.Sort(Cmp);

            return HullPoints(Identity.data)(sortedPoints.Distinct().ToArray()).ToArray();
        }
    }
}
