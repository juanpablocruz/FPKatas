using System;
using System.Collections.Generic;
using System.Text;
using Tennis;
using Xunit;

namespace Test.Tennis
{
    public class AdvantagePointFacts
    {
        [Theory, AutoTennisData]
        public void SutIsPoints(AdvantagePoint sut)
        {
            Assert.IsAssignableFrom<IPoints>(sut);
        }
    }
}
