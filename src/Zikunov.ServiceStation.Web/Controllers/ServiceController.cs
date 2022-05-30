using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Web.Interfaces;

namespace Zikunov.ServiceStation.Web.Controllers
{
    /// <summary>
    /// Service controller.
    /// </summary>
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="serviceService">Service service.</param>
        /// <param name="vehicleService">Vehicle service.</param>
        public ServiceController(
            IServiceService serviceService,
            IVehicleService vehicleService)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
        }

        /// <summary>
        /// Send (Get).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SendAsync()
        {
            var token = User.FindFirst(ClaimTypes.Name).Value;
            var vehicles = await _vehicleService.GetAllAsync(token);

            SelectList vehiclesList = new SelectList(vehicles, "Id", "Brand");
            ViewBag.Vehicles = vehiclesList;

            return View();
        }

        /// <summary>
        /// Send (Post).
        /// </summary>
        /// <param name="request">Service create request.</param>
        [HttpPost]
        public async Task<IActionResult> SendAsync(ServiceCreateRequest request)
        {
            var token = User.FindFirst(ClaimTypes.Name).Value;

            await _serviceService.AddAsync(request, token);

            return RedirectToAction("Index", "Home");
        }
    }
}
