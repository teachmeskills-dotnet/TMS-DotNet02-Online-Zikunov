using System.ComponentModel.DataAnnotations;

namespace Zikunov.ServiceStation.WebApi.Contracts.Requests
{
    /// <summary>
    /// User SignUp operation request.
    /// </summary>
    public class UserSignUpRequest
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

        /// <summary>
        /// Fullname.
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        [Required]
        public string Phone { get; set; }
    }
}
