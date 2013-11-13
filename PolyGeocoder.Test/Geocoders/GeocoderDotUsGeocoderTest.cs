using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolyGeocoder.Geocoders;
using PolyGeocoder.Support;

namespace PolyGeocoder.Test.Geocoders
{
    [TestClass]
    public class GeocoderDotUsGeocoderTest : GeocoderTest
    {
        [TestMethod]
        public void HappyPath()
        {
            // ARRANGE
            IClient client = GetClientAlwaysReturningLines(new[]
            {
                "number=1600,prefix=,street=Pennsylvania,type=Ave,suffix=NW,city=Washington,state=DC,zip=20500,original address",
                "lat=38.898748,long=-77.037684,number=1600,prefix=,street=Pennsylvania,type=Ave,suffix=NW,city=Washington,state=DC,zip=20502,geocoder modified"
            });
            ISimpleGeocoder geocoder = new GeocoderDotUsGeocoder(client);

            // ACT
            Response response = geocoder.GeocodeAsync(null).Result;

            // ASSERT
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Locations);
            Assert.AreEqual(1, response.Locations.Length);
            Location location = response.Locations[0];
            Assert.AreEqual(38.898748, location.Latitude);
            Assert.AreEqual(-77.037684, location.Longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionOnHttpError()
        {
            // ARRANGE
            IClient client = GetClientAlwaysFailing();
            ISimpleGeocoder geocoder = new GeocoderDotUsGeocoder(client);

            // ACT
            try
            {
                geocoder.GeocodeAsync(null).Wait();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException;
            }
        }
    }
}