using ConnectLine.CourierManager.SuperFunction.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace ConnectLine.CourierManager.SuperFunction.CosmosDbProvider
{
    public class CosmosDb
    {
        // The Azure Cosmos DB endpoint.
        //private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
        private string EndpointUri { get; set; }

        // The primary key for the Azure Cosmos account.
        //private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        private string PrimaryKey { get; set; }

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database.
        private Database database;

        // The container.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "DemoComaDB";
        private string containerId = "DemoComa";

        public CosmosDb()
        {
            EndpointUri = "https://couriermanagercosmosdemo.documents.azure.com:443/";
            PrimaryKey = "2eqcrZow1NBIFbSbQ1drPylNaj70UurVlPGz5CChgRMEsQxlhBq1Bo8lsjBHNaYMMsku9FoqJCB0ACDbIeLMLw==";
        }

        // <Initialize>
        /// <summary>
        /// Initialize Cosmos Database
        /// </summary>
        public bool Initialize(IConfiguration configuration = null)
        {
            try
            {
                // Read configuration data
                //string keyName = "TestApp:Settings:Message";
                //string message = configuration[keyName];

                // Create a new instance of the Cosmos Client
                this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBComaDemo" });

                // Get Database
                this.database = this.cosmosClient.GetDatabase(databaseId);

                // Get container
                this.container = this.database.GetContainer(containerId);

                if (this.container == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // </Initialize>

        // <Dispose>
        /// <summary>
        /// Dispose Cosmos Database
        /// </summary>
        public void Dispose()
        {
            //Dispose of CosmosClient
            this.cosmosClient.Dispose();
        }
        // </Dispose>


        // <QueryItemsAsync>
        /// <summary>
        /// Run a query (using Azure Cosmos DB SQL syntax) against the container
        /// </summary>
        public async Task<List<CourierConfig>> GetItemFromContainerAsync(string id)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.id = '{id}'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<CourierConfig> queryResultSetIterator = this.container.GetItemQueryIterator<CourierConfig>(queryDefinition);

            List<CourierConfig> courierConfigs = new List<CourierConfig>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<CourierConfig> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (CourierConfig config in currentResultSet)
                {
                    courierConfigs.Add(config);
                    Console.WriteLine("\tRead {0}\n", config);
                }
            }

            return courierConfigs;
        }
        // </QueryItemsAsync>



        // <AddItemsToContainerAsync>
        /// <summary>
        /// Add Config items to the container
        /// </summary>
        public async Task AddItemsToContainerAsync(CourierConfig courierConfig)
        {

            try
            {
                // Read the item to see if it exists.  
                string uniqueId = courierConfig.SelectedCourier.ToString() + "." + courierConfig.Customer.Id.ToString() + "." + courierConfig.Customer.CustomerCompany.Id.ToString();
                ItemResponse<CourierConfig> courierConfigResponse = await this.container.ReadItemAsync<CourierConfig>(uniqueId, new PartitionKey(courierConfig.PartitionKey));
                Console.WriteLine("Item in database with id: {0} already exists\n", courierConfigResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Courier Configuration.
                ItemResponse<CourierConfig> courierConfigResponse = await this.container.CreateItemAsync<CourierConfig>(courierConfig, new PartitionKey(courierConfig.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", courierConfigResponse.Resource.Id, courierConfigResponse.RequestCharge);
            }
        }
        // </AddItemsToContainerAsync>
    }
}
