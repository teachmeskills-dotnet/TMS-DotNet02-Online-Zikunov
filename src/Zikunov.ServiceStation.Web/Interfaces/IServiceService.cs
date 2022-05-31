using System.Threading.Tasks;

namespace Zikunov.ServiceStation.Web.Interfaces
{
    /// <summary>
    /// Service service.
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Add service.
        /// </summary>
        /// <param name="value">Object.</param>
        /// <param name="token">Jwt token.</param>
        Task AddAsync(object value, string token);
    }
}
