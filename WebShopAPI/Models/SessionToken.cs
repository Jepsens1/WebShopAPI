namespace WebShopAPI.Models
{
    public class SessionToken
    {
        public string Token { get; set; }

        public SessionToken(string token)
        {
            Token = token;
        }
    }
}
