using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Domain.Utils;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DataBaseTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Util.ObterNumeroMenorQue10EmDoisDigitos(DateTime.Now.Day)}_{Util.ObterNumeroMenorQue10EmDoisDigitos(DateTime.Now.Month)}_{DateTime.Now.Year}_{Util.ObterNumeroMenorQue10EmDoisDigitos(DateTime.Now.Hour)}_{Util.ObterNumeroMenorQue10EmDoisDigitos(DateTime.Now.Minute)}_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider {get; private set;}

        public DataBaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseMySql($"Persist Security Info=True;Server=localhost;Database={dataBaseName};User=root;Password=root"),
                  ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
