using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Zikunov.ServiceStation.Data.Models
{
    /// <summary>
    /// User.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// FullName.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Navigation property for vehicle.
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
