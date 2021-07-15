using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Http.HttpRequest;
using Microsoft.AspNetCore.Http;

namespace SingularCoffeMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        // public event EventHandler SomethingHappened;
        // public void PausedEvent(){
        //     if( SomethingHappened != null )
        //         SomethingHappened;
        // }

        public event Action MyEvent;
        private readonly ILogger<MachineController> _logger;

        public MachineController(SequentialUpdateSimulationSingleton simp,ILogger<MachineController> logger){
            _simp=simp;
            _logger = logger;

        }
        private  SequentialUpdateSimulationSingleton _simp {get;set;}

        [Route("~/api/machineEmpty")]
        [HttpPost]
        public async void PauseTimer()
        {
            try{
            _simp.StopTimer();
            // target.simp.StopTimer();
            await Console.Out.WriteLineAsync("Machine stopped");
            }catch(Exception er){await Console.Out.WriteLineAsync(er.ToString());}
            //kada je stroj prazan triba se sequential drain zaustavit da ne spusti stanje aparata ispod nule

        }

        [Route("~/api/machineRefilled")]
        [HttpPost]
        public async void ResumeTimer()
        {
            try{
            _simp.RestartTimer();
            // target.simp.StopTimer();
            await Console.Out.WriteLineAsync("Machine should resume");
            }catch(Exception er){await Console.Out.WriteLineAsync(er.ToString());}
            
            //kada je stroj prazan triba se sequential drain zaustavit da ne spusti stanje aparata ispod nule

            // SequentialUpdateSimulation.StopTimer(SingularCoffeMachine.Startup);            
        }

        [HttpPost]
        [Route("~/api/Handshake")]
        public async Task<string> ReadStringDataManual()
        {

            // using (StreamReader reader = new StreamReader(Request.Headers.ToString(), Encoding.UTF8))
            // {
            //     return await reader.ReadToEndAsync();
            // }
            string headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + "=" + Request.Headers[key] + Environment.NewLine;
            // return await headers.ToString();
            // string reqw= Request.Headers.Host();
            return await Task.Run(() => { string duck = "I have made machine with item,stanje= " + headers; return duck; });

            //for the love of god
            //samo isprintaj cili httprequest da vidin sta je u njemu ima li spasa za port sistem

        }

    }
}
