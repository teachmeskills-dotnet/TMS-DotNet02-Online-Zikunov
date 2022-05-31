using System.Collections.Generic;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;

namespace Zikunov.ServiceStation.Web.Interfaces
{
    /// <summary>
    /// Vehicle service.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Get all services.
        /// </summary>
        /// <param name="token">Jwt token.</param>
        /// <returns>Service collection.</returns>
        Task<IEnumerable<VehicleItemResponse>> GetAllAsync(string token);
    }
}
