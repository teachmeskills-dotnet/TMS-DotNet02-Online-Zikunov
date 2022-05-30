using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.Logic.Interfaces
{
    /// <summary>
    /// Service manager
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// Create Service.
        /// </summary>
        /// <param name="model">Service data transfer object.</param>
        /// <param name="userId">User identifier.</param>
        Task CreateAsync(ServiceDto model, string userId);

        /// <summary>
        /// Get service by vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">Vehicle Identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>Service data transfer object.</returns>
        Task<IEnumerable<ServiceDto>> GetAllByVehicleIdAsync(int vehicleId, string userId);

        /// <summary>
        /// Update service by identifier.
        /// </summary>
        /// <param name="model">Service data transfer object.</param>
        Task UpdateAsync(ServiceDto model, string userId);

        /// <summary>
        /// Delete service by identifier
        /// </summary>
        /// <param name="id">Service identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns></returns>
        Task DeleteAsync(int id, string userId);
    }
}
