﻿using System.Net;
using System.Net.Http.Headers;

namespace Knapcode.PolyGeocoder
{
    public class ClientResponse
    {
        public HttpResponseHeaders Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public byte[] Content { get; set; }
    }
}