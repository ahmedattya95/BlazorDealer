using AKSoftware.WebApi.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorDealer.Client
{
    public class CarsService
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

        public CarsService(string url)
        {
            _client = new ServiceClient();
            _baseUrl = url;
        }

        // Get all cars 
        public async Task<HttpCollectionResponse<Car>> GetAllCarsAsync(string query)
        {
            return await _client.GetProtectedAsync<HttpCollectionResponse<Car>>($"{_baseUrl}/cars?getall=true");
        }

        // Get one car 
        public async Task<HttpSingleResponse<Car>> GetByIdAsync(string id)
        {
            return await _client.GetProtectedAsync<HttpSingleResponse<Car>>($"{_baseUrl}/cars/{id}");
        }

        // Upload Car 
        public async Task<HttpSingleResponse<Car>> AddCarAsync(CarViewModel model, AppFile carImage)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(carImage.FileStream, (int)carImage.FileStream.Length), "CarImage", carImage.FileName);
            content.Add(new StringContent(model.CarModel), "CarModel");
            content.Add(new StringContent(model.Description), "Description");
            content.Add(new StringContent(model.Price.ToString()), "Price");
            content.Add(new StringContent(model.Year.ToString()), "Year");
            content.Add(new StringContent(model.TypeId), "TypeId");
            content.Add(new StringContent(model.BrandId), "BrandId");

            var result = await client.PostAsync($"{_baseUrl}/cars", content);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HttpSingleResponse<Car>>(jsonResponse);
        }

        // Upload car images 
        public async Task<HttpCollectionResponse<string>> UploadCarImagesAsync(string id, IEnumerable<AppFile> files)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

            var content = new MultipartFormDataContent();

            foreach (var item in files)
            {
                content.Add(new StreamContent(item.FileStream, (int)item.FileStream.Length), "CarImages", item.FileName);
            }
        
            var result = await client.PostAsync($"{_baseUrl}/cars/{id}", content);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HttpCollectionResponse<string>>(jsonResponse);
        }
    }
}
