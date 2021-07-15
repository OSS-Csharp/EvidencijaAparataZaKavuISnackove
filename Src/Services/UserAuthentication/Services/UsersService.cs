using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _httpClient.GetFromJsonAsync<ApplicationUser[]>($"https://localhost:5001/users");
        }
    }
}
