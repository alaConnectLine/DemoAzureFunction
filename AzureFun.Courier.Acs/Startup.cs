using AzureFun.Courier.Acs.Handlers;
using CommonArea.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureFun.Courier.Acs.Startup))]
namespace AzureFun.Courier.Acs
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            //builder.Services.AddSingleton<IManagerCourier, AcsManager>();
            builder.Services.AddScoped<IManagerCourier, AcsManagerHandler>();
        }
    }
}
