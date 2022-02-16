namespace WebShopAPI.Models
{
    public class Customer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Customer()
        {

        }
        public Customer(string username, string password, string Fname, string Lname, string email, string address, string phone)
        {
            Username = username;
            Password = password;
            Firstname = Fname;
            Lastname = Lname;
            Email = email;
            Address = address;
            PhoneNumber = phone;
        }
        public Customer(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
