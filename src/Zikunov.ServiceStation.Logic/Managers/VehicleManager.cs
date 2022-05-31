using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Core.Enums;
using Zikunov.ServiceStation.Data.Enums;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Exceptions;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.Logic.Managers
{
    /// <inheritdoc cref="IUserManager"/>
    public class VehicleManager : IVehicleManager
    {
        private readonly IRepositoryManager<Vehicle> _vehicleRepository;

        public VehicleManager(IRepositoryManager<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }

        public async Task CreateAsync(VehicleDto model)
        {
            if (string.IsNullOrEmpty(model.Brand))
            {
                throw new AppException($"'{nameof(model.Brand)}' cannot be null or empty.", nameof(model.Brand));
            }

            if (string.IsNullOrEmpty(model.Number))
            {
                throw new AppException($"'{nameof(model.Number)}' cannot be null or empty.", nameof(model.Number));
            }

            var vehicle = new Vehicle
            {
                UserId = model.UserId,
                Brand = model.Brand,
                Number = model.Number,
                VehicleType = VehicleType.Car
            };

            await _vehicleRepository.CreateAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var vehicle = await _vehicleRepository.GetEntityAsync(v => v.Id == id && v.UserId == userId);

            if (vehicle is null)
            {
                throw new NotFoundException($"'{nameof(id)}' vehicle is not found.", nameof(id));
            }

            _vehicleRepository.Delete(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleDto>> GetAllByUserIdAsync(string userId)
        {
            var vehicles = await _vehicleRepository.GetAll()
                .Where(v => v.UserId == userId)
                .ToListAsync();

            if(!vehicles.Any())
            {
                return new List<VehicleDto>();
            }

            var vehicleDtos = new List<VehicleDto>();

            foreach(var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto
                {
                    Id = vehicle.Id,
                    UserId = vehicle.UserId,
                    Brand = vehicle.Brand,
                    Number = vehicle.Number,
                    VehicleType = vehicle.VehicleType,
                });
            }

            return vehicleDtos;
        }

        public async Task UpdateAsync(VehicleDto model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var vehicle = await _vehicleRepository.GetEntityAsync(v => v.Id == model.Id && v.UserId == model.UserId);

            if (vehicle is null)
            {
                throw new NotFoundException($"'{nameof(model.Id)}' vehicle is not found.", nameof(model.Id));
            }

            const string defaultSwaggerStringTypeValue = "string"; 

            if(vehicle.Brand != model.Brand && model.Brand != defaultSwaggerStringTypeValue)
            {
                vehicle.Brand = model.Brand;
            }

            if (vehicle.Number != model.Number && model.Number != defaultSwaggerStringTypeValue)
            {
                vehicle.Number = model.Number;
            }

            if (vehicle.VehicleType != model.VehicleType && model.VehicleType != VehicleType.Unknown)
            {
                vehicle.VehicleType = model.VehicleType;
            }

            await _vehicleRepository.SaveChangesAsync();
        }
    }
}
