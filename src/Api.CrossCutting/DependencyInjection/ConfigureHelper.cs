using Microsoft.Extensions.DependencyInjection;
using Api.Domain.Interfaces.Helpers.User;
using Api.Service.Helpers;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureHelper
    {
        public static void ConfigureDependenciesHelper(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserHelper, UserHelper>();
        }
    }
}
