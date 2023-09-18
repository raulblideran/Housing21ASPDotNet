using Housing21ASPdotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Housing21ASPdotNet.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly Housing21appContext _context;
        private readonly ClientSettings _clientSettings;

        public UserRepository(IOptions<ClientSettings> clientSettings, Housing21appContext context)
        {
            this._clientSettings = clientSettings.Value;
           this._context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {

            string baseUrl = _clientSettings.ClientBaseUrl;
            string apiUrlUser = baseUrl + "/api/housing/users";

            List<User> allUsers = new List<User>();

            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.GetAsync(apiUrlUser);
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                allUsers = JsonConvert.DeserializeObject<List<User>>(results);
                return allUsers;
            }

            return null;
        }

        public string AddNewUser(User user)
        {
            string baseUrl = _clientSettings.ClientBaseUrl;
            string apiUrlUser = baseUrl + "/api/housing/users";

            HttpClient client = new HttpClient();
            var postResponse = client.PostAsJsonAsync<User>(apiUrlUser, user);
            postResponse.Wait();
            var result = postResponse.Result;
            
            if(result.IsSuccessStatusCode)
            {
                return "successful";
            }
            else
            {
                return "unsuccessful";
            }
        }

    }
}
