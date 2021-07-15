using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetailedModels;
using VendingMachine.UserInterface.Services;

namespace VendingMachine.UserInterface.Pages.VendingMachinePages
{
    public class VendingPage : ComponentBase
    {

        [Inject]
        public IVendingService _vendingService { get; set; }
        public IEnumerable<VendingMachineObject> machines { get; set; }
        public VendingMachineObject machine { get; set; } = new VendingMachineObject();
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            machines = (await _vendingService.GetAllMachines()).ToList();
        }
        protected async Task OnCreateNewMachine()
        {
            var result = await _vendingService.CreateNewMachine(machine);
            if (result != null)
                _navigationManager.NavigateTo("/");
        }
    }
}
