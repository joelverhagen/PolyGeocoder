using Newtonsoft.Json;

namespace Knapcode.PolyGeocoder.ExternalEntities.OpenStreetMap
{
    public class Address
    {
        [JsonProperty(PropertyName = "house_number")]
        public string HouseNumber { get; set; }

        [JsonProperty(PropertyName = "road")]
        public string Road { get; set; }

        [JsonProperty(PropertyName = "suburb")]
        public string Suburb { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "county")]
        public string County { get; set; }

        [JsonProperty(PropertyName = "state_district")]
        public string StateDistrict { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }
    }

    public class Place
    {
        [JsonProperty(PropertyName = "place_id")]
        public string PlaceId { get; set; }

        [JsonProperty(PropertyName = "licence")]
        public string Licence { get; set; }

        [JsonProperty(PropertyName = "osm_type")]
        public string OsmType { get; set; }

        [JsonProperty(PropertyName = "osm_id")]
        public string OsmId { get; set; }

        [JsonProperty(PropertyName = "boundingbox")]
        public double[] BoundingBox { get; set; }

        [JsonProperty(PropertyName = "polygonpoints")]
        public double[][] PolygonPoints { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "place_rank")]
        public string PlaceRank { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "importance")]
        public double Importance { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}