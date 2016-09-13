using Newtonsoft.Json;

namespace Knapcode.PolyGeocoder.Geocoders.ExternalEntities.Bing
{
    public class Point
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public double[] Coordinates { get; set; }
    }

    public class Address
    {
        [JsonProperty(PropertyName = "addressLine")]
        public string AddressLine { get; set; }

        [JsonProperty(PropertyName = "adminDistrict")]
        public string AdminDistrict { get; set; }

        [JsonProperty(PropertyName = "adminDistrict2")]
        public string AdminDistrict2 { get; set; }

        [JsonProperty(PropertyName = "countryRegion")]
        public string CountryRegion { get; set; }

        [JsonProperty(PropertyName = "formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }
    }

    public class GeocodePoint
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public double[] Coordinates { get; set; }

        [JsonProperty(PropertyName = "calculationMethod")]
        public string CalculationMethod { get; set; }

        [JsonProperty(PropertyName = "usageTypes")]
        public string[] UsageTypes { get; set; }
    }

    public class QueryParseValue
    {
        [JsonProperty(PropertyName = "property")]
        public string Property { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    public class Resource
    {
        [JsonProperty(PropertyName = "__type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "bbox")]
        public double[] BoundingBox { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "point")]
        public Point Point { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public string Confidence { get; set; }

        [JsonProperty(PropertyName = "entityType")]
        public string EntityType { get; set; }

        [JsonProperty(PropertyName = "geocodePoints")]
        public GeocodePoint[] GeocodePoints { get; set; }

        [JsonProperty(PropertyName = "matchCodes")]
        public string[] MatchCodes { get; set; }

        [JsonProperty(PropertyName = "queryParseValues")]
        public QueryParseValue[] QueryParseValues { get; set; }
    }

    public class ResourceSet
    {
        [JsonProperty(PropertyName = "estimatedTotal")]
        public int EstimatedTotal { get; set; }

        [JsonProperty(PropertyName = "resources")]
        public Resource[] Resources { get; set; }
    }

    public class Response
    {
        [JsonProperty(PropertyName = "authenticationResultCode")]
        public string AuthenticationResultCode { get; set; }

        [JsonProperty(PropertyName = "brandLogoUri")]
        public string BrandLogoUri { get; set; }

        [JsonProperty(PropertyName = "copyright")]
        public string Copyright { get; set; }

        [JsonProperty(PropertyName = "resourceSets")]
        public ResourceSet[] ResourceSets { get; set; }

        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty(PropertyName = "traceId")]
        public string TraceId { get; set; }
    }
}