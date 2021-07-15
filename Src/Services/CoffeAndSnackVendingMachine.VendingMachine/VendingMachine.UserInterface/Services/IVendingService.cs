using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetailedModels;

namespace VendingMachine.UserInterface.Services
{
    public interface IVendingService
    {
        Task<IEnumerable<VendingMachineObject>> GetAllMachines();
        Task<DetailedMachine> GetMachineById(int id);
        Task SetMachineValidity(DetailedMachine detailedMachine);
        Task SetItemAmount(DetailedMachine detailedMachine, int itemId);
        Task<VendingMachineObject> CreateNewMachine(VendingMachineObject entity);
        Task AddItemsToMachine(int machineId, int itemId, List<int> lst);
        Task DeleteMachine(int id);

    }
}
