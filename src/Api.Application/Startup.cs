using System;
using Api.CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
            services.AddControllers();
            services.AddSwaggerGen(c => ConfigurationSwagger(c));
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => ConfigurationSwaggerUI(c));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Metodos Privados

        private void ConfigurationSwagger(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API com AspNetCore 3.1",
                Description = "Arquitetura DDD",
                TermsOfService = new Uri("https://github.com/FelipeFalanque"),
                Contact = new OpenApiContact
                {
                    Name = "FelipeFalanque",
                    Email = "felipefalanque@gmail.com",
                    Url = new Uri("https://github.com/FelipeFalanque")
                },
                License = new OpenApiLicense
                {
                    Name = "Termo de Licença de Uso",
                    Url = new Uri("https://github.com/FelipeFalanque")
                }
            });
        }

        private void ConfigurationSwaggerUI(SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API com AspNetCore 3.1");
            c.RoutePrefix = string.Empty;
        }

        #endregion

    }
}
