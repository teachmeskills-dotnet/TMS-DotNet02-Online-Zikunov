using System;
using Zikunov.ServiceStation.Data.Enums;

namespace Zikunov.ServiceStation.Data.Models
{
    /// <summary>
    /// Service.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Navigation property for vehicle.
        /// </summary>
        public Vehicle Vehicle{ get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Start.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public ServiceType TypeOfService { get; set; }
    }
}
