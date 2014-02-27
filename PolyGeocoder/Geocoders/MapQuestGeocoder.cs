using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PolyGeocoder.Support;
using Location = PolyGeocoder.Geocoders.ExternalEntities.MapQuest.Location;

namespace PolyGeocoder.Geocoders
{
    public class MapQuestGeocoder : ISimpleGeocoder
    {
        public const string LicensedEndpoint = "http://www.mapquestapi.com/geocoding/v1/address";
        public const string OpenEndpoint = "http://open.mapquestapi.com/geocoding/v1/address";

        private readonly string _key;
        private readonly IClient _client;
        private readonly string _endpoint;

        public MapQuestGeocoder(IClient client, string key) : this(client, key, LicensedEndpoint)
        {
        }

        public MapQuestGeocoder(IClient client, string endpoint, string key)
        {
            _client = client;
            _key = key;
            _endpoint = endpoint;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // generate the request URI
            var builder = new UriBuilder(_endpoint);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["location"] = request;
            query["outFormat"] = "json";
            query["key"] = _key;
            builder.Query = query.ToString();

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(builder.ToString()).ConfigureAwait(false);

            // parse the response
            string content = Encoding.UTF8.GetString(clientResponse.Content);
            var response = JsonConvert.DeserializeObject<ExternalEntities.MapQuest.Response>(content);

            // project the response
            return new Response
            {
                Locations = response.Results.Single().Locations.Select(l => new Support.Location
                {
                    Name = ConstructName(l),
                    Latitude = l.Point.Latitude,
                    Longitude = l.Point.Longitude
                }).ToArray()
            };
        }

        private string ConstructName(Location location)
        {
            return string.Join(", ", new[]
            {
                location.Street,
                location.AdminArea5,
                location.AdminArea4,
                location.AdminArea3,
                location.AdminArea2,
                location.AdminArea1,
                location.PostalCode
            }
                .Select(s => (s ?? string.Empty).Trim())
                .Where(s => s.Length > 0));
        }
    }
}