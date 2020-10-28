using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Immutability
{
    public class AddJsonBodyEndomorphism : IEndomorphism<HttpRequestMessageBuilder>
    {
        private readonly object jsonBody;

        public AddJsonBodyEndomorphism(object jsonBody)
        {
            this.jsonBody = jsonBody;
        }

        public HttpRequestMessageBuilder Run(HttpRequestMessageBuilder x)
        {
            if (x is null)
                throw new ArgumentNullException(nameof(x));
            return x.AddJsonBody(jsonBody);
        }
    }
}
