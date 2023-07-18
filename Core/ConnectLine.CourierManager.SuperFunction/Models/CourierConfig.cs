using CommonArea.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConnectLine.CourierManager.SuperFunction.Models
{
    public class CourierConfig
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        public CourierType SelectedCourier { get; set; }
        public Courier Courier { get; set; }
        public CloudConfig CloudConfig { get; set; }
        public CustomerInfo Customer { get; set; }

        //public List<CourierRecord> CourierRecords { get; set; }
    }

    public class CourierRecord
    {
        //public ExecuteOn ExecuteOn { get; set; }
        public CourierType SelectedCourier { get; set; }
        public Courier Courier { get; set; }
        public CloudConfig CloudConfig { get; set; }
        public CustomerInfo Customer { get; set; }
    }

    public class Environment
    {
        //public bool IsDemo { get; set; }
        public ConfigCredentials? TestEnvironmentConfig { get; set; }
        public ConfigCredentials? LiveEnvironmentConfig { get; set; }
    }
    public class CloudConfig
    {
        public List<AzureFunConfig> AzureFunConfig { get; set; }
    }
    public class AzureFunConfig
    {
        public BasicOperation BasicOperation { get; set; }
        public Uri Uri { get; set; }
        public AzureFunCredentials AzureFunCredentials { get; set; }
    }
    public class AzureFunCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class ConfigCredentials
    {
        public Uri Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AppKey { get; set; }
    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerCompanyInfo CustomerCompany { get; set; }

    }

    public class CustomerCompanyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Courier : Environment
    { }
}
