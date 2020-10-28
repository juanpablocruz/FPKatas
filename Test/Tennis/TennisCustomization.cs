using AutoFixture;
using AutoFixture.AutoMoq;

namespace Test.Tennis
{
    public class TennisCustomization : CompositeCustomization
    {
        public TennisCustomization()
            : base(new AutoMoqCustomization())
        { }
    }
}
