using Newtonsoft.Json;

namespace PolyGeocoder.Geocoders.JsonEntities.DataScienceToolkit
{
    public class Location
    {
        [JsonProperty(PropertyName = "country_code3")]
        public string CountryCode3 { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        [JsonProperty(PropertyName = "street_number")]
        public string StreetNumber { get; set; }

        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        [JsonProperty(PropertyName = "street_name")]
        public string StreetName { get; set; }

        [JsonProperty(PropertyName = "fips_county")]
        public string FipsCounty { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }
    }
}