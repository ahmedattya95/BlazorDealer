using AKSoftware.WebApi.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDealer.Client
{
    public class CarTypesService
    {
        private readonly ServiceClient _client;
        public string AccessToken
        {
            get => _client.AccessToken;
            set
            {
                _client.AccessToken = value;
            }
        }
        private readonly string _baseUrl;

        public CarTypesService(string url)
        {
            _client = new ServiceClient();
            _baseUrl = url;
        }

        public async Task<HttpCollectionResponse<CarType>> GetAllCarTypesAsync()
        {
            return await _client.GetProtectedAsync<HttpCollectionResponse<CarType>>($"{_baseUrl}/cartypes");
        }
    }
}
