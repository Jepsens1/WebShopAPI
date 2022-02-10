using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Models;
using WebShopAPI.Managers;

namespace WebShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        LoginManager loginManager = new LoginManager();
        [Route("/Signup")]
        [HttpPost]
        public IActionResult SignUp(Customer customer)
        {
            try
            {
                loginManager.SignUp(customer);
                return Ok("User was made");
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }
        [Route("/SignIn")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                return Ok(loginManager.LoginValidate(username, password));
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }
    }
}
