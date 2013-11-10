﻿using System.Net.Http;
using System.Threading.Tasks;

namespace PolyGeocoder.Support
{
    public interface IClient
    {
        string UserAgent { get; set; }
        Task<ClientResponse> GetAsync(string requestUri);
        Task<ClientResponse> PostAsync(string requestUri, HttpContent content);
    }
}