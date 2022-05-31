using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.Logic.Interfaces
{
    /// <summary>
    /// Vehicle manager
    /// </summary>
    public interface IVehicleManager
    {
        /// <summary>
        /// Create Vehicle.
        /// </summary>
        /// <param name="model">Vehicle data transfer object.</param>
        Task CreateAsync(VehicleDto model);

        /// <summary>
        /// Get vehicle by user identifier.
        /// </summary>
        /// <param name="userId">User Identifier.</param>
        /// <returns>Vehicle data transfer object.</returns>
        Task<IEnumerable<VehicleDto>> GetAllByUserIdAsync(string userId);

        /// <summary>
        /// Update vehicle by identifier.
        /// </summary>
        /// <param name="model">Vehicle data transfer object.</param>
        Task UpdateAsync(VehicleDto model);

        /// <summary>
        /// Delete vehicle by identifier
        /// </summary>
        /// <param name="id">Vehicle identifier</param>
        /// <param name="id">User identifier</param>
        /// <returns></returns>
        Task DeleteAsync(int id, string userId);
    }
}
