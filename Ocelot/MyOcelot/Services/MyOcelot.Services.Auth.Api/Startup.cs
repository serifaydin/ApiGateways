using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyOcelot.Services.Auth.Api.Middleware;
using MyOcelot.Services.Auth.Api.Models;
using System;
using System.Text;

namespace MyOcelot.Services.Auth.Api
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
            services.AddSingleton<IJWTManager>(new JWTManager());

            services.AddControllers();

            var authenticationProviderKey = "MyOcelot";

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = authenticationProviderKey;
                option.DefaultChallengeScheme = authenticationProviderKey;
            }).AddJwtBearer(authenticationProviderKey, options =>
             {
                 options.RequireHttpsMetadata = false;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTModel.Key)),
                     ValidateIssuer = true,
                     ValidIssuer = JWTModel.Issuer,
                     ValidateAudience = true,
                     ValidAudience = JWTModel.Audience,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero, //Verilen sürede token sonlanır. eğer Zero olarak verilmez ise default da 5 dakikadır. Jetonunuzun süresinin tam zamanında bitmesini istiyorsanız; ClockSkew'i aşağıdaki gibi sıfıra ayarlamanız gerekir,
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
}