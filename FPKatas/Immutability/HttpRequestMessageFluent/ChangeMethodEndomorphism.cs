using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FPKatas.Immutability
{
    public class ChangeMethodEndomorphism : IEndomorphism<HttpRequestMessageBuilder>
    {
        private readonly HttpMethod newMethod;

        public ChangeMethodEndomorphism(HttpMethod newMethod)
        {
            this.newMethod = newMethod;
        }

        public HttpRequestMessageBuilder Run(HttpRequestMessageBuilder x)
        {
            if (x is null)
                throw new ArgumentNullException(nameof(x));
            return x.WithMethod(newMethod);
        }
    }
}
