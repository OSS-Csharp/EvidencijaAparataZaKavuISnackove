using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Persistence.Repository;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models.DetaildModels;
using VendingMachine.Domain.Models.DetailedModels;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VendingMachine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VMController : ControllerBase
    {
        public IBaseRepository _baseRepository { get; set; }
        public VMController(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }


        [HttpGet("items")]
        public ActionResult<IEnumerable<Item>> GetAllItems()
        {
            var items = _baseRepository.GetAllItems().Select(x => Item.MapFrom(x)).ToList();
            return Ok(items);
        }
        [HttpGet("items/{id}")]
        public ActionResult<DetailedItem> GetItemById(int id)
        {
            try
            {
                var entity = _baseRepository.GetItemById(id);
                var detailedItem = DetailedItem.MapFromDetailed(entity);
                return Ok(detailedItem);
            }
            catch (NullReferenceException) { return NotFound(); }
            catch (InvalidOperationException) { return Unauthorized(); }
        }

        private ActionResult<DetailedItem> GetItemByName(string name)
        {
            try
            {
                var entity = _baseRepository.GetItemByName(name);
                var detailedItem = DetailedItem.MapFromDetailed(entity);
                return Ok(detailedItem);

            }
            catch (NullReferenceException) { return NotFound(); }
            catch (InvalidOperationException) { return Unauthorized(); }
        }
        [HttpPost("items")]
        public ActionResult<ItemEntity> CreateNewItem(Item newItem)
        {
            if (newItem == null)
                return BadRequest();
            var item = _baseRepository.CreateNewItem(newItem);
            if (item == null)
                return BadRequest($"Item with name {newItem._itemName} already exists. ");
            return CreatedAtAction(nameof(GetItemByName), new { name = item.itemName }, item);
        }

        [HttpPut("items/{id}")]
        public ActionResult<ItemEntity> UpdateItem(Item item)
        {
            var entity = _baseRepository.UpdateItem(item);
            if (entity != null)
                return Ok(entity);
            return NotFound();
        }
        [HttpDelete("items/{id}")]
        public ActionResult<Item> DeleteItem(int id)
        {
            var itemsRemaining = _baseRepository.DeleteItem(id);
            if (itemsRemaining == null)
                return BadRequest("Item can not be deleted. ");
            var items = GetAllItems();
            return Ok(items);
        }


        [HttpGet("machines")]
        public ActionResult<IEnumerable<VendingMachineObject>> GetAllMachines()
        {
            var machines = _baseRepository.GetAllMachines().Select(x => VendingMachineObject.MapFrom(x)).ToList();
            return Ok(machines);
        }
        [HttpGet("machines/{id}")]
        public ActionResult<DetailedMachine> GetMachineById(int id)
        {
            var entity = _baseRepository.GetMachineById(id);
            if (entity != null)
            {
                var detailedMachine = DetailedMachine.MapFromDetailed(entity);
                return Ok(detailedMachine);
            }
            return NotFound();
        }
        [HttpPost("machines")]
        public ActionResult<VendingMachineEntity> CreateNewMachine(VendingMachineObject entity)
        {
            if (entity == null)
                return BadRequest();
            var machine = _baseRepository.CreateNewMachine(entity);
            if (machine == null)
                return NotFound();
            return CreatedAtAction(nameof(GetMachineById), new { id = machine.id }, machine);
        }


        [HttpPost("machines/{machineId}/{itemId}")]
        public ActionResult<VendingMachineEntity> AddItemsToMachine(int machineId, int itemId, List<int> ids)
        {
            var machine = _baseRepository.AddItemsToMachine(machineId, itemId);
            if (machine == null)
                return BadRequest("The item already exists in the machine. ");
            return Ok(machine);
        }

        [HttpPut("machines/{machineId}/{itemId}")]
        public ActionResult<DetailedMachine> SetItemAmount(DetailedMachine detailedMachine)
        {
            var machine = _baseRepository.SetItemAmount(detailedMachine);
            if (machine == null)
                return NotFound();
            try
            {
                var detailed = DetailedMachine.MapFromDetailed(machine);
                return Ok(detailed);
            }
            catch (NullReferenceException) { return Ok(machine); }

        }


        [HttpPut("machines/{id}")]
        public ActionResult<DetailedMachine> SetMachineValidity(DetailedMachine detailedMachine)
        {
            var newMachine = new VendingMachineEntity
            {
                id = detailedMachine._id,
                machineValidity = detailedMachine._machineValidity
            };
            var result = _baseRepository.SetMachineValidity(newMachine);
            if (result == null)
                return NotFound();
            try
            {
                var detailed = DetailedMachine.MapFromDetailed(result);
                return Ok(detailed);
            }
            catch (NullReferenceException) { return Ok(result); }
        }


        [HttpDelete("machines/{id}")]
        public ActionResult<VendingMachineObject> DeleteMachine(int id)
        {
            var machine = _baseRepository.DeleteMachine(id);
            if (machine != null)
                return Ok(machine);
            return BadRequest("Machine can not be deleted. ");
        }


        [HttpGet("many")]
        public ActionResult<IEnumerable<VendingMachineItem>> GetAllManyToMany()
        {
            var many = _baseRepository.GetAllManyToMany().Select(x => VendingMachineItem.MapFrom(x)).ToList();
            return Ok(many);
        }
        [HttpGet("many/{id}")]
        public ActionResult<VendingMachineItemEntity> GetManyById(int id)
        {
            var entity = _baseRepository.GetManyById(id);
            if (entity != null)
                return Ok(entity);
            return NotFound();
        }

         [HttpPost]
        [Route("~/api/Handshake")]
        public async Task<string> NewMachineHandshake()
        {
            _baseRepository.CreateNewMachineVoid();
            var newMachine=_baseRepository.GetNewMachine();
            _baseRepository.InsertCoffee(newMachine);

            return await Task.Run(() => { string handshake = "Server has recognised machine,creating new instance"; return handshake; });
            
        }
        [HttpPost]
        [Route("~/api/HeaderDetailer")]
        public async Task<string> HeaderDetailer()
        {
            string headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + "=" + Request.Headers[key] + Environment.NewLine;
            await Console.Out.WriteLineAsync("Machine header details: " + Request.ToString());
            return await Task.Run(() => { string data = "Header state= " + headers; return data; });
        }
        [HttpPost]
        [Route("~/api/Drain")]
        public async void DrainMachine()  
        {
            if(_baseRepository.BuyACoffee()==true)
            {
            await Console.Out.WriteAsync("Bought one coffee ");
            }
            else
            {
            await Console.Out.WriteAsync("Bought the last coffee ");
            Actions.StopMachine();
            _baseRepository.CreateTestJob();
            }
        }

        [HttpPost]
        [Route("~/api/KickStartMachine")]
        public async void ManualMachineRefil()
        {

            await Console.Out.WriteAsync("\nServicemen restocked the machine \n");
            _baseRepository.Refuel();
            Actions.ResumeMachine();
            _baseRepository.WorkComplete(1);
            
        }
        [HttpPost]
        [Route("~/api/ManualJobCompletion")]
        public async void ManualJobCompletion()
        {
            await Console.Out.WriteAsync("\n Admin restocked the machine \n");
            _baseRepository.WorkComplete(1);
        } 

        [HttpPost]
        [Route("~/api/ByeBye")]
        public async void MachineShuttingDown()
        {
            await Console.Out.WriteAsync("\nMachine is shutting down\n");

            _baseRepository.RemoveMachine();
            
        }

        [HttpPost]
        [Route("~/api/NewJob")]
        public ActionResult<JobMachineWorkerEntity> AddNewJob(int machineId, int workerId)
        {
            var job = _baseRepository.CreateNewJob(machineId, workerId);
            if (job == null)
                return NotFound();
            return Ok(job);
        }

        [HttpPost]
        [Route("~/api/UserList")]
        public async void ParseUserDatabase()
        {

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requiredData = await reader.ReadToEndAsync();
                dynamic d = JObject.Parse(requiredData.ToString());
                foreach (var user in d)
                {
                    Console.WriteLine(user.userName);
                    _baseRepository.UpdateLocalUserList(user.userName,user.id);
                }
            }
        }
    }
}

