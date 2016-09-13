using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using PolyGeocoder.Geocoders.ExternalEntities.GeocoderDotUs;
using PolyGeocoder.Support;
using Response = PolyGeocoder.Support.Response;

namespace PolyGeocoder.Geocoders
{
    public class GeocoderDotUsGeocoder : ISimpleGeocoder
    {
        private const string Endpoint = "http://rpc.geocoder.us/service/namedcsv";

        private readonly IClient _client;

        public GeocoderDotUsGeocoder(IClient client)
        {
            _client = client;
        }

        public async Task<Response> GeocodeAsync(string request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // generate the request URI
            var query = new Dictionary<string, string>();
            query["parse_address"] = "1";
            query["address"] = request;
            var requestUri = QueryHelpers.AddQueryString(Endpoint, query);

            // get the response
            ClientResponse clientResponse = await _client.GetAsync(requestUri).ConfigureAwait(false);

            // parse the response
            ExternalEntities.GeocoderDotUs.Response parsedResponse = ParseResponse(clientResponse);

            return new Response
            {
                Locations = parsedResponse.LocatedAddresses.Select(l => new Location
                {
                    Name = ConstructName(l),
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                }).ToArray()
            };
        }

        private IDictionary<string, string> ParseLine(string line)
        {
            MatchCollection matchCollection = Regex.Matches(line, @"(?<Key>\w+)=(?<Value>.*?),");
            IDictionary<string, string> details = new Dictionary<string, string>();
            foreach (Match match in matchCollection)
            {
                details[match.Groups["Key"].Value] = match.Groups["Value"].Value;
            }
            return details;
        }

        private T PopulateLocatedAddress<T>(T locationAddress, IDictionary<string, string> details) where T : LocatedAddress
        {
            locationAddress = PopulateAddress(locationAddress, details);
            locationAddress.Latitude = double.Parse(details["lat"]);
            locationAddress.Longitude = double.Parse(details["long"]);

            return locationAddress;
        }

        private T PopulateAddress<T>(T address, IDictionary<string, string> details) where T : Address
        {
            address.Number = details["number"];
            address.Prefix = details["prefix"];
            address.Street = details["street"];
            address.Type = details["type"];
            address.Suffix = details["suffix"];
            address.City = details["city"];
            address.State = details["state"];
            address.Zip = details["zip"];

            return address;
        }

        private ExternalEntities.GeocoderDotUs.Response ParseResponse(ClientResponse clientResponse)
        {
            // make sure we have a valid status code
            if (clientResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException(string.Format("The provided HTTP status code is not 'OK'. Instead, '{0}' was provided.", clientResponse.StatusCode), "clientResponse");
            }

            // convert the bytes to UTF-8... because that's probably the encoding?
            string content = Encoding.UTF8.GetString(clientResponse.Content);

            // parse the string content
            Address originalAddress = null;
            IList<LocatedAddress> locatedAddresses = new List<LocatedAddress>();
            using (var reader = new StringReader(content))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    IDictionary<string, string> details = ParseLine(line);
                    if (line.EndsWith(",original address"))
                    {
                        originalAddress = PopulateAddress(new Address(), details);
                    }
                    else if (line.EndsWith(",geocoder modified"))
                    {
                        locatedAddresses.Add(PopulateLocatedAddress(new LocatedAddress(), details));
                    }
                }
            }

            return new ExternalEntities.GeocoderDotUs.Response
            {
                OriginalAddress = originalAddress,
                LocatedAddresses = locatedAddresses.ToArray()
            };
        }

        private string ConstructName(Address address)
        {
            string streetName = string.Join(" ", new[]
            {
                address.Number,
                address.Prefix,
                address.Street,
                address.Type,
                address.Suffix
            });

            return string.Join(", ", new[]
            {
                streetName,
                address.City,
                address.State,
                address.Zip
            }
                .Select(s => s.Trim())
                .Where(s => s.Length > 0));
        }
    }
}