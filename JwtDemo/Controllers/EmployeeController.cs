using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JwtDemo.Models;

namespace JwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }
        [Authorize]
        [HttpGet]
        [Route("Details")]
        public string Details() {
            return "Authenticated with JWT"; 
        }

        [Authorize]
        [HttpPost]
        public string AddUser(Users user) {
            return "User added with username " + user.name;
                }
    }
}
