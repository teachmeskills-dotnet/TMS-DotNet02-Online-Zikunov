using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Web.Interfaces;

namespace Zikunov.ServiceStation.Web.Services
{
    /// <inheritdoc cref="IIdentityService"/>
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> SignInAsync(object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/user/SignIn");
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            using var response = await _httpClient.SendAsync(request);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            var service = await response.Content.ReadFromJsonAsync<UserAuthModel>();
            return service.Token;
        }
    }
}
