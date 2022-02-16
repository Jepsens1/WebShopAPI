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
        /// <summary>
        /// Gets all the products from the database and adds to a list which we will return
        /// </summary>
        /// <param name="products"></param>
        /// <returns>Returns a list of products</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Returns a product we find by a id
        /// Returns the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a product</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// We get the user information from the angular page and inserts to the database, password is hashed from the loginmanager and we insert it to the database
        /// the salt parameter is also from loginmanager, we will store, so we can validate login credentials
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="salt"></param>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// This method checks to see if a user with the username already exists in the database. Returns false if a user with the given username doenst not exists
        /// Returns true if a user already exists with that username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Gets the username and password, from a username, if the database has rows we will return the customer with the data, else returns null
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                    Customer customer = new Customer((string)reader["customer_username"], (string)reader["customer_password"]);
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

        /// <summary>
        /// Returns the salt we stored from a user, will be used to validate a userlogin
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetCustomerSalt(string username)
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                cmd = new MySqlCommand("SELECT customer_passwordSalt FROM customer where customer_username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                reader.Read();
                string result = (string)reader["customer_passwordSalt"];
                conn.Close();
                return result;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
