using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebShopAPI.Models;
namespace WebShopAPI.Managers
{
    public class TokenManager
    {
        /// <summary>
        /// This method generates a jwt token that we will be used on angular
        /// the token will expire after 15 minuted
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns the token</returns>
        /// <exception cref="Exception"></exception>
        public string CreateSessionToken(Customer customer)
        {
            try
            {
                List<Claim> claimss = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer.Username)
                };
                SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4"));
                SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: claimss,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: cred);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
