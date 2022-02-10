using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Managers;

namespace WebShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        //ProductManager manager;
        //[Route("/GetAllProducts")]
        //[HttpGet]
        //public IActionResult GetAllProducts()
        //{
        //    try
        //    {
        //        manager = new ProductManager();
        //        return Ok(manager.GetAllProducts());
        //    }
        //    catch (Exception e)
        //    {

        //        return Problem(e.Message);
        //    }
        //}
        //[Route("/GetProductById")]
        //[HttpGet]
        //public IActionResult GetProductById(int id)
        //{
        //    try
        //    {
        //        manager = new ProductManager();
        //        return Ok(manager.GetProductById(id));
        //    }
        //    catch (Exception e)
        //    {

        //        return Problem(e.Message);
        //    }
        //}
    }
}
