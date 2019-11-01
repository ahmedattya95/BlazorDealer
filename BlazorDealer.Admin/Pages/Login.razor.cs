
using BlazorDealer.Client;
using BlazorDealer.Models.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class LoginBase : ComponentBase
    {

        [Inject]public AuthService AuthService { get; set; }
        [Inject]public NavigationManager NavigationManager { get; set; }
        [Inject]public ILocalStorageService LocalStorage { get; set; }

        protected LoginViewModel Model = new LoginViewModel(); 
        protected string Message { get; set; }
        protected bool IsBusy = false; 
        protected async Task LoginAsync()
        {
            IsBusy = true; 
            var result = await AuthService.LoginAsync(Model);
            IsBusy = false; 
            if(result.IsSuccess)
            {
                GeneralSettings.AccessToken = result.Message;
                await LocalStorage.SetItemAsync("AccessToken", result.Message);
                NavigationManager.NavigateTo("/"); 
            }
            else
            {
                Message = result.Message; 
            }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var isLoggedIn = await LocalStorage.ContainKeyAsync("AccessToken");
                if (isLoggedIn)
                {
                    GeneralSettings.AccessToken = await LocalStorage.GetItemAsync<string>("AccessToken");
                }
            }
            catch (Exception)
            {

            }

                

        }
    }
}
