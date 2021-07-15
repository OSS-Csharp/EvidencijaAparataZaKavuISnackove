using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.UserInterface.Services;

namespace VendingMachine.UserInterface.Pages.VendingMachinePages
{
    public class ItemPage : ComponentBase
    {
        [Inject]
        public IItemService _itemService { get; set; }
        public IEnumerable<Item> items { get; set; }
        public Item item { get; set; } = new Item();
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = (await _itemService.GetAllItems()).ToList();
        }
        protected async Task OnCreateNewItem()
        {
            var result = await _itemService.CreateNewItem(item);
            if (result != null)
                _navigationManager.NavigateTo("/");
        }
    }
}
