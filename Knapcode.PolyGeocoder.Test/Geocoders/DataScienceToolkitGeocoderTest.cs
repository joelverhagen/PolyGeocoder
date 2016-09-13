using System.Threading.Tasks;
using Knapcode.PolyGeocoder.Geocoders;
using Knapcode.PolyGeocoder.Support;
using Xunit;

namespace Knapcode.PolyGeocoder.Test.Geocoders
{
    public class DataScienceToolkitGeocoderTest
    {
        [Fact]
        public async Task Integration()
        {
            // Arrange
            var client = new Client();
            var target = new DataScienceToolkitGeocoder(client);

            // Act
            var location = await target.GeocodeAsync("1600 pennsylvania ave nw washington dc 20500");

            // Assert
            Assert.NotNull(location);
            Assert.Equal(1, location.Locations.Length);
            Assert.Equal("1600 Pennsylvania Ave NW, Washington, DC, United States", location.Locations[0].Name);
            Assert.Equal(38.90, location.Locations[0].Latitude, 2);
            Assert.Equal(-77.04, location.Locations[0].Longitude, 2);
        }
    }
}
