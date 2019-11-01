using Blazor.FileReader;
using BlazorDealer.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class CarsBase : ComponentBase
    {

        [Inject]protected CarTypesService CarTypesService { get; set; }
        [Inject]protected CarsService CarsService { get; set; }
        [Inject]protected BrandsService BrandsService { get; set; }
        [Inject] IFileReaderService FileReaderService { get; set; }
        [Inject]NavigationManager NavigationManager { get; set; }

        protected List<CarType> CarTypes = new List<CarType>();
        protected bool IsBusy = false;
        protected List<Brand> Brands = new List<Brand>();
        protected List<Car> MyCars = new List<Car>(); 
        protected CarViewModel Model = new CarViewModel();
        protected ElementReference inputReference;
        IFileInfo _fileInfo;

        protected string Message { get; set; }
        protected string AlertClass { get; set; }
        #region Initialize 
        private async Task GetCarTypesAsync()
        {
            IsBusy = true; 
            var result = await CarTypesService.GetAllCarTypesAsync();
            CarTypes = result.Values.ToList();
            IsBusy = false; 
        }

        private async Task GetBrandsAsync()
        {
            IsBusy = true; 
            var result = await BrandsService.GetAllBrandsAsync();
            Brands = result.Values.ToList();
            IsBusy = false; 
        }

        private async Task GetCarsAsync()
        {
            IsBusy = true;
            var result = await CarsService.GetAllCarsAsync("");
            MyCars = result.Values.ToList();
            IsBusy = false;
        }

        #endregion 
        protected async Task InsertCarAsync()
        {
            IsBusy = true;
            var file = (await FileReaderService.CreateReference(inputReference).EnumerateFilesAsync()).FirstOrDefault();
            _fileInfo = await file.ReadFileInfoAsync(); 
            using (var ms = await file.CreateMemoryStreamAsync())
            {

                var result = await CarsService.AddCarAsync(Model, new AppFile
                {
                    FileStream = ms,
                    FileName = $"{_fileInfo.Name}",
                    Name = Model.CarModel
                }); 

                if(result.IsSuccess)
                {
                    Message = "Car has been inserted successfully!";
                    AlertClass = "alert-success";
                    MyCars.Add(result.Value); 
                }
                else
                {
                    Message = "There is something wrong try again later";
                    AlertClass = "alert-error";
                }
            }

            IsBusy = false; 
        }


        protected async override Task OnInitializedAsync()
        {
            if (GeneralSettings.AccessToken == null)
                NavigationManager.NavigateTo("login");

            BrandsService.AccessToken = GeneralSettings.AccessToken;
            CarsService.AccessToken = GeneralSettings.AccessToken;
            CarTypesService.AccessToken = GeneralSettings.AccessToken; 

            await GetCarTypesAsync();
            await GetBrandsAsync();
            await GetCarsAsync(); 

            await base.OnInitializedAsync();
        }

    }
}
