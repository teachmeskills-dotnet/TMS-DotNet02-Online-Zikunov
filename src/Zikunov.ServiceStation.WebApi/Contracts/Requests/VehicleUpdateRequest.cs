using Zikunov.ServiceStation.Core.Enums;

namespace Zikunov.ServiceStation.WebApi.Contracts.Requests
{
    /// <summary>
    /// Vehicle update request.
    /// </summary>
    public class VehicleUpdateRequest
    {
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
