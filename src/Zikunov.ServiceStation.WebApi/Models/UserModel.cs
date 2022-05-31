namespace Zikunov.ServiceStation.WebApi.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// FullName.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
