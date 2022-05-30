using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Data.Enums;

namespace Zikunov.ServiceStation.Logic.Models
{
    /// <summary>
    /// Service.
    /// </summary>
    public class ServiceDto
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
