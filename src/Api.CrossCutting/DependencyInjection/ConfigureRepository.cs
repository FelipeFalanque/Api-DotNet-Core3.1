using System;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Repository.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(dbConnection)
            );
        }
    }
}
