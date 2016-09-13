using System;
using System.Threading.Tasks;
using Knapcode.PolyGeocoder.Geocoders;
using Knapcode.PolyGeocoder.Support;
using Xunit;

namespace Knapcode.PolyGeocoder.Test.Geocoders
{
    public class GeocoderDotUsGeocoderTest
    {
        [Fact]
        public async Task HappyPath()
        {
            // ARRANGE
            IClient client = Support.GetClientAlwaysReturningLines(new[]
            {
                "number=1600,prefix=,street=Pennsylvania,type=Ave,suffix=NW,city=Washington,state=DC,zip=20500,original address",
                "lat=38.898748,long=-77.037684,number=1600,prefix=,street=Pennsylvania,type=Ave,suffix=NW,city=Washington,state=DC,zip=20502,geocoder modified"
            });
            ISimpleGeocoder geocoder = new GeocoderDotUsGeocoder(client);

            // ACT
            Response response = await geocoder.GeocodeAsync(string.Empty);

            // ASSERT
            Assert.NotNull(response);
            Assert.NotNull(response.Locations);
            Assert.Equal(1, response.Locations.Length);
            Location location = response.Locations[0];
            Assert.Equal(38.898748, location.Latitude);
            Assert.Equal(-77.037684, location.Longitude);
        }

        [Fact]
        public async Task ThrowExceptionOnHttpError()
        {
            // ARRANGE
            IClient client = Support.GetClientAlwaysFailing();
            ISimpleGeocoder geocoder = new GeocoderDotUsGeocoder(client);

            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentException>(() => geocoder.GeocodeAsync(string.Empty));
        }

        [Fact]
        public async Task ThrowExceptionOnNullInput()
        {
            // ARRANGE
            IClient client = Support.GetClientAlwaysReturningEmptyResponse();
            ISimpleGeocoder geocoder = new GeocoderDotUsGeocoder(client);

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => geocoder.GeocodeAsync(null));
            Assert.Equal("request", exception.ParamName);
        }
    }
}