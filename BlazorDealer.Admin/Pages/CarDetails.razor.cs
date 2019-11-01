using Blazor.FileReader;
using BlazorDealer.Client;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class CarDetailsBase : ComponentBase
    {

        [Inject]public CarsService CarsService { get; set; }
        [Inject]public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public Car Car { get; set; }

        public bool IsBusy { get; set; } = false; 

        #region Get Car Details 
        private async Task GetCarDetails()
        {
            if (Id != null)
            {
                IsBusy = true; 
                var result = await CarsService.GetByIdAsync(Id);
                Car = result.Value;

                IsBusy = false;
            }
            else
                NavigationManager.NavigateTo("/cars"); 
        }
        #endregion
  
        // On Page Load 
        protected async override Task OnInitializedAsync()
        {
            if (GeneralSettings.AccessToken == null)
                NavigationManager.NavigateTo("/login");

            CarsService.AccessToken = GeneralSettings.AccessToken;

            await GetCarDetails();
            await base.OnInitializedAsync();
        }

    }
}
