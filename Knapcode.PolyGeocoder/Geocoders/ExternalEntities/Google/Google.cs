using Newtonsoft.Json;

namespace Knapcode.PolyGeocoder.Geocoders.ExternalEntities.Google
{
    public class AddressComponent
    {
        [JsonProperty(PropertyName = "long_name")]
        public string LongName { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "types")]
        public string[] Types { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
    }

    public class Viewport
    {
        [JsonProperty(PropertyName = "northeast")]
        public Location Northeast { get; set; }

        [JsonProperty(PropertyName = "southwest")]
        public Location Southwest { get; set; }
    }

    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        [JsonProperty(PropertyName = "location_type")]
        public string LocationType { get; set; }

        [JsonProperty(PropertyName = "viewport")]
        public Viewport Viewport { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty(PropertyName = "types")]
        public string[] Types { get; set; }
    }

    public class GeocodeResponse
    {
        [JsonProperty(PropertyName = "results")]
        public Result[] Results { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}