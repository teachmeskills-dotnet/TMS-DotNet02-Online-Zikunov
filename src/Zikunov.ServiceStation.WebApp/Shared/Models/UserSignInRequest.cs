using System.ComponentModel.DataAnnotations;

namespace Zikunov.ServiceStation.WebApp.Shared.Models
{
    /// <summary>
    /// User SignIn operation request.
    /// </summary>
    public class UserSignInRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
