using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class VehicleController : ControllerBase
    {
        private IVehicleManager _vehicleManager;

        public VehicleController(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager ?? throw new ArgumentNullException(nameof(vehicleManager));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = (UserModel)HttpContext.Items["User"];
            var vehicles = await _vehicleManager.GetAllByUserIdAsync(user.Id);

            var vehicleModels = new List<VehicleItemResponse>();
            foreach (var vehicle in vehicles)
            {
                vehicleModels.Add(new VehicleItemResponse
                {
                    Id = vehicle.Id,
                    Brand = vehicle.Brand,
                    Number = vehicle.Number,
                    VehicleType = vehicle.VehicleType,
                    UserId = vehicle.UserId
                });
            }

            return Ok(vehicleModels);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] VehicleCreateRequest request)
        {
            var vehicle = new VehicleDto
            {
                UserId = ((UserModel)HttpContext.Items["User"]).Id,
                Brand = request.Brand,
                Number = request.Number,
            };
 
            await _vehicleManager.CreateAsync(vehicle);
            
            return Ok();
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] VehicleUpdateRequest request)
        {
            var vehicle = new VehicleDto
            {
                Id = id,
                UserId = ((UserModel)HttpContext.Items["User"]).Id,
                Brand = request.Brand,
                Number = request.Number,
                VehicleType = request.VehicleType
            };

            await _vehicleManager.UpdateAsync(vehicle);
             
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = (UserModel)HttpContext.Items["User"];
            await _vehicleManager.DeleteAsync(id, user.Id);

            return Ok();
        }
    }
}
