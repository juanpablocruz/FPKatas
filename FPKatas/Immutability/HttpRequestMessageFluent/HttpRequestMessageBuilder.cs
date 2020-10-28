using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FPKatas.Immutability
{
    public class HttpRequestMessageBuilder : IHttpRequestMessageBuilder
    {
        private readonly Uri url;
        private readonly object? jsonBody;
        public HttpMethod Method { get; }

        public HttpRequestMessageBuilder(string url) : this(new Uri(url)) { }

        public HttpRequestMessageBuilder(Uri url) : this(url, HttpMethod.Get, null) { }

        private HttpRequestMessageBuilder(Uri url, HttpMethod method, object? jsonBody)
        {
            this.url = url;
            Method = method;
            this.jsonBody = jsonBody;
        }

        public HttpRequestMessageBuilder WithMethod(HttpMethod newMethod)
        {
            return new HttpRequestMessageBuilder(url, newMethod, jsonBody);
        }

        public HttpRequestMessageBuilder AddJsonBody(object jsonBody)
        {
            return new HttpRequestMessageBuilder(url, Method, jsonBody);
        }

        public HttpRequestMessage Build()
        {
            var message = new HttpRequestMessage(Method, url);
            BuildBody(message);
            return message;
        }

        private void BuildBody(HttpRequestMessage message)
        {
            if (jsonBody is null)
                return;

            string json = JsonConvert.SerializeObject(jsonBody);
            message.Content = new StringContent(json);
            message.Content.Headers.ContentType.MediaType = "application/json";
        }
    }
}
