using AutoFixture;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Tennis
{
    public class AutoTennisDataAttribute : AutoDataAttribute
    {
        [Obsolete]
        public AutoTennisDataAttribute()
            : base(new Fixture().Customize(new TennisCustomization()))
        { }
    }
}
