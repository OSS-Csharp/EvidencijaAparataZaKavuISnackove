using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;

namespace VendingMachine.UserInterface.Services
{
    public class ItemService : IItemService
    {
        //44396
        private readonly HttpClient _httpClient;
        public ItemService(HttpClient httpClient)
        {
            //httpClient.BaseAddress = new Uri("https://localhost:44396/TemporaryVM/items");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _httpClient.GetFromJsonAsync<Item[]>("https://localhost:44396/TemporaryVM/items");
        }

        public async Task<DetailedItem> GetItemById(int id)
        {
            return await _httpClient.GetFromJsonAsync<DetailedItem>($"https://localhost:44396/TemporaryVM/items/{id}");
        }
        public async Task<Item> CreateNewItem(Item entity)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:44396/TemporaryVM/items/", entity);
            return entity;
        }
        
        public async Task<Item> UpdadeItem(Item updatedItem)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:44396/TemporaryVM/items/{updatedItem}", updatedItem);
            return updatedItem;
        }

        public async Task DeleteItem(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:44396/TemporaryVM/items/{id}");
        }
    }
}
