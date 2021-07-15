using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Models;
using VendingMachine.Persistence.Repository;
using VendingMachine.Domain.Entity;
using System.IO;
using System.Text;
using System.Data;
using System.Net;


namespace VendingMachine.API.Controllers
{
    public class Actions
    {
        public IBaseRepository _baseRepository { get; set; }
        public Actions(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public static async void sendStopSignal(int emptyMachineId)
        {
            //construct url from empty
            await Console.Out.WriteLineAsync("Sending stop signal to machine : " + emptyMachineId.ToString());

            var url = "http://localhost:" + emptyMachineId.ToString() + "/api/machineEmpty";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var message = "No avaliable items";
            var json = System.Text.Json.JsonSerializer.Serialize(message);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

        }
        public static async void sendRestartSignal(int emptyMachineId)
        {
            //construct url from empty
            await Console.Out.WriteLineAsync("Sending restart signal to machine : " + emptyMachineId.ToString());

            var url = "http://localhost:" + emptyMachineId.ToString() + "/api/machineRefilled";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var message = "Machine refilled";
            var json = System.Text.Json.JsonSerializer.Serialize(message);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

        }

        public static async void StopMachine()
        {

            await Console.Out.WriteLineAsync("Stopping the machine");
            var url = "http://localhost:5005/api/machineEmpty";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var user = "Machine empty ,stop update";
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
        }
        //
        public static async void ResumeMachine()
        {

            await Console.Out.WriteLineAsync("Sending restart signal to machine...");
            var url = "http://localhost:5005/api/machineRefilled";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var user = "Restart the machine";
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
        }

        public static async void SendJobToWorker(int JobId)
        {

            await Console.Out.WriteLineAsync("Sending job to worker...");
            var url = "http://localhost:5010/api/newJob";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var user = "WorkerId : 1 \n JobId : "+JobId.ToString();
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
        }
    }
}