using ConnectLine.CourierManager.SuperFunction.Handlers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(ConnectLine.CourierManager.SuperFunction.Startup))]
namespace ConnectLine.CourierManager.SuperFunction
{
    public class Startup : FunctionsStartup
    {

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            //string cs = Environment.GetEnvironmentVariable("ConnectionString");
            //string cs = "AccountEndpoint=https://couriermanagercosmosdemo.documents.azure.com:443/;AccountKey=2eqcrZow1NBIFbSbQ1drPylNaj70UurVlPGz5CChgRMEsQxlhBq1Bo8lsjBHNaYMMsku9FoqJCB0ACDbIeLMLw==;";
            //builder.ConfigurationBuilder.AddAzureAppConfiguration(cs);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.AddScoped<ConfigHandler>();
            builder.Services.AddScoped<CourierHandler>();

            //var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
            //builder.Services.AddAppConfiguration(configuration);

        }

        private IConfiguration BuildConfiguration(string applicationRootPath)
        {
            var config =
                new ConfigurationBuilder()
                    .SetBasePath(applicationRootPath)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

            return config;
        }

    }
}
