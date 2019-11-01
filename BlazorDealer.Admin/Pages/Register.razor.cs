using BlazorDealer.Client;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class RegisterBase : ComponentBase
    {

        [Inject]
        public AuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManger { get; set; }

        public RegisterViewModel Model = new RegisterViewModel();
        protected bool IsBusy = false; 
        protected string Message { get; set;  }
        protected async Task RegisterAsync()
        {
            IsBusy = true;
            var result = await AuthService.RegisterAsync(Model);
            IsBusy = false;
            if (result.IsSuccess)
            {
                NavigationManger.NavigateTo("login"); 
            }
            else
            {
                Message = result.Message; 
            }

            
        }

    }
}
