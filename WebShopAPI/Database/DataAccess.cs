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

        public List<Product> GetProducts(List<Product> products)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("CALL sp_GetAllProduct()", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product((int)reader["product_id"], (string)reader["product_name"],
                        (int)reader["product_price"], (int)reader["product_quantity"], (string)reader["product_identifier"]));
                }
                conn.Close();
                return products;

            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        public Product GetProductById(int id)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("sp_GetProductById(@id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                Product product = new Product((int)reader["product_id"], (string)reader["product_name"], (int)reader["product_price"], (int)reader["product_quantity"], (string)reader["product_identifier"]);
                conn.Close();
                return product;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        public void PostToken(string token, string username)
        {
            try
            {
                int id = GetCustomerIdFromUsername(username);
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("CALL sp_postsessiontoken(@id, @token)", conn);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        private int GetCustomerIdFromUsername(string username)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("CALL sp_getcustomeridtosession(@username)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                reader.Read();
                int result = (int)reader["customer_id"];
                conn.Close();
                return result;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        public bool SelectToken(string token)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("call sp_validatetoken(@token)", conn);
                cmd.Parameters.AddWithValue("@token", token);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                return false;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
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
