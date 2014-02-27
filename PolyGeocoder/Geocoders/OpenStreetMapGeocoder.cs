using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PolyGeocoder.Geocoders.ExternalEntities.OpenStreetMap;
using PolyGeocoder.Support;

namespace PolyGeocoder.Geocoders
{
    public class OpenStreetMapGeocoder : ISimpleGeocoder, IStructuredGeocoder
    {
        public const string OpenScreetMapEndpoint = "http://nominatim.openstreetmap.org/search";
        public const string MapQuestEndpoint = "http://open.mapquestapi.com/nominatim/v1/search";

        private readonly IClient _client;
        private readonly string _endpoint;

        public OpenStreetMapGeocoder(IClient client) : this(client, OpenScreetMapEndpoint)
        {
        }

        public OpenStreetMapGeocoder(IClient client, string endpoint)
        {
            _client = client;
            _endpoint = endpoint;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            // construct the query
            IDictionary<string, string> query = new Dictionary<string, string>();
            query["q"] = request;

            // geocode
            return await GeocodeAsync(query).ConfigureAwait(false);
        }

        public async Task<Response> GeocodeAsync(StructuredRequest request)
        {
            // construct the query
            IDictionary<string, string> query = new Dictionary<string, string>();
            IList<string> streetPieces = new List<string>();
            if (request.Number != null)
            {
                streetPieces.Add(request.Number);
            }
            if (request.Street != null)
            {
                streetPieces.Add(request.Street);
            }
            if (streetPieces.Count > 0)
            {
                query["street"] = string.Join(" ", streetPieces);
            }

            if (request.City != null)
            {
                query["city"] = request.City;
            }
            if (request.County != null)
            {
                query["county"] = request.County;
            }
            if (request.State != null)
            {
                query["state"] = request.State;
            }
            if (request.Country != null)
            {
                query["country"] = request.Country;
            }
            if (request.PostalCode != null)
            {
                query["postalcode"] = request.PostalCode;
            }

            // geocode
            return await GeocodeAsync(query).ConfigureAwait(false);
        }

        private async Task<Response> GeocodeAsync(IDictionary<string, string> query)
        {
            // build the request URI
            var builder = new UriBuilder(_endpoint);
            NameValueCollection queryObject = HttpUtility.ParseQueryString(builder.Query);
            foreach (string key in query.Keys)
            {
                queryObject[key] = query[key];
            }
            queryObject["format"] = "jsonv2";
            queryObject["polygon"] = "1";
            queryObject["addressdetails"] = "1";
            builder.Query = queryObject.ToString();

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(builder.ToString()).ConfigureAwait(false);

            // parse the response
            string content = Encoding.UTF8.GetString(clientResponse.Content);
            var places = JsonConvert.DeserializeObject<Place[]>(content);

            // project the response
            return new Response
            {
                Locations = places.Select(p => new Location
                {
                    Name = p.DisplayName,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude
                }).ToArray()
            };
        }
    }
}