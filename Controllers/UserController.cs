using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using Housing21ASPdotNet.Models;
using Housing21ASPdotNet.Repository;

namespace Housing21ASPdotNet.Controllers
{
    public class UserController : Controller
    {

        private readonly ClientSettings _clientSettings;
        private IUserRepository _usersRepository;

        public UserController(IOptions<ClientSettings> clientSettings) {

            _clientSettings = clientSettings.Value;
            this._usersRepository = new UserRepository(clientSettings, new Housing21appContext());

        }
        public async Task<IActionResult> Index()
        {
            var retrievedUsers = _usersRepository?.GetAllUsers().Result;
            return View(retrievedUsers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            try
            {
                var result = _usersRepository.AddNewUser(user);

                if (result.Equals("successful"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else if(result.Equals("unsuccessful")) 
                {
                    return View();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Export()
        {
            var retrievedUsers = _usersRepository?.GetAllUsers().Result;
            string[] columnNames = new string[] { "ID", "Name", "Date of birth", "Phone Number", "Email" };

            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ",";
            }

            csv += "\r\n";

            foreach (var user in retrievedUsers)
            {
                csv += user.Id.ToString().Replace(",", " ") + ',';
                csv += user.Name.Replace(",", " ") + ',';
                csv += user.Dob.ToString().Replace(",", " ") + ',';
                csv += user.Phonenumber.Replace(",", " ") + ',';
                csv += user.Email.ToString().Replace(",", " ") + ',';
                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "report.csv");

        }


    }
}
