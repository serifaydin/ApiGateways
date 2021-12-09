using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MyOcelot.Services.Weather.Api
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

            var authenticationProviderKey = "MyOcelot";

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = authenticationProviderKey;
                option.DefaultChallengeScheme = authenticationProviderKey;
            })
                .AddJwtBearer(authenticationProviderKey, options =>
                 {
                     options.RequireHttpsMetadata = false;
                     options.SaveToken = true;

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTModel.Key)),
                         ValidateIssuer = true,
                         ValidIssuer = JWTModel.Issuer,
                         ValidateAudience = true,
                         ValidAudience = JWTModel.Audience,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero,
                         RequireExpirationTime = true
                     };
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class JWTModel
    {
        public static string Key = "MyOcelotSecretKey";
        public static string Issuer = "MyOcelot";
        public static string Audience = "Serif";
    }
}
