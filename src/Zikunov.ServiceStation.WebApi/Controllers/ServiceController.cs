using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.Logic.Models;
using Zikunov.ServiceStation.WebApi.Attributes;
using Zikunov.ServiceStation.WebApi.Contracts.Requests;
using Zikunov.ServiceStation.WebApi.Models;

namespace Zikunov.ServiceStation.WebApi.Controllers
{
    [OwnAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IServiceManager _serviceManager;

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetAsync(int vehicleId)
        {
            var user = (UserModel)HttpContext.Items["User"];
            var services = await _serviceManager.GetAllByVehicleIdAsync(vehicleId, user.Id);

            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ServiceCreateRequest request)
        {
            var user = (UserModel)HttpContext.Items["User"];

            var service = new ServiceDto
            {
                Comment = request.Comment,
                VehicleId = request.VehicleId,
                Start = request.Start,
                End = request.End,
            };

            await _serviceManager.CreateAsync(service, user.Id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ServiceUpdateRequest request)
        {
            var user = (UserModel)HttpContext.Items["User"];
                
            var service = new ServiceDto
            {
                ServiceId = id,
                Start = request.Start,
                End = request.End
            };
             
            await _serviceManager.UpdateAsync(service, user.Id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = (UserModel)HttpContext.Items["User"];
            await _serviceManager.DeleteAsync(id, user.Id);

            return Ok();
        }
    }
}
