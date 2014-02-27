using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PolyGeocoder.Support;

namespace PolyGeocoder.Geocoders
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
            var builder = new UriBuilder(Endpoint);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["q"] = request;
            query["o"] = "json";
            query["inclnb"] = "1";
            query["incl"] = "queryParse";
            query["maxRes"] = "20";
            query["key"] = _key;
            builder.Query = query.ToString();

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(builder.ToString()).ConfigureAwait(false);

            // parse the response
            string content = Encoding.UTF8.GetString(clientResponse.Content);
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