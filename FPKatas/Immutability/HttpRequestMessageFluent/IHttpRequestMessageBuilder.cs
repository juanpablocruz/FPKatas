using System.Net.Http;

namespace FPKatas.Immutability
{
    public interface IHttpRequestMessageBuilder
    {
        HttpRequestMessage Build();
        HttpRequestMessageBuilder WithMethod(HttpMethod newMethod);
        HttpRequestMessageBuilder AddJsonBody(object jsonBody);
    }
}
