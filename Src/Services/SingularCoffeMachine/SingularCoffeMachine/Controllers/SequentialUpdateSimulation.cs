using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Timers;
using System.Net;
using System.Text.Json;
using System.Web;

namespace SingularCoffeMachine.Controllers
{
    public class SequentialUpdateSimulation
    {

        System.Timers.Timer _timer = new System.Timers.Timer
        {
            AutoReset = true,
            Enabled = true,
            Interval = TimeSpan.FromSeconds(5).TotalMilliseconds //7 seconds interval
        };
        static System.Timers.Timer _timer2 = new System.Timers.Timer
        {
            AutoReset = true,
            Enabled = true,
            Interval = TimeSpan.FromSeconds(7).TotalMilliseconds //7 seconds interval
        };
        public static void SequentialUpdate()
        {
            System.Timers.Timer t = new System.Timers.Timer(7000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Start();
        }
        public void SequentialUpdate2()
        {

            this._timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            this._timer.Start();
        }
        // public static void StopUpdate(this SequentialUpdateSimulation sequentialUpdate){
        //     // t.Stop();
        // }
        public void StopTimer()
        {

            this._timer.AutoReset = false;
        }
        public void RestartTimer()
        {
            this._timer.AutoReset = true;
        }

        public static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            await Console.Out.WriteLineAsync("Request sent , status: ");
            var url = "http://localhost:5000/api/Handshake2";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var user = "Data transfered succesfully";
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            await Console.Out.WriteLineAsync("Headers: "+(((HttpWebResponse)response).Headers).ToString() + "\n");
            await Console.Out.WriteLineAsync("Cookies: "+(((HttpWebResponse)response).Cookies).ToString()  + "\n");
            await Console.Out.WriteLineAsync("ResponseURI: "+(((HttpWebResponse)response).ResponseUri).ToString()  + "\n");

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
        }
        async public static void MachineHandshake()
        {
            await Console.Out.WriteLineAsync("Initializing handshake with server");
            var url = "http://localhost:5000/api/Handshake";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var user = "New machine appears";
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