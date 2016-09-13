using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knapcode.PolyGeocoder;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Knapcode.PolyGeocoder
{
    public class BingGeocoder : ISimpleGeocoder
    {
        private const string Endpoint = "http://dev.virtualearth.net/REST/v1/Locations";

        private readonly IClient _client;
        private readonly string _key;

        public BingGeocoder(IClient client, string key)
        {
            _client = client;
            _key = key;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // generate the request URI
            var query = new Dictionary<string, string>();
            query["q"] = request;
            query["o"] = "json";
            query["inclnb"] = "1";
            query["incl"] = "queryParse";
            query["maxRes"] = "20";
            query["key"] = _key;
            var requestUri = QueryHelpers.AddQueryString(Endpoint, query);

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(requestUri).ConfigureAwait(false);

            // parse the response
            string content = Encoding.UTF8.GetString(clientResponse.Content, 0, clientResponse.Content.Length);
            var response = JsonConvert.DeserializeObject<ExternalEntities.Bing.Response>(content);

            // project the response
            return new Response
            {
                Locations = response
                    .ResourceSets
                    .SelectMany(s => s.Resources)
                    .Select(r => new Location
                    {
                        Name = r.Name,
                        Latitude = r.Point.Coordinates[0],
                        Longitude = r.Point.Coordinates[1]
                    }).ToArray()
            };
        }
    }
}