using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.UserInterface.Services;

namespace VendingMachine.UserInterface.Pages.VendingMachinePages
{
    public class ItemDetailsBase : ComponentBase
    {
        public DetailedItem _detaildItem { get; set; } = new DetailedItem();
        public Item item { get; set; } = new Item();
        public Item updatedItem { get; set; }
        [Parameter]
        public string _id { get; set; }
        [Inject]
        public IItemService _itemService { get; set; }
        [Parameter]
        public string _itemName { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            //_detaildItem = await _itemService.GetItemByName(_itemName.ToString());
            //item = await _itemService.GetItemById(int.Parse(_id));
            _detaildItem = await _itemService.GetItemById(int.Parse(_id));

        }
        protected async Task OnUpdateItem()
        {
            var result = await _itemService.UpdadeItem(updatedItem);
            if (result != null)
                _navigationManager.NavigateTo("/");

        }
        protected async Task OnDeleteItem()
        {
            await _itemService.DeleteItem(int.Parse(_id));
            _navigationManager.NavigateTo("/items");
        }
    }
}
