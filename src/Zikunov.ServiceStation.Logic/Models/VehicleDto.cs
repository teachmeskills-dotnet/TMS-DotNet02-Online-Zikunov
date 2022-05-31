using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Core.Enums;
using Zikunov.ServiceStation.Data.Enums;

namespace Zikunov.ServiceStation.Logic.Models
{
    /// <summary>
    /// Vehicle.
    /// </summary>
    public class VehicleDto
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
    }
}
