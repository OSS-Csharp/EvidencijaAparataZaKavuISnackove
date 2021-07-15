using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.Domain.Models.DetailedModels;
using VendingMachine.UserInterface.Services;

namespace VendingMachine.UserInterface.Pages.VendingMachinePages
{
    public class VendingDetailsBase : ComponentBase
    {
        public DetailedMachine _detailedMachine { get; set; } = new DetailedMachine();
        public DetailedMachineItem _detailedMachineItem { get; set; } = new DetailedMachineItem();
        [Parameter]
        public string _machineId { get; set; }
        [Parameter]
        public string _itemId { get; set; }
        //public string _manyItemId { get; set; }

        [Inject]
        public IVendingService _vendingService { get; set; }
        
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        /* ITEM */
        [Inject]
        public IItemService _itemService { get; set; }
        public List<Item> _items { get; set; } = new List<Item>();
        /* END ITEM */

        protected override async Task OnInitializedAsync()
        {
            _detailedMachine = await _vendingService.GetMachineById(int.Parse(_machineId));
            _items = (await _itemService.GetAllItems()).ToList();
            _detailedMachineItem._vendingMachineId = (int.Parse(_machineId));
        }
        protected async Task OnSetValidity()
        {
            await _vendingService.SetMachineValidity(_detailedMachine);
            _navigationManager.NavigateTo("/");
        }
        protected async Task OnSetItemAmount()
        {
            await _vendingService.SetItemAmount(_detailedMachine, int.Parse(_itemId));
            _navigationManager.NavigateTo("/");
        }
        protected async Task OnAddItemToMachine()
        {
            var lst = new List<int>();
            lst.Add(int.Parse(_machineId));
            lst.Add(int.Parse(_itemId));
            await _vendingService.AddItemsToMachine(int.Parse(_machineId), int.Parse(_itemId), lst);
            _navigationManager.NavigateTo($"/machineDetails/{_machineId}");
        }
        protected async Task OnDeleteMachine()
        {
            await _vendingService.DeleteMachine(int.Parse(_machineId));
            _navigationManager.NavigateTo("/");
        }

    }
}
