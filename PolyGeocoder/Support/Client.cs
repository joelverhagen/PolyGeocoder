using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PolyGeocoder.Support
{
    public class Client : IClient
    {
        private const string UserAgentHeader = "User-Agent";
        private readonly HttpClient _client;

        public Client()
        {
            _client = new HttpClient(new WebRequestHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = false,
                AllowPipelining = true,
            });
        }

        public Client(string userAgent) : this()
        {
            UserAgent = userAgent;
        }

        public string UserAgent
        {
            get
            {
                IEnumerable<string> values;
                if (!_client.DefaultRequestHeaders.TryGetValues(UserAgentHeader, out values))
                {
                    return null;
                }
                return values.Single();
            }

            set
            {
                if (_client.DefaultRequestHeaders.Contains(UserAgentHeader))
                {
                    _client.DefaultRequestHeaders.Remove(UserAgentHeader);
                }
                _client.DefaultRequestHeaders.Add(UserAgentHeader, value);
            }
        }

        public async Task<ClientResponse> GetAsync(string requestUri)
        {
            return await GetClientResponseAsync(_client.GetAsync(requestUri));
        }

        public async Task<ClientResponse> PostAsync(string requestUri, HttpContent content)
        {
            return await GetClientResponseAsync(_client.PostAsync(requestUri, content));
        }

        private async Task<ClientResponse> GetClientResponseAsync(Task<HttpResponseMessage> responseTask)
        {
            // get the response
            HttpResponseMessage response = await responseTask;

            // get the content
            HttpContent httpContent = response.Content;
            byte[] content = await httpContent.ReadAsByteArrayAsync();

            // construct the output
            return new ClientResponse
            {
                Content = content,
                Headers = response.Headers,
                StatusCode = response.StatusCode
            };
        }
    }
}