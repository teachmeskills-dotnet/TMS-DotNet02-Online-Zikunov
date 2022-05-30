using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Data.Enums;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Exceptions;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.Logic.Managers
{
    /// <inheritdoc cref="IServiceManager"/>
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager<Service> _serviceRepository;
        private readonly IRepositoryManager<Vehicle> _vehicleRepository;

        public ServiceManager(
            IRepositoryManager<Service> serviceRepository,
            IRepositoryManager<Vehicle> vehicleRepository)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }

        public async Task CreateAsync(ServiceDto model, string userId)
        {
            if (string.IsNullOrEmpty(model.Comment))
            {
                throw new AppException($"'{nameof(model.Comment)}' cannot be null or empty.", nameof(model.Comment));
            }

            if (model.Start >= model.End)
            {
                throw new AppException($"'{nameof(model.Start)}' >= '{nameof(model.End)}'.");
            }

            if (model.VehicleId == 0)
            {
                throw new AppException($"'{nameof(model.VehicleId)}' cannot be zero identifier.", nameof(model.VehicleId));
            }

            var isUserVehicleExist = await _vehicleRepository
                .GetAll()
                .AnyAsync(v => v.Id == model.VehicleId && v.UserId == userId);

            if(!isUserVehicleExist)
            {
                throw new AppException($"'{nameof(model.VehicleId)}' forbidden.", nameof(model.VehicleId));
            }

            var service = new Service
            {
                Comment = model.Comment,
                Start = model.Start,
                End = model.End,
                TypeOfService = ServiceType.Inside,
                VehicleId = model.VehicleId
            };

            await _serviceRepository.CreateAsync(service);
            await _serviceRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var service = await _serviceRepository
               .GetAll()
               .Include(s => s.Vehicle)
               .SingleOrDefaultAsync(s => s.ServiceId == id && s.Vehicle.UserId == userId);

            if (service is null)
            {
                throw new NotFoundException($"'{nameof(id)}' service is not found.", nameof(id));
            }

            _serviceRepository.Delete(service);
            await _serviceRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceDto>> GetAllByVehicleIdAsync(int vehicleId, string userId)
        {
            var services = await _serviceRepository
                .GetAll()
                .Include(s => s.Vehicle)
                .Where(s => s.VehicleId == vehicleId && s.Vehicle.UserId == userId)
                .ToListAsync();

            if (!services.Any())
            {
                return new List<ServiceDto>();
            }

            var models = new List<ServiceDto>();

            foreach (var service in services)
            {
                models.Add(new ServiceDto
                {

                    ServiceId = service.ServiceId,
                    VehicleId = service.VehicleId,
                    Comment = service.Comment,
                    Start = service.Start,
                    End = service.End,
                    TypeOfService =  service.TypeOfService
                });
            }

            return models;
        }

        public async Task UpdateAsync(ServiceDto model, string userId)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var service = await _serviceRepository
               .GetAll()
               .Include(s => s.Vehicle)
               .SingleOrDefaultAsync(s => s.ServiceId == model.ServiceId && s.Vehicle.UserId == userId);

            if (service is null)
            {
                throw new NotFoundException($"'{nameof(model.ServiceId)}' service is not found.", nameof(model.ServiceId));
            }

            if (model.Start >= model.End)
            {
                throw new AppException($"'{nameof(model.Start)}' >= '{nameof(model.End)}'.");
            }

            if (service.Start != model.Start)
            {
                service.Start = model.Start;
            }

            if (service.End != model.End)
            {
                service.End = model.End;
            }

            await _serviceRepository.SaveChangesAsync();
        }
    }
}
