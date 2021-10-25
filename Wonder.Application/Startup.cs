using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Wonder.Domain.DomainServices;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Infra.Data.Context;
using Wonder.Infra.Data.Repository;
using Wonder.Service.Application;
using Wonder.Service.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wonder.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Wonder.Application.Token;
using Wonder.Service.Contracts;

namespace Wonder.Application
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Wonder.Application", Version = "v1"});
            });
            
            services.AddScoped<IStockRepository, StockRepositoryImpl>();
            services.AddScoped<IAppStockContracts, AppStockContractsImpl>();
            services.AddScoped<IUserContracts, UserContractsImpl>();
            services.AddScoped<StockService, StockService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddDbContext<PostgreSqlContext>();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PostgreSqlContext>();

            var key = Encoding.ASCII.GetBytes("Secret_Key-12345678");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(option =>
            //     {
            //         option.SaveToken = true;
            //         option.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuer = false,
            //             ValidateAudience = false,
            //             ValidateLifetime = true,
            //             ValidateIssuerSigningKey = true,
            //
            //             ValidIssuer = "Teste.Securiry.Bearer",
            //             ValidAudience = "Teste.Securiry.Bearer",
            //             IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
            //             
            //         };
            //
            //         option.Events = new JwtBearerEvents
            //         {
            //             OnAuthenticationFailed = context =>
            //             {
            //                 Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
            //                 return Task.CompletedTask;
            //             },
            //             OnTokenValidated = context =>
            //             {
            //                 Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
            //                 return Task.CompletedTask;
            //             }
            //         };
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wonder.Application v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseDeveloperExceptionPage();
        }
    }
}