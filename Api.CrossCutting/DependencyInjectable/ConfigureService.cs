using Api.Domain.Interfaces.Services.Item;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Services.Reservation;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjectable
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<IItemService, ItemService>();
            serviceCollection.AddTransient<IReservationService, ReservationService>();
        }
    }
}