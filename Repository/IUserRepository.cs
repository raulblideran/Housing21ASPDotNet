using Housing21ASPdotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Housing21ASPdotNet.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        string AddNewUser(User user);
    }
}
