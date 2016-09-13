using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knapcode.PolyGeocoder.Geocoders.ExternalEntities.Google;
using Knapcode.PolyGeocoder.Support;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Location = Knapcode.PolyGeocoder.Support.Location;

namespace Knapcode.PolyGeocoder.Geocoders
{
    public class GoogleGeocoder : ISimpleGeocoder
    {
        private const string EndpointFormat = "https://maps.googleapis.com/maps/api/geocode/json?sensor=false&address={0}";

        private readonly IClient _client;
        private readonly string _key;

        public GoogleGeocoder(IClient client)
        {
            _client = client;
            _key = null;
        }
        public GoogleGeocoder(IClient client, string key)
        {
            _client = client;
            _key = key;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // generate the request URI
            string requestUri = string.Format(EndpointFormat, Uri.EscapeDataString(request));

            if (!string.IsNullOrEmpty(_key))
            {
                requestUri = QueryHelpers.AddQueryString(requestUri, "key", _key);
            }

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(requestUri).ConfigureAwait(false);

            // parse the response
            string content = Encoding.UTF8.GetString(clientResponse.Content);
            var response = JsonConvert.DeserializeObject<GeocodeResponse>(content);

            // project the response
            return new Response
            {
                Locations = response.Results.Select(r => new Location
                {
                    Name = r.FormattedAddress,
                    Latitude = r.Geometry.Location.Latitude,
                    Longitude = r.Geometry.Location.Longitude,
                }).ToArray()
            };
        }
    }
}