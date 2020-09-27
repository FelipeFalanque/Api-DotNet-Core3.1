using System;
using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Api.CrossCutting.Mapping;
using AutoMapper;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            services.AddControllers();

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            services.AddSingleton(ConfigurationMapper());

            SigningConfigurations signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

            services.AddAuthentication(ConfigurationAddAuthentication()).AddJwtBearer(ConfigurationAddJwtBearer(signingConfigurations, tokenConfigurations));

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(ConfigurationAddAuthorization());

            services.AddSingleton(tokenConfigurations);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AplicarMigracoes(app);
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

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Entre com o Token JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string>()
                }
            });

        }

        private void ConfigurationSwaggerUI(SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API com AspNetCore 3.1");
            c.RoutePrefix = string.Empty;
        }

        private Action<JwtBearerOptions> ConfigurationAddJwtBearer(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            return bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            };
        }

        private Action<AuthenticationOptions> ConfigurationAddAuthentication()
        {
            return authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }

        private Action<AuthorizationOptions> ConfigurationAddAuthorization()
        {
            return auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            };
        }

        private IMapper ConfigurationMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        private void AplicarMigracoes(IApplicationBuilder app)
        {
            bool aplicarMigration = false;

            bool.TryParse(
                Environment.GetEnvironmentVariable("RUN_MIGRATION").ToLower(),
                out aplicarMigration);

            if (aplicarMigration)
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
        
        #endregion

    }
}
