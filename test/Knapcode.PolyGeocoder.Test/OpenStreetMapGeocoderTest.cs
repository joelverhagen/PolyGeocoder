using System.Threading.Tasks;
using Xunit;

namespace Knapcode.PolyGeocoder.Test
{
    public class OpenStreetMapGeocoderTest
    {
        [Fact]
        public async Task Integration()
        {
            // Arrange
            var client = new Client();
            var target = new OpenStreetMapGeocoder(client);

            // Act
            var location = await target.GeocodeAsync("1600 pennsylvania ave nw washington dc 20500");

            // Assert
            Assert.NotNull(location);
            Assert.True(location.Locations.Length >= 1);
            Assert.Equal("White House, 1600, Pennsylvania Avenue Northwest, Monumental Core, Washington, District of Columbia, 20500, United States of America", location.Locations[0].Name);
            Assert.Equal(38.90, location.Locations[0].Latitude, 2);
            Assert.Equal(-77.04, location.Locations[0].Longitude, 2);
        }
    }
}
