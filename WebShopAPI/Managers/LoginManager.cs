using WebShopAPI.Models;
using WebShopAPI.Database;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebShopAPI.Managers
{
    public class LoginManager
    {
        DataAccess dataAccess;
        public string SignUp(Customer customer)
        {
            try
            {
                dataAccess = new DataAccess();
                if (dataAccess.CheckIfUserAlreadyExist(customer.Username))
                    return "User already exist";

                string salt = Convert.ToBase64String(GenerateSalt());
                customer.Password = Convert.ToBase64String(HashPassWordWithSalt(Encoding.UTF8.GetBytes(salt), Encoding.UTF8.GetBytes(customer.Password)));
                dataAccess.RegisterCustomer(customer, salt);
                return "User Created";
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public string LoginValidate(string username, string password)
        {
            try
            {
                dataAccess = new DataAccess();
                Customer customer = dataAccess.Login(username);
                if (customer == null)
                {
                    return "User not found";
                }
                string saltpassword = Convert.ToBase64String(HashPassWordWithSalt(Encoding.UTF8.GetBytes(customer.PasswordSalt), Encoding.UTF8.GetBytes(password)));
                if (customer.Password == saltpassword)
                {
                    return "Login Success";
                }
                return "Wrong Username or Password";
            }
            catch (Exception)
            {

                throw new Exception();
            }
            
        }
        private byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[32];
                rng.GetBytes(data);
                return data;
            };
        }
        private byte[] HashPassWordWithSalt(byte[] salt, byte[] password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(password, salt));
            }
        }
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
       
    }
}
