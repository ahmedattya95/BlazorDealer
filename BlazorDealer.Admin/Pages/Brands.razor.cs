using Blazor.FileReader;
using BlazorDealer.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class BrandsBase : ComponentBase
    {

        public bool IsBusy { get; set; } = false;

        [Inject] BrandsService BrandsService { get; set; }
        [Inject] IFileReaderService FileReaderService { get; set; }
        [Inject]NavigationManager NavigationManager { get; set; }

        protected List<Brand> brands = new List<Brand>();

        protected BrandViewModel Model = new BrandViewModel();
        protected Stream FileStream { get; set; }
        Blazor.FileReader.IFileInfo _fileInfo;
        #region Upload Icon 
        protected ElementReference inputReference;
        protected string Message { get; set; }
        protected string AlertClass { get; set; }
        protected async Task ReadFileAsync()
        {
            var file = (await FileReaderService.CreateReference(inputReference).EnumerateFilesAsync()).FirstOrDefault();
            _fileInfo = await file.ReadFileInfoAsync();
            using (var fs = await file.OpenReadAsync())
            {
                FileStream = fs;
            }
        }

        #endregion

        public async Task InsertBrandAsync()
        {
            IsBusy = true;

            var file = (await FileReaderService.CreateReference(inputReference).EnumerateFilesAsync()).FirstOrDefault();
            _fileInfo = await file.ReadFileInfoAsync();
            using (var ms = await file.CreateMemoryStreamAsync((int)_fileInfo.Size))
            {
                FileStream = ms;

                var result = await BrandsService.AddNewBrandAsync(Model, new AppFile
                {
                    FileStream = FileStream,
                    FileName = _fileInfo.Name,
                    Name = $"{Model.Name}{Path.GetExtension(_fileInfo.Name)}"
                });
                if (result.IsSuccess)
                {
                    AlertClass = "alert-success";
                    Message = "Brand has been added successfully!";
                    brands.Add(result.Value);
                }
                else
                {
                    AlertClass = "alert-danger";
                    Message = "There is something wrong";
                    Debug.WriteLine(result.Message);
                }
            }
          
            IsBusy = false;
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            IsBusy = true;
            await Task.Delay(1500);
            var response = await BrandsService.GetAllBrandsAsync();
            IsBusy = false;
            if (response.IsSuccess)
            {
                return response.Values.ToList();
            }

            return null;
        }

        protected override async Task OnInitializedAsync()
        {
            if (GeneralSettings.AccessToken == null)
                NavigationManager.NavigateTo("login");

            BrandsService.AccessToken = GeneralSettings.AccessToken; 
            brands = await GetBrandsAsync();
        }
    }
}
