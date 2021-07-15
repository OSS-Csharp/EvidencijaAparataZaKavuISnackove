using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.Domain.Models.DetailedModels;

namespace VendingMachine.Persistence.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly VmDbContext _dbContext;

        public BaseRepository(VmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //  Items   //
        public IEnumerable<ItemEntity> GetAllItems()
        {
            return _dbContext.ItemEntities.Include(o => o.vendingMachineItemEntities).ToList();

        }
        public ItemEntity GetItemById(int id)
        {
            var item = _dbContext.ItemEntities.Include(o => o.vendingMachineItemEntities).ThenInclude(m => m.vendingMachine).Single(i => i.id == id);
            if (item != null)
                return item;
            return null;
        }
        public ItemEntity GetItemByName(string name)     
        {
            var item = _dbContext.ItemEntities.Include(o => o.vendingMachineItemEntities).ThenInclude(m => m.vendingMachine).Single(i => i.itemName == name);
            if (item != null)
                return item;
            return null;
        }
        public ItemEntity CreateNewItem(Item newItem)
        {
            var check = _dbContext.ItemEntities.ToList();
            foreach(var c in check)
            {
                if (c.itemName == newItem._itemName)
                    return null;
            }
            var entity = new ItemEntity
            {
                itemName = newItem._itemName,
                price = newItem._price
            };
            _dbContext.ItemEntities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public ItemEntity UpdateItem(Item updatedItem)
        {
            var entity = _dbContext.ItemEntities.Find(updatedItem._id);
            if(entity != null)
            {
                entity.itemName = updatedItem._itemName;
                entity.price = updatedItem._price;
                _dbContext.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            return null;
        }
        private bool CheckItem(int itemId)
        {
            var many = _dbContext.VendingMachineItemEntities.ToList();
            foreach(var m in many)
            {
                if (m.itemId == itemId)
                    return false;
            }
            return true;
        }
        public IEnumerable<ItemEntity> DeleteItem(int id)
        {
            var item = _dbContext.ItemEntities.Find(id);
            if (item != null)
            {
                try
                {
                    if(CheckItem(item.id))
                    {
                        _dbContext.ItemEntities.Remove(item);
                        _dbContext.SaveChanges();
                        return GetAllItems();
                    }
                }
                catch { return null; }
            }
            return null;
        }

        //  Machines    //
        public IEnumerable<VendingMachineEntity> GetAllMachines()
        {
            return _dbContext.VendingMachineEntities.Include(o => o.vendingMachineItemEntities).ToList();
        }
        public VendingMachineEntity GetMachineById(int id)
        {
            var machine = _dbContext.VendingMachineEntities.Include(o => o.vendingMachineItemEntities).ThenInclude(i => i.item).FirstOrDefault(o => o.id == id);
            if (machine != null)
                return machine;
            return null;
        }

        public VendingMachineEntity AddItemsToMachine(int machineId, int itemId)
        {
            var machine = _dbContext.VendingMachineEntities.Find(machineId);
            var item = _dbContext.ItemEntities.Find(itemId);
            var many = _dbContext.VendingMachineItemEntities.ToList();
            if (machine == null)
                return null;
            if (item == null)
                return null;
            foreach (var m in many)
            {
                if (m.itemId == item.id && m.vendingMachineId == machine.id)
                    return null;
            }
            if (machine.vendingMachineItemEntities == null)
            {
                machine.vendingMachineItemEntities = new List<VendingMachineItemEntity>()
                {
                    new VendingMachineItemEntity
                    {
                        vendingMachineId = machine.id,
                        vendingMachine = machine,
                        itemId = item.id,
                        item = item,
                        amountOfItem = 10
                    }
                };
            }
            else
            {
                machine.vendingMachineItemEntities.Add
                    (
                        new VendingMachineItemEntity
                        {
                            vendingMachineId = machine.id,
                            vendingMachine = machine,
                            itemId = item.id,
                            item = item,
                            amountOfItem = 10
                        });
            }
            _dbContext.VendingMachineEntities.Include(o => o.vendingMachineItemEntities);
            _dbContext.Update(machine);
            _dbContext.SaveChanges();
            return machine;
        }

        public VendingMachineEntity SetItemAmount(DetailedMachine entity)
        {
            var machine = _dbContext.VendingMachineEntities.Include(im => im.vendingMachineItemEntities).FirstOrDefault(m => m.id == entity._id);
            foreach(var im in machine.vendingMachineItemEntities)
            {
                foreach(var e in entity._vendingMachineItemEntities)
                {
                    if(im.itemId == e._itemId)
                    {
                        var toUpdate = machine.vendingMachineItemEntities.Single(x => x.itemId == im.itemId);
                        if (e._amountOfAnItem < 0)
                        {
                            toUpdate.amountOfItem = 0;
                            _dbContext.Update(machine);
                            _dbContext.SaveChanges();
                        }
                        else if(e._amountOfAnItem > 10)
                        {
                            toUpdate.amountOfItem = 10;
                            _dbContext.Update(machine);
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            toUpdate.amountOfItem = e._amountOfAnItem;
                            _dbContext.Update(machine);
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }
            return machine;
        }


        private VendingMachineEntity AddInitialItemsToNewlyCreatedMachine(VendingMachineEntity vendingMachineEntity)
        {
            var items = _dbContext.ItemEntities.ToList();
            var itemCount = items.Count();
            int count = 0;
            if(items != null)
            {
                vendingMachineEntity.vendingMachineItemEntities = new List<VendingMachineItemEntity>();
                foreach (var item in items)
                {
                    if (item != null && count < 3)
                    {
                        vendingMachineEntity.vendingMachineItemEntities.Add(new VendingMachineItemEntity
                        {
                            vendingMachineId = vendingMachineEntity.id,
                            vendingMachine = vendingMachineEntity,
                            item = item,
                            itemId = item.id,
                            amountOfItem = 10
                        });
                        count++;
                    }
                }
                _dbContext.VendingMachineEntities.Include(i => i.vendingMachineItemEntities);
                _dbContext.Update(vendingMachineEntity);
                return vendingMachineEntity;
            }
            return vendingMachineEntity;
        }

        public VendingMachineEntity CreateNewMachine(VendingMachineObject machineObject)
        {
            var newMachine = new VendingMachineEntity
            {
                machineValidity = machineObject._machineValidity
            };
            _dbContext.VendingMachineEntities.Add(newMachine);
            _dbContext.SaveChangesAsync();
            //newMachine = AddInitialItemsToNewlyCreatedMachine(newMachine);
            return _dbContext.VendingMachineEntities.Find(newMachine.id);
        }


        public VendingMachineEntity SetMachineValidity(VendingMachineEntity entity)
        {
            var machine = _dbContext.VendingMachineEntities.Find(entity.id);
            if (machine == null)
                return null;
            machine.machineValidity = entity.machineValidity;
            _dbContext.VendingMachineEntities.Update(machine);
            _dbContext.SaveChanges();
            return machine;
        }




        public IEnumerable<VendingMachineEntity> DeleteMachine(int id)
        {
            var machine = _dbContext.VendingMachineEntities.Find(id);
            if(machine != null)
            {
                var many = _dbContext.VendingMachineItemEntities.ToList();
                foreach (var m in many)
                {
                    if (m.vendingMachineId == machine.id)
                        return null;
                }
                _dbContext.VendingMachineEntities.Remove(machine);
                _dbContext.SaveChanges();
                return GetAllMachines();
            }
            return null;
        }
        //  Many to many    //
        public IEnumerable<VendingMachineItemEntity> GetAllManyToMany()
        {
            return _dbContext.VendingMachineItemEntities.ToList();
        }
        public VendingMachineItemEntity GetManyById(int id)
        {
            return _dbContext.VendingMachineItemEntities.Find(id); 
        }


        public JobMachineWorkerEntity CreateNewJob(int machineEntityId,int workerEntityId)
        {
            var machine =_dbContext.VendingMachineEntities.Find(machineEntityId);
            var workerToAdd =_dbContext.WorkerEntities.Find(workerEntityId);

            if (machine == null)
                return null;
            if (workerToAdd == null)
                return null;

            var newJobEntry= new JobMachineWorkerEntity{
                    vendingMachineId = machine.id,
                    vendingMachine = machine,
                    workerId = workerToAdd.id,
                    worker= workerToAdd,
                    finished = false
            };

            _dbContext.JobMachineWorkerEntities.Add(newJobEntry);
            _dbContext.SaveChangesAsync();

            _dbContext.SaveChanges();
            return newJobEntry;
        }

        public void CreateNewMachineVoid()
        {
            var newMachine = new VendingMachineEntity
            {
                machineValidity = true
            };
            try{
            _dbContext.VendingMachineEntities.Add(newMachine);
            _dbContext.SaveChangesAsync();
            }catch(Exception err){Console.WriteLine("Unsuccessful machine creation" + err);}
        }

        public VendingMachineEntity GetNewMachine(){
            return _dbContext.VendingMachineEntities.OrderByDescending(x => x.id).FirstOrDefault();
        }

        public void InsertCoffee(VendingMachineEntity vendingMachineEntity){
            try{
            var newMachineFiller= new VendingMachineItemEntity{
                    vendingMachineId = vendingMachineEntity.id,
                    vendingMachine = vendingMachineEntity,
                    itemId=1,
                    item=_dbContext.ItemEntities.Find(1),
                    amountOfItem=7
            };

            _dbContext.VendingMachineItemEntities.Add(newMachineFiller);

            _dbContext.SaveChanges();
            }catch(Exception err){Console.WriteLine("Unsuccessful machine restock" + err);}

        }

        public bool BuyACoffee()
        {
            var toBeUsed=_dbContext.VendingMachineItemEntities.OrderByDescending(x => x.id).FirstOrDefault();
            if(toBeUsed.amountOfItem==1)
            return false;
            int amount= toBeUsed.amountOfItem;
            toBeUsed.amountOfItem = amount -1 ;
            _dbContext.SaveChanges();

            Console.WriteLine("Coffe Status: "+toBeUsed.amountOfItem.ToString());
            return true;

        }
        public void Refuel()
        {
            var toBeUsed=_dbContext.VendingMachineItemEntities.OrderByDescending(x => x.id).FirstOrDefault();;
            toBeUsed.amountOfItem = 10 ;
            _dbContext.SaveChanges();

            Console.WriteLine("Machine refilled : "+toBeUsed.amountOfItem.ToString());
            
        }

        public void RemoveMachine()
        {
            var toBeUsed=_dbContext.VendingMachineItemEntities.OrderByDescending(x => x.id).FirstOrDefault();;
            _dbContext.VendingMachineItemEntities.Remove(toBeUsed);

            _dbContext.SaveChanges();

            Console.WriteLine("Machine will be removed .");
            
        }
        public void CreateTestJob()
        {
            var machine =_dbContext.VendingMachineEntities.OrderByDescending(x => x.id).FirstOrDefault();;
            if (_dbContext.JobMachineWorkerEntities.Any(o => o.vendingMachineId == machine.id && o.finished==false)) return;
            var workerToAdd =_dbContext.WorkerEntities.Find(1);

            var newJobEntry= new JobMachineWorkerEntity{
                    vendingMachineId = machine.id,
                    vendingMachine = machine,
                    workerId = workerToAdd.id,
                    intworkerIdOriginal=1,
                    worker= workerToAdd,
                    finished = false
            };

            _dbContext.JobMachineWorkerEntities.Add(newJobEntry);

            _dbContext.SaveChanges();
        }

        public void WorkComplete(int servicemen)
        {
            try{
            var worker=_dbContext.WorkerEntities.Find(servicemen);
            var job=_dbContext.JobMachineWorkerEntities.Single(i => i.workerId == servicemen && i.finished!= true);
            job.finished=true;
            _dbContext.SaveChanges();
            }catch(Exception err){Console.WriteLine("Error manually finishing job :" + err);}
        }

        public void UpdateLocalUserList(string workerName,int foreignId)
        {
            if (_dbContext.WorkerEntities.Any(o => o.workerIdOriginal == foreignId && o.name==workerName)) return;

            try{
            var newUser= new WorkerEntity{
                    workerIdOriginal = foreignId,
                    name=workerName
            };

            _dbContext.WorkerEntities.Add(newUser);
            _dbContext.SaveChanges();
            }catch(Exception err){Console.WriteLine("User creation failiure" + err);}            
        }
    }
}
