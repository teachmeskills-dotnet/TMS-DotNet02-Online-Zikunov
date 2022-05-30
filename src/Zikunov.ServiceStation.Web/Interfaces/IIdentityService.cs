using System.Threading.Tasks;

namespace Zikunov.ServiceStation.Web.Interfaces
{
    /// <summary>
    /// Identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="value">Object.</param>
        /// <returns>Jwt token.</returns>
        Task<string> SignInAsync(object value);
    }
}
