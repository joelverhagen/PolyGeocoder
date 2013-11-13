using Newtonsoft.Json;

namespace PolyGeocoder.Geocoders.ExternalEntities.MapQuest
{
    public class Point
    {
        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "latLng")]
        public Point Point { get; set; }

        [JsonProperty(PropertyName = "adminArea4")]
        public string AdminArea4 { get; set; }

        [JsonProperty(PropertyName = "adminArea5Type")]
        public string AdminArea5Type { get; set; }

        [JsonProperty(PropertyName = "adminArea4Type")]
        public string AdminArea4Type { get; set; }

        [JsonProperty(PropertyName = "adminArea5")]
        public string AdminArea5 { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "adminArea1")]
        public string AdminArea1 { get; set; }

        [JsonProperty(PropertyName = "adminArea2")]
        public string AdminArea2 { get; set; }

        [JsonProperty(PropertyName = "adminArea2Type")]
        public string AdminArea2Type { get; set; }

        [JsonProperty(PropertyName = "adminArea3")]
        public string AdminArea3 { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "displayLatLng")]
        public Point DisplayPoint { get; set; }

        [JsonProperty(PropertyName = "linkId")]
        public int LinkId { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "sideOfStreet")]
        public string SideOfStreet { get; set; }

        [JsonProperty(PropertyName = "dragPoint")]
        public bool IsDragPoint { get; set; }

        [JsonProperty(PropertyName = "adminArea1Type")]
        public string AdminArea1Type { get; set; }

        [JsonProperty(PropertyName = "geocodeQuality")]
        public string GeocodeQuality { get; set; }

        [JsonProperty(PropertyName = "geocodeQualityCode")]
        public string GeocodeQualityCode { get; set; }

        [JsonProperty(PropertyName = "mapUrl")]
        public string MapUrl { get; set; }

        [JsonProperty(PropertyName = "adminArea3Type")]
        public string AdminArea3Type { get; set; }
    }

    public class ProvidedLocation
    {
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "locations")]
        public Location[] Locations { get; set; }

        [JsonProperty(PropertyName = "providedLocation")]
        public ProvidedLocation ProvidedLocation { get; set; }
    }

    public class Options
    {
        [JsonProperty(PropertyName = "ignoreLatLngInput")]
        public bool IgnoreLatitudeLongitudeInput { get; set; }

        [JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty(PropertyName = "thumbMaps")]
        public bool ThumbMaps { get; set; }
    }

    public class Copyright
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "imageAltText")]
        public string ImageAltText { get; set; }
    }

    public class Info
    {
        [JsonProperty(PropertyName = "copyright")]
        public Copyright Copyright { get; set; }

        [JsonProperty(PropertyName = "statuscode")]
        public int Statuscode { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; set; }
    }

    public class Response
    {
        [JsonProperty(PropertyName = "results")]
        public Result[] Results { get; set; }

        [JsonProperty(PropertyName = "options")]
        public Options Options { get; set; }

        [JsonProperty(PropertyName = "info")]
        public Info Info { get; set; }
    }
}