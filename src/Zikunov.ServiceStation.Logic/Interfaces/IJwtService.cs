using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zikunov.ServiceStation.Logic.Interfaces
{
    /// <summary>
    /// JWT service.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generate Jwt token.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="secret">Secret key</param>
        /// <returns></returns>
        string GenerateJwtToken(string userId, string secret);
    }
}
