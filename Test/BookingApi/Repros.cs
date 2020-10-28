using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Test.BookingApi
{
    public class Repros
    {
        [Fact]
        public void CapacityEdgeCase()
        {
            var log = Log.LoadEmbeddedFile("CapacityEdgeCase.txt");
            var (sut, dto) = Log.LoadReservationsControllerPostScenario(log);

            var actual = sut.Post(dto);

            Assert.IsAssignableFrom<OkResult>(actual);
        }
    }
}
