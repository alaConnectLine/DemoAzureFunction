using CommonArea.Enums;
using CommonArea.Handlers;
using CommonArea.Models.Requests;
using CommonArea.Models.Responses;
using ConnectLine.CourierManager.SuperFunction.CosmosDbProvider;
using ConnectLine.CourierManager.SuperFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConnectLine.CourierManager.SuperFunction.Handlers
{
    public class CourierHandler
    {
        public async Task<CreateVoucherResponse> ProccedCreateVoucherRequest(CreateVoucherRequest createVoucherRequest)
        {
            try
            {
                Uri createdAzureFunUri = null;
                Uri createdCourierUri = null;
                string courierUsername = null;
                string courierPassword = null;

                
                //var options = new JsonSerializerOptions();
                //options.Converters.Add(new JsonStringEnumConverter());
                //CreateVoucherRequest createVoucherRequest = JsonSerializer.Deserialize<CreateVoucherRequest>(jsonData, options);
                //ToDo : Validate createVoucherRequest

                string uniqueId = createVoucherRequest.SelectedCourier.ToString() + "." + createVoucherRequest.CompanyId.ToString() + "." + createVoucherRequest.CompanyId.ToString();
                //List<CourierConfig> courierConfigsList = await GetConfiguration(uniqueId);
                List<CourierConfig> courierConfigsList = await GetConfiguration("1");

                //ToDo : Validate courierConfigs
                if (courierConfigsList == null ||courierConfigsList.Count == 0) new Exception("Could not find courier configuration.");
                CourierConfig courierConfig = courierConfigsList.FirstOrDefault();

                AzureFunConfig azureFunConfig = courierConfig.CloudConfig.AzureFunConfig.Where(c => c.BasicOperation == BasicOperation.Create).FirstOrDefault();
                if (azureFunConfig == null) new Exception("Could not find courier cloud configuration.");


                if (azureFunConfig.Uri == null) new Exception("Could not found Azure Function Uri.");
                if (!Uri.TryCreate(azureFunConfig.Uri.ToString(), UriKind.Absolute, out createdAzureFunUri)) new Exception("Azure Function Uri it is not in the proper format.");

                if (createVoucherRequest.IsDemoEndPoint)
                {
                    if (courierConfig.Courier.TestEnvironmentConfig.Uri == null) new Exception("Could not found Courier Uri.");
                    if (!Uri.TryCreate(courierConfig.Courier.TestEnvironmentConfig.Uri.ToString(), UriKind.Absolute, out createdCourierUri)) new Exception("Courier Uri it is not in the proper format.");

                    if (string.IsNullOrEmpty(courierConfig.Courier.TestEnvironmentConfig.Username)) new Exception("Could not found Courier Credentials Username.");
                    if (string.IsNullOrEmpty(courierConfig.Courier.TestEnvironmentConfig.Password)) new Exception("Could not found Courier Credentials Password.");

                    courierUsername = courierConfig.Courier.TestEnvironmentConfig.Username;
                    courierPassword = courierConfig.Courier.TestEnvironmentConfig.Password;
                }
                else
                {
                    if (courierConfig.Courier.LiveEnvironmentConfig.Uri == null) new Exception("Could not found Courier Uri.");
                    if (!Uri.TryCreate(courierConfig.Courier.LiveEnvironmentConfig.Uri.ToString(), UriKind.Absolute, out createdCourierUri)) new Exception("Courier Uri it is not in the proper format.");

                    if (string.IsNullOrEmpty(courierConfig.Courier.LiveEnvironmentConfig.Username)) new Exception("Could not found Courier Credentials Username.");
                    if (string.IsNullOrEmpty(courierConfig.Courier.LiveEnvironmentConfig.Password)) new Exception("Could not found Courier Credentials Password.");

                    courierUsername = courierConfig.Courier.LiveEnvironmentConfig.Username;
                    courierPassword = courierConfig.Courier.LiveEnvironmentConfig.Password;
                }

                switch (createVoucherRequest.SelectedCourier)
                {
                    case CourierType.Acs:
                        createVoucherRequest.CourierCredentials = new Dictionary<string, object>();
                        createVoucherRequest.CourierCredentials.Add("Uri", createdCourierUri);
                        createVoucherRequest.CourierCredentials.Add("Username", courierUsername);
                        createVoucherRequest.CourierCredentials.Add("Password", courierPassword);
                        createVoucherRequest.CourierCredentials.Add("AppKey", "2295e77a427244b79ccc6c8032bd9ddb");

                        break;
                    case CourierType.Speedex:
                        break;
                    default:
                        return null; //new BadRequestObjectResult("Invalid Selected Courier");
                }
                
                var response = await WebServicesHandler.CallWebService(createdAzureFunUri, JsonSerializer.Serialize(createVoucherRequest), WebServiceMethod.POST);
                CreateVoucherResponse createVoucherResponse = JsonSerializer.Deserialize<CreateVoucherResponse>(response);
                return new CreateVoucherResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CreateVoucherResponse();
            }
        }

        public async Task<CancelVoucherResponse> ProccedCancelVoucherRequest(CancelVoucherRequest cancelVoucherRequest)
        {
            try
            {
                Uri createdAzureFunUri = null;
                Uri createdCourierUri = null;
                string courierUsername = null;
                string courierPassword = null;

                //var options = new JsonSerializerOptions();
                //options.Converters.Add(new JsonStringEnumConverter());
                //CancelVoucherRequest cancelVoucherRequest = JsonSerializer.Deserialize<CancelVoucherRequest>(jsonData, options);
                //ToDo : Validate cancelVoucherRequest

                string uniqueId = cancelVoucherRequest.SelectedCourier.ToString() + "." + cancelVoucherRequest.CompanyId.ToString() + "." + cancelVoucherRequest.CompanyId.ToString();
                //List<CourierConfig> courierConfigsList = await GetConfiguration(uniqueId);
                List<CourierConfig> courierConfigsList = await GetConfiguration("1");

                //ToDo : Validate courierConfigs
                if (courierConfigsList.Count == 0) new Exception("Could not find courier configuration.");
                CourierConfig courierConfig = courierConfigsList.FirstOrDefault();

                AzureFunConfig azureFunConfig = courierConfig.CloudConfig.AzureFunConfig.Where(c => c.BasicOperation == BasicOperation.Delete).FirstOrDefault();
                if (azureFunConfig == null) new Exception("Could not find courier cloud configuration.");

                if (azureFunConfig.Uri == null) new Exception("Could not found Azure Function Uri.");
                if (!Uri.TryCreate(azureFunConfig.Uri.ToString(), UriKind.Absolute, out createdAzureFunUri)) new Exception("Azure Function Uri it is not in the proper format.");

                if (cancelVoucherRequest.IsDemoEndPoint)
                {
                    if (courierConfig.Courier.TestEnvironmentConfig.Uri == null) new Exception("Could not found Courier Uri.");
                    if (!Uri.TryCreate(courierConfig.Courier.TestEnvironmentConfig.Uri.ToString(), UriKind.Absolute, out createdCourierUri)) new Exception("Courier Uri it is not in the proper format.");

                    if (string.IsNullOrEmpty(courierConfig.Courier.TestEnvironmentConfig.Username)) new Exception("Could not found Courier Credentials Username.");
                    if (string.IsNullOrEmpty(courierConfig.Courier.TestEnvironmentConfig.Password)) new Exception("Could not found Courier Credentials Password.");

                    courierUsername = courierConfig.Courier.TestEnvironmentConfig.Username;
                    courierPassword = courierConfig.Courier.TestEnvironmentConfig.Password;
                }
                else
                {
                    if (courierConfig.Courier.LiveEnvironmentConfig.Uri == null) new Exception("Could not found Courier Uri.");
                    if (!Uri.TryCreate(courierConfig.Courier.LiveEnvironmentConfig.Uri.ToString(), UriKind.Absolute, out createdCourierUri)) new Exception("Courier Uri it is not in the proper format.");

                    if (string.IsNullOrEmpty(courierConfig.Courier.LiveEnvironmentConfig.Username)) new Exception("Could not found Courier Credentials Username.");
                    if (string.IsNullOrEmpty(courierConfig.Courier.LiveEnvironmentConfig.Password)) new Exception("Could not found Courier Credentials Password.");

                    courierUsername = courierConfig.Courier.LiveEnvironmentConfig.Username;
                    courierPassword = courierConfig.Courier.LiveEnvironmentConfig.Password;
                }

                switch (cancelVoucherRequest.SelectedCourier)
                {
                    case CourierType.Acs:
                        cancelVoucherRequest.CourierCredentials = new Dictionary<string, object>();
                        cancelVoucherRequest.CourierCredentials.Add("Uri", createdCourierUri);
                        cancelVoucherRequest.CourierCredentials.Add("Username", courierUsername);
                        cancelVoucherRequest.CourierCredentials.Add("Password", courierPassword);
                        cancelVoucherRequest.CourierCredentials.Add("AppKey", "2295e77a427244b79ccc6c8032bd9ddb");

                        break;
                    case CourierType.Speedex:
                        break;
                    default:
                        return null; //new BadRequestObjectResult("Invalid Selected Courier");
                }

                var response = await WebServicesHandler.CallWebService(createdAzureFunUri, JsonSerializer.Serialize(cancelVoucherRequest), WebServiceMethod.POST);

                return new CancelVoucherResponse();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CreateVoucherResponse> ProccedGetVoucherRequest(string jsonData)
        {
            try
            {

                return new CreateVoucherResponse();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<List<CourierConfig>> GetConfiguration(string id)
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
    }
}
