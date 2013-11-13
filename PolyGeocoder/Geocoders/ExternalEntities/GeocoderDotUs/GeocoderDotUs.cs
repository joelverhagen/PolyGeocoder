namespace PolyGeocoder.Geocoders.ExternalEntities.GeocoderDotUs
{
    public class Response
    {
        public Address OriginalAddress { get; set; }
        public LocatedAddress[] LocatedAddresses { get; set; }
    }

    public class Address
    {
        public string Number { get; set; }
        public string Prefix { get; set; }
        public string Street { get; set; }
        public string Type { get; set; }
        public string Suffix { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class LocatedAddress : Address
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}