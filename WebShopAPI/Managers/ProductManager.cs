using WebShopAPI.Models;
using WebShopAPI.Database;
namespace WebShopAPI.Managers
{
    public class ProductManager
    {
        DataAccess dataAccess;
        List<Product> products = new List<Product>();
        //public List<Product> GetAllProducts()
        //{
        //    //try
        //    //{
        //    //    dataAccess = new DataAccess();
        //    //    return dataAccess.GetProducts(products);
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    throw new Exception();
        //    //}
        //}
        //public Product GetProductById(int id)
        //{
        //    //try
        //    //{
        //    //    dataAccess = new DataAccess();
        //    //    return dataAccess.GetProductById(id);
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    throw new Exception();
        //    //}
        //}
    }
}
