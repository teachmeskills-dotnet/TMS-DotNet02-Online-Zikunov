using System.Collections.Generic;
using Zikunov.ServiceStation.Core.Enums;

namespace Zikunov.ServiceStation.Data.Models
{
    /// <summary>
    /// Vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// Navigation property for service.
        /// </summary>
        public ICollection<Service> Services { get; set; }
    }
}
