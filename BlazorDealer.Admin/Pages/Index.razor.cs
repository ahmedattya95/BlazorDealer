using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDealer.Admin.Pages
{
    public class IndexBase : ComponentBase
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<string> Bookings = new List<string>();

        HubConnection connection; 

        protected async override Task OnInitializedAsync()
        {
            if (GeneralSettings.AccessToken == null)
                NavigationManager.NavigateTo("login");

            await Task.Delay(0);

            connection = new HubConnectionBuilder().WithUrl("https://localhost:44374/bookinghub").Build();

            connection.On<string>("booking", b =>
            {
                Bookings.Add(b);
                StateHasChanged();
            });

            await connection.StartAsync(); 
        }
    }
}
