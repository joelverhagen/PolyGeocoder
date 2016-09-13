using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Knapcode.PolyGeocoder
{
    public class DataScienceToolkitGeocoder : ISimpleGeocoder
    {
        public const string Endpoint = "http://www.datasciencetoolkit.org/street2coordinates";

        private readonly IClient _client;
        private readonly string _endpoint;

        public DataScienceToolkitGeocoder(IClient client) : this(client, Endpoint)
        {
        }

        public DataScienceToolkitGeocoder(IClient client, string endpoint)
        {
            _client = client;
            _endpoint = endpoint.TrimEnd('/') + '/';
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // build the request URI
            string requestUri = _endpoint + (Uri.EscapeDataString(request) ?? string.Empty)
                .Replace("%2F", "/") // Data Science Toolkit expects the forward slashes to be plaintext
                .Replace("%2f", "/");

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(requestUri).ConfigureAwait(false);
            string content = Encoding.UTF8.GetString(clientResponse.Content);

            // parse the response
            var response = JsonConvert.DeserializeObject<IDictionary<string, ExternalEntities.DataScienceToolkit.Location>>(content);

            // project the response
            return new Response
            {
                Locations = response
                    .Where(p => p.Value != null)
                    .Select(p => new Location
                    {
                        Name = ConstructName(p.Value),
                        Latitude = p.Value.Latitude,
                        Longitude = p.Value.Longitude
                    }).ToArray()
            };
        }

        private string ConstructName(ExternalEntities.DataScienceToolkit.Location location)
        {
            return string.Join(", ", new[]
            {
                location.StreetAddress,
                location.Locality,
                location.Region,
                location.CountryName
            });
        }
    }
}