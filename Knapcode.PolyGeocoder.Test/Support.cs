using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Knapcode.PolyGeocoder;
using Moq;

namespace Knapcode.PolyGeocoder.Test.Geocoders
{
    public static class Support
    {
        public static IClient GetClientAlwaysReturningLines(IEnumerable<string> lines)
        {
            return GetClientAlwaysReturningClientResponse(new ClientResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines))
            });
        }

        public static IClient GetClientAlwaysFailing()
        {
            return GetClientAlwaysReturningClientResponse(new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
        }

        public static IClient GetClientAlwaysReturningClientResponse(ClientResponse clientResponse)
        {
            var mock = new Mock<IClient>();
            mock
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(clientResponse));
            return mock.Object;
        }

        public static IClient GetClientAlwaysReturningEmptyResponse()
        {
            return GetClientAlwaysReturningClientResponse(new ClientResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = Encoding.UTF8.GetBytes(string.Empty)
            });
        }
    }
}