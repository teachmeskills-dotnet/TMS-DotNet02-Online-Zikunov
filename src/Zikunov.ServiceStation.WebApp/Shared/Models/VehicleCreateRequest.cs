using System.ComponentModel.DataAnnotations;

namespace Zikunov.ServiceStation.WebApp.Shared.Models
{
    /// <summary>
    /// Vehicle create request.
    /// </summary>
    public class VehicleCreateRequest
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Brand.
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Number.
        /// </summary>
        [Required]
        public string Number { get; set; }
    }
}
