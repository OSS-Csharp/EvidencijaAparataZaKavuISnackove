using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly HttpClient _httpClient;
        public ScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Schedule> GetUserSchedule(string userName)
        {
            return await _httpClient.GetFromJsonAsync<Schedule>($"https://localhost:5001/schedule/{userName}");
        }
    }
}
