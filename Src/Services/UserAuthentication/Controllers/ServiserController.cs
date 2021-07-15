using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthentication.Data;
using UserAuthentication.Models;
using UserAuthentication.Repository;

namespace UserAuthentication.Controllers
{
    [Route("[controller")]
    public class ServiserController : ControllerBase
    {
        public IBaseRepository _baseRepository { get; set; }
        public ServiserController(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        [HttpPost]
        [Route("~/assign")]
        public async Task AssignJob()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requiredData = await reader.ReadToEndAsync();
                dynamic d = JObject.Parse(requiredData.ToString());
                string userName = d.UserName;
                int id = d.Id;
                _baseRepository.AssignJob(userName, id);
            }
        }
        [HttpGet]
        [Route("~/schedule/{userName}")]
        public async Task<Schedule> GetUserSchedule(string userName)
        {
            return await Task.Run(() => _baseRepository.GetUserSchedule(userName));
        }

        [HttpGet]
        [Route("~/users")]
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return (List<ApplicationUser>)await Task.Run(() => _baseRepository.GetAllUsers());
        }

        [HttpPost]
        [Route("~/users/{userName}")]
        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            return await Task.Run(() => _baseRepository.GetUserByUserName(userName));
        }
        [HttpGet]
        [Route("~/availables")]
        public async Task<List<ApplicationUser>> Availables()
        {
            return (List<ApplicationUser>)await Task.Run(() => _baseRepository.GetAvailables());
        }

    }
}
