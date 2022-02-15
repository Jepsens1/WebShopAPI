using System.Text.RegularExpressions;
using WebShopAPI.Models;
using WebShopAPI.Database;
namespace WebShopAPI.Managers
{
    public class TokenManager
    {
        DataAccess dataAccess;
        public SessionToken CreateSessionToken(string username)
        {
            try
            {
                dataAccess = new DataAccess();
                string token = "";
                for (int i = 0; i < 5; i++)
                {
                    token += Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                }
                token = Regex.Replace(token, @"[^0-9a-zA-Z]+", "");
                dataAccess.PostToken(token, username);
                return new SessionToken(token);
            }
            catch (Exception)
            {

                throw new Exception();
            }
           
        }
        public bool CheckIfTokenExist(string token)
        {
            dataAccess = new DataAccess();
            try
            {
                if (dataAccess.SelectToken(token))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
