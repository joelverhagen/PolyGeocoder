using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PolyGeocoder.Support;

namespace PolyGeocoder.Test.Geocoders
{
    public class GeocoderTest
    {
        public IClient GetClientAlwaysReturningLines(IEnumerable<string> lines)
        {
            return GetClientAlwaysReturningClientResponse(new ClientResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines))
            });
        }

        public IClient GetClientAlwaysFailing()
        {
            return GetClientAlwaysReturningClientResponse(new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
        }

        public IClient GetClientAlwaysReturningClientResponse(ClientResponse clientResponse)
        {
            var mock = new Mock<IClient>();
            mock
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(clientResponse));
            return mock.Object;
        }
    }
}