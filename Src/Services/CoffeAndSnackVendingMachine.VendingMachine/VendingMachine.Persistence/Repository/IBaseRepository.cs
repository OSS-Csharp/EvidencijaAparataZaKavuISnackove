using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.Domain.Models.DetailedModels;

namespace VendingMachine.Persistence.Repository
{
    public interface IBaseRepository
    {
        // Items
        public IEnumerable<ItemEntity> GetAllItems();
        public ItemEntity GetItemById(int id);
        public ItemEntity GetItemByName(string name);
        public ItemEntity CreateNewItem(Item newItem);
        public ItemEntity UpdateItem(Item updatedItem);
        public IEnumerable<ItemEntity> DeleteItem(int id);

        //Machines
        public IEnumerable<VendingMachineEntity> GetAllMachines();
        public VendingMachineEntity GetMachineById(int id);
        public VendingMachineEntity AddItemsToMachine(int machineId, int itemId);
        public VendingMachineEntity SetItemAmount(DetailedMachine entity);

        public VendingMachineEntity CreateNewMachine(VendingMachineObject entity);
        public VendingMachineEntity SetMachineValidity(VendingMachineEntity entity);
        public IEnumerable<VendingMachineEntity> DeleteMachine(int id);
        // Many To Many 
        public IEnumerable<VendingMachineItemEntity> GetAllManyToMany();
        public VendingMachineItemEntity GetManyById(int id);

        public JobMachineWorkerEntity CreateNewJob(int machineEntityId,int workerEntityId);
        public void CreateNewMachineVoid();
        public VendingMachineEntity GetNewMachine();
        public void InsertCoffee(VendingMachineEntity vendingMachineEntity);
        public bool BuyACoffee();
        public void Refuel();
        public void RemoveMachine();
        public void CreateTestJob();
        public void WorkComplete(int servicemen);
        public void UpdateLocalUserList(string workerName,int foreignId);

    }
}
