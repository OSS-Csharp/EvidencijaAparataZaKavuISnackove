using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetailedModels;

namespace VendingMachine.UserInterface.Services
{
    public class VendingService : IVendingService
    {
        private readonly HttpClient _httpClient;
        public VendingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<VendingMachineObject>> GetAllMachines()
        {
            return await _httpClient.GetFromJsonAsync<VendingMachineObject[]>("https://localhost:44396/TemporaryVM/machines");
        }

        public async Task<DetailedMachine> GetMachineById(int id)
        {
            return await _httpClient.GetFromJsonAsync<DetailedMachine>($"https://localhost:44396/TemporaryVM/machines/{id}");
        }

        public async Task SetItemAmount(DetailedMachine detailedMachine, int itemId)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:44396/TemporaryVM/machines/{detailedMachine._id}/{itemId}", detailedMachine);
        }

        public async Task SetMachineValidity(DetailedMachine detailedMachine)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:44396/TemporaryVM/machines/{detailedMachine._id}", detailedMachine);
        }

        public async Task<VendingMachineObject> CreateNewMachine(VendingMachineObject entity)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:44396/TemporaryVM/machines/", entity);
            return entity;
        }

        public async Task AddItemsToMachine(int machineId, int itemId, List<int> ids)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:44396/TemporaryVM/machines/{machineId}/{itemId}", ids);
        }
        public async Task DeleteMachine(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:44396/TemporaryVM/machines/{id}");
        }
    }
}
