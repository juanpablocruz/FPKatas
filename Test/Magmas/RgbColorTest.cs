using FPKatas.Magmas;
using System.Linq;
using Xunit;

namespace Test.Magmas
{
    public class RgbColorTest
    {
        // If not associative then its not a semigroup nor a monoid
        [Fact]
        public void MixWithIsNotAssociative()
        {
            // Counter-example
            var x = new RgbColor(67, 108, 13);
            var y = new RgbColor(33, 114, 130);
            var z = new RgbColor(38, 104, 245);

            Assert.NotEqual(
                x.MixWith(y).MixWith(z),
                x.MixWith(y.MixWith(z)));
        }

        // If not invertible then its not a quasigroup
        [Fact]
        public void MixWithIsNotInvertible()
        {
            // Counter-example
            var a = new RgbColor(94, 35, 172);
            var b = new RgbColor(151, 185, 7);

            Assert.DoesNotContain(RgbColor.All, x => a.MixWith(x) == b);
            Assert.DoesNotContain(RgbColor.All, y => y.MixWith(a) == b);
        }

        [Fact]
        public void MixWithHasNoIdentity()
        {
            var nearBlack = new RgbColor(1, 1, 1);

            var identityCandidates = from e in RgbColor.All
                                     where nearBlack.MixWith(e) == nearBlack
                                     select e;
            // Verify that there's only a single candidate:
            var identityCandidate = Assert.Single(identityCandidates);
            // Demonstrate that the candidate does behave like identity for
            // nearBlack:
            Assert.Equal(nearBlack, nearBlack.MixWith(identityCandidate));
            Assert.Equal(nearBlack, identityCandidate.MixWith(nearBlack));

            // Counter-example
            var counterExample = new RgbColor(3, 3, 3);
            Assert.NotEqual(
                counterExample,
                counterExample.MixWith(identityCandidate));
        }

        [Fact]
        public void MixWithIsCommutative()
        {
            var x = new RgbColor(67, 108, 13);
            var y = new RgbColor(33, 114, 130);

            Assert.Equal(
                x.MixWith(y),
                y.MixWith(x));
        }
    }


}
