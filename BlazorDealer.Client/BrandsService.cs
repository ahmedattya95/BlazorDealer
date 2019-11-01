using AKSoftware.WebApi.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDealer.Client
{
    public class BrandsService
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

        public BrandsService(string url)
        {
            _client = new ServiceClient();
            _baseUrl = url;
        }

        public async Task<HttpCollectionResponse<Brand>> GetAllBrandsAsync()
        {
            return await _client.GetProtectedAsync<HttpCollectionResponse<Brand>>($"{_baseUrl}/brands");
        }

        public async Task<HttpSingleResponse<Brand>> AddNewBrandAsync(BrandViewModel model, AppFile brandIcon)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

                content.Add(new StreamContent(brandIcon.FileStream, (int)brandIcon.FileStream.Length), "brandIcon", brandIcon.FileName);
                content.Add(new StringContent(model.Name), "Name");
                content.Add(new StringContent(model.Country), "Country");

                var result = await client.PostAsync($"{_baseUrl}/brands", content);
                var jsonResponse = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<HttpSingleResponse<Brand>>(jsonResponse);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null; 
            }
            
        }

    }
}
