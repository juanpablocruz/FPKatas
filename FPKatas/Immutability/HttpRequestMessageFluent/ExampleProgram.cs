using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace FPKatas.Immutability.HttpRequestMessageFluent
{
    public class ExampleProgram
    {
        public void Main()
        {
            IEndomorphism<HttpRequestMessageBuilder> morphism = new AppendEndomorphism<HttpRequestMessageBuilder>(
                new ChangeMethodEndomorphism(HttpMethod.Post),
                new AddJsonBodyEndomorphism(new
                {
                    id = Guid.NewGuid(),
                    date = "2020-05-20 9:00:00",
                    quantity = 1
                }));

            string url = "https://sgs.com";
            HttpRequestMessage msg = morphism.Run(new HttpRequestMessageBuilder(url)).Build();
        }
    }
}
