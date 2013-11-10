using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PolyGeocoder.Geocoders.JsonEntities.Google;
using PolyGeocoder.Support;
using Location = PolyGeocoder.Support.Location;

namespace PolyGeocoder.Geocoders
{
    public class GoogleGeocoder : ISimpleGeocoder
    {
        private const string EndpointFormat = "https://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address={0}";

        private readonly IClient _client;

        public GoogleGeocoder(IClient client)
        {
            _client = client;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // generate the request URI
            string requestUri = string.Format(EndpointFormat, HttpUtility.UrlEncode(request));

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(requestUri);

            // parse the response
            var response = JsonConvert.DeserializeObject<GeocodeResponse>(Encoding.UTF8.GetString(clientResponse.Content));

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