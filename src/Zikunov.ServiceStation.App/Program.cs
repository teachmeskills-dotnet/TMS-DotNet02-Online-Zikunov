using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Data.Contexts;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.Logic.Managers;
using Zikunov.ServiceStation.Logic.Models;

namespace Zikunov.ServiceStation.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>))
                .AddDbContext<ApplicationContext>()
                .BuildServiceProvider();

            var userRepositoryManager = serviceProvider.GetService<RepositoryManager<User>>();

            //code
            var userManager = serviceProvider.GetService<IUserManager>();

            var userTest = new UserDto
            {
                FullName = "Test",
                Email = "email@email.email",
                Password = "password"
            };

            await userManager.CreateAsync(userTest);
        }
    }
}
