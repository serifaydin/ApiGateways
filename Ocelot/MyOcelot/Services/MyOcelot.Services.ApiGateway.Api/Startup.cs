using CacheManager.Core.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Logging;
using Ocelot.Middleware;
using System;
using System.Configuration;
using System.Text;

namespace MyOcelot.Services.ApiGateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var authenticationProviderKey = "MyOcelot";

            Action<JwtBearerOptions> options = o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = JWTModel.Issuer,
                    ValidAudience = JWTModel.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTModel.Key))
                };
            };

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = authenticationProviderKey;
                option.DefaultChallengeScheme = authenticationProviderKey;

            }).AddJwtBearer(authenticationProviderKey, options);

            services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOcelot().Wait();
        }
    }

    public static class JWTModel
    {
        public static string Key = "MyOcelotSecretKey";
        public static string Issuer = "MyOcelot";
        public static string Audience = "Serif";
    }
}
