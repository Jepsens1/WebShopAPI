using MySql.Data.MySqlClient;
using WebShopAPI.Models;
namespace WebShopAPI.Database
{
    public class DataAccess
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        const string ConnectionString = "Server=localhost;Database=angularwebshop;Uid=root;Pwd=Kode0911;";

        //public List<Product> GetProducts(List<Product> products)
        //{
        //    try
        //    {
        //        conn = new MySqlConnection(ConnectionString);
        //        conn.Open();
        //        cmd = new MySqlCommand("", conn);
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            products.Add(new Product());
        //        }
        //        conn.Close();
        //        return products;

        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception();
        //    }
        //}
        //public Product GetProductById(int id)
        //{
        //    try
        //    {
        //        conn = new MySqlConnection(ConnectionString);
        //        conn.Open();
        //        cmd = new MySqlCommand("", conn);
        //        reader = cmd.ExecuteReader();
        //        reader.Read();
        //        Product product = new Product();
        //        conn.Close();
        //        return product;
        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception();
        //    }
        //}
        public void RegisterCustomer(Customer customer, string salt)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("Call sp_RegisterUser(@username,@password,@passwordsalt,@firstname,@lastname,@email,@address,@phonenumber)", conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Parameters.AddWithValue("@passwordsalt", salt);
                cmd.Parameters.AddWithValue("@firstname", customer.Firstname);
                cmd.Parameters.AddWithValue("@lastname", customer.Lastname);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@phonenumber", customer.PhoneNumber);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        public bool CheckIfUserAlreadyExist(string username)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("CALL sp_CheckIfUserExist(@username)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
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
        public Customer Login(string username)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("Call sp_LoginValidate(@username)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Customer customer = new Customer((string)reader["customer_username"], (string)reader["customer_password"], (string)reader["customer_passwordSalt"]);
                    conn.Close();
                    return customer;
                }
                conn.Close();
                return null;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
