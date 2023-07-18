using CommonArea.Enums;
using ConnectLine.CourierManager.SuperFunction.CosmosDbProvider;
using ConnectLine.CourierManager.SuperFunction.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConnectLine.CourierManager.SuperFunction.Handlers
{
    public class ConfigHandler
    {
        private CosmosDb _CosmosDb { get; set; }
        public ConfigHandler()
        {

        }
        public async Task<List<CourierConfig>> GetConfiguration(string id)
        {
            try
            {
                CosmosDb _CosmosDb = new();
                if (!_CosmosDb.Initialize()) throw new Exception("Could not initialize Cosmos DB");

                List<CourierConfig> courierConfigs = await _CosmosDb.GetItemFromContainerAsync(id);

                _CosmosDb.Dispose();

                return courierConfigs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddConfiguration(string jsonData, IConfiguration configuration)
        {
            try
            {
                CosmosDb _CosmosDb = new();
                if (!_CosmosDb.Initialize(configuration)) throw new Exception("Could not initialize Cosmos DB");
                
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());
                //if(!GenericHandler.CanCovert(jsonData, typeof(CourierConfig))) throw new Exception("Could not convert request to CourierConfig");
                CourierConfig courierConfig = JsonSerializer.Deserialize<CourierConfig>(jsonData, options);

                //ToDo : Validate courierConfig

                await _CosmosDb.AddItemsToContainerAsync(courierConfig);

                _CosmosDb.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
