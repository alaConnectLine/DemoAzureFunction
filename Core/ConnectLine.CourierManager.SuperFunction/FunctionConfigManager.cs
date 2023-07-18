using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.AppConfiguration;
using ConnectLine.CourierManager.SuperFunction.Handlers;
using Microsoft.Extensions.Configuration;

namespace ConnectLine.CourierManager.SuperFunction
{
    public class FunctionConfigManager
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigHandler _handler;
        public FunctionConfigManager(ConfigHandler handler, IConfiguration configuration)
        {
            _configuration = configuration;
            _handler = handler;
        }

        [FunctionName("UpdateConfiguration")]
        public static async Task<IActionResult> RunUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<dynamic>(requestBody);
            string connectionString = data?.connectionString;
            string key = data?.key;
            string value = data?.value;


            // Authenticate with Azure
            //var credential = new DefaultAzureCredential();

            var client = new ConfigurationClient(connectionString);

            //var currentVal = client.GetConfigurationSettingAsync(key, value);

            ConfigurationSetting setting = await client.GetConfigurationSettingAsync(key);

            //Update the configuration value
            setting.Value = value;
            await client.SetConfigurationSettingAsync(setting);

            return new OkResult();
        }

        [FunctionName("AddConfiguration")]
        public async Task<IActionResult> RunAdd(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<dynamic>(requestBody);

            var response = await _handler.AddConfiguration(requestBody, _configuration);

            return new OkObjectResult(response);
        }

        [FunctionName("DeleteConfiguration")]
        public static async Task<IActionResult> RunDelete(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<dynamic>(requestBody);
            string connectionString = data?.connectionString;
            string key = data?.key;
            string value = data?.value;


            // Authenticate with Azure
            //var credential = new DefaultAzureCredential();

            var client = new ConfigurationClient(connectionString);

            //var currentVal = client.GetConfigurationSettingAsync(key, value);

            ConfigurationSetting setting = await client.GetConfigurationSettingAsync(key);

            //Update the configuration value
            setting.Value = value;
            await client.SetConfigurationSettingAsync(setting);

            return new OkResult();
        }

        [FunctionName("GetConfiguration")]
        public async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            string id = req.Query["id"];
            if (string.IsNullOrEmpty(id)) return new BadRequestResult();

            var response = await _handler.GetConfiguration(id);

            return new OkObjectResult(response);
        }
    }
}
