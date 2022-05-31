using Zikunov.ServiceStation.Core.Enums;

namespace Zikunov.ServiceStation.WebApp.Shared.Models
{
    /// <summary>
    /// Vehicle item response
    /// </summary>
    public class VehicleItemResponse
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
