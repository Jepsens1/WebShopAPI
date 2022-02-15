using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Managers;

namespace WebShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        TokenManager tokenManager;
        [Route("GetSession")]
        [HttpGet]
        public IActionResult GetToken(string username)
        {
            try
            {
                tokenManager = new TokenManager();
                return Ok(tokenManager.CreateSessionToken(username));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [Route("ValidateSession")]
        [HttpGet]
        public IActionResult ValidateSession(string token)
        {
            try
            {
                tokenManager = new TokenManager();
                return Ok(tokenManager.CheckIfTokenExist(token));
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }
    }
}
