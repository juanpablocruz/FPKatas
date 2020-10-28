using FPKatas.Hull;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Test.GeometryTest
{

    public class HullData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            
            yield return new object[] {
                new Point[] { (3, 1), (3, 7), (2, 5), (2, 4), (1, 6), (2, 3), (1, 2) },
                new Point[] { (3, 1), (3, 7), (1, 6), (1, 2) }
            };

            yield return new object[] {
                new Point[] { (1, -4), (2, 5), (1, 3), (1, -3), (1, -2), (0, 4) },
                new Point[] { (1, -4), (2, 5), (0, 4) }
            };

            yield return new object[] {
                new Point[] { (1, 1), (0, 3), (-2, 1), (-4, 3), (5, 2), (3, 2), (5, 5), (2, 5), (1, 3), (1, -3), (1, -2), (7, -4), (-1, 1), (-3, 0), (-5, -2), (1, -4), (0, 1), (0, 4), (3, -3), (6, 1) },
                new Point[] { (1, -4), (7, -4), (6, 1), (5, 5), (2, 5), (-4, 3), (-5, -2) }
            };

            yield return new object[] {
                new Point[] { (-7, -7), (4, -7), (2, 3), (4, 4), (3, 1), (2, -1), (-3, -5), (4, -2), (-1, -7), (-6, 9), (4, 4), (-8, -2), (9, 4), (3, 0), (7, 0), (-7, 3), (0, 9), (4, -7), (-7, -6), (-1, 7), (6, 5), (7, -3), (-8, -8), (-6, -2), (3, 5), (-5, 7), (8, 1), (3, -2), (-9, -4), (-7, 8) },
                new Point[] { (-8, -8), (4, -7), (7, -3), (9, 4), (0, 9), (-6, 9), (-7, 8), (-9, -4) }
            };
            
            yield return new object[]
            {
                new Point[]{ },
                new Point[]{ },
            };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
