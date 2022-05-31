using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.WebApi.Contracts.Responses
{
    /// <summary>
    /// User authenticate response.
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Fullname.
        /// </summary>
        public string FullName{ get; set; }
        
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="user">User database model.</param>
        /// <param name="token">Jwt token.</param>
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Phone = user.Phone;
            FullName = user.FullName;
            Email = user.Email;
            Token = token;
        }
    }
}
