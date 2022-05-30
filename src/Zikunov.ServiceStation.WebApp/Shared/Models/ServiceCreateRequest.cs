using System;
using System.ComponentModel.DataAnnotations;


namespace Zikunov.ServiceStation.WebApp.Shared.Models
{
    /// <summary>
    /// Service create request.
    /// </summary>
    public class ServiceCreateRequest
    { 
        /// <summary>
        /// Vehicle identifier.
        /// </summary>
        [Required]
        public int VehicleId { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Start.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// End.
        /// </summary>
        [Required]
        public DateTime End { get; set; }
    }
}
