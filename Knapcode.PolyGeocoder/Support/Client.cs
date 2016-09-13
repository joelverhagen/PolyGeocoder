using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Knapcode.PolyGeocoder.Support
{
    public class Client : IClient
    {
        private const string UserAgentHeader = "User-Agent";
        private readonly HttpClient _client;

        public Client()
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = false
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
            ClientResponse response = await GetClientResponseAsync(_client.GetAsync(requestUri)).ConfigureAwait(false);
            return await AfterGetAsync(requestUri, response).ConfigureAwait(false);
        }

        public async Task<ClientResponse> PostAsync(string requestUri, HttpContent content)
        {
            ClientResponse response = await GetClientResponseAsync(_client.PostAsync(requestUri, content)).ConfigureAwait(false);
            return await AfterPostAsync(requestUri, content, response).ConfigureAwait(false);
        }

        protected virtual Task<ClientResponse> AfterGetAsync(string requestUri, ClientResponse clientResponse)
        {
            return Task.FromResult(clientResponse);
        }

        protected virtual Task<ClientResponse> AfterPostAsync(string requestUri, HttpContent content, ClientResponse clientResponse)
        {
            return Task.FromResult(clientResponse);
        }

        private async Task<ClientResponse> GetClientResponseAsync(Task<HttpResponseMessage> responseTask)
        {
            // get the response
            HttpResponseMessage response = await responseTask.ConfigureAwait(false);

            // get the content
            HttpContent httpContent = response.Content;
            byte[] content = await httpContent.ReadAsByteArrayAsync().ConfigureAwait(false);

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