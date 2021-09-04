using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Infra;
using Wonder.Infra.Data.Repository;
using Wonder.Service.Application;
using Wonder.Service.Contracts;

namespace Wonder.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //.UseDotNurseInjector()
        //.ConfigureServices((_, services) =>
        //   services.AddSingleton<IStockRepository, StockRepositoryImpl>()
        //     .AddSingleton<IAppStockContracts, AppStockContractsImpl>());

    }
}