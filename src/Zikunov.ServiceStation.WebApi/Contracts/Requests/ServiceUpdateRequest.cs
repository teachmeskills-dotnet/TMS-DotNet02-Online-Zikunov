using System;

namespace Zikunov.ServiceStation.WebApi.Contracts.Requests
{
    /// <summary>
    /// Service update request.
    /// </summary>
    public class ServiceUpdateRequest
    {
        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Start.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End.
        /// </summary>
        public DateTime End { get; set; }
    }
}
