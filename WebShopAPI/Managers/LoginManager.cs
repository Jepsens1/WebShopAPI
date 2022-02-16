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
        TokenManager tokenManager;
        /// <summary>
        /// Checks to see if a user with that username already exist in the database
        /// if username does not exist, we start generate salt, and hash password the generated salt, and register the user in database
        /// Sets the customer password to the new hashed password
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns if a user is created or not</returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Gets the user from database, if user is null, means that the user is not found
        /// if found, we will hash password with salt, we get the salt from the user in database
        /// Checks to see if the password we got from user in database, is equal to the password we just hashed
        /// if true we create a jwt token, else returns wrong password or username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Return token, not found or wrong credentials</returns>
        /// <exception cref="Exception"></exception>
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
                string saltpassword = Convert.ToBase64String(HashPassWordWithSalt(Encoding.UTF8.GetBytes(dataAccess.GetCustomerSalt(customer.Username)), Encoding.UTF8.GetBytes(password)));
                if (customer.Password == saltpassword)
                {
                    tokenManager = new TokenManager();
                    return tokenManager.CreateSessionToken(customer);
                }
                return "Wrong Username or Password";
            }
            catch (Exception)
            {

                throw new Exception();
            }

        }

        /// <summary>
        /// Generate a randon salt
        /// </summary>
        /// <returns>Returns a byte array with a random salt</returns>
        private byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[32];
                rng.GetBytes(data);
                return data;
            };
        }
        /// <summary>
        /// Uses sha256 to hash, the salt parameter comes from GenerateSalt Method and password from the userinput
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <returns>Byte array with new password that is hashed</returns>
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
