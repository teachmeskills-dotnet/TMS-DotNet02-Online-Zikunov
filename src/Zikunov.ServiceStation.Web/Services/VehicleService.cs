using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Web.Interfaces;

namespace Zikunov.ServiceStation.Web.Services
{
    /// <inheritdoc cref="IVehicleService"/>
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<VehicleItemResponse>> GetAllAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Vehicle");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            var vehicles = await response.Content.ReadFromJsonAsync<List<VehicleItemResponse>>();
            return vehicles;
        }
    }
}
