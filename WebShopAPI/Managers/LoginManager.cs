﻿using WebShopAPI.Models;
using WebShopAPI.Database;
using System.Security.Cryptography;
using System.Text;

namespace WebShopAPI.Managers
{
    public class LoginManager
    {
        DataAccess dataAccess = new DataAccess();
        public void SignUp(Customer customer)
        {
            string salt = Convert.ToBase64String(GenerateSalt());
            customer.Password = Convert.ToBase64String(HashPassWordWithSalt(Encoding.UTF8.GetBytes(salt), Encoding.UTF8.GetBytes(customer.Password)));
            dataAccess.RegisterCustomer(customer, salt);
        }

        public string LoginValidate(string username, string password)
        {
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