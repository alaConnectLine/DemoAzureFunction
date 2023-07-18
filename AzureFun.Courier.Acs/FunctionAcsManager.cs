using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CommonArea.Interfaces;
using CommonArea.Models.Requests;

namespace AzureFun.Courier.Acs
{
    public class FunctionAcsManager
    {
        private readonly IManagerCourier _courierManager;
        //private readonly IFunctionsConfigurationBuilder _configuration;

        public FunctionAcsManager(IManagerCourier courierManager)//, IFunctionsConfigurationBuilder configuration)
        {
            _courierManager = courierManager;
            //_configuration = configuration;
        }


        [FunctionName("CreateVoucherAcs")]
        public async Task<IActionResult> RunCreate(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Acs/CreateVoucherAcs")]
            CreateVoucherRequest createVoucherRequest,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            //ToDo : Do something with req.?
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            var response = await _courierManager.CreateVoucher(createVoucherRequest);
            return new OkObjectResult(response);
        }

        [FunctionName("CancelVoucherAcs")]
        public async Task<IActionResult> RunCancel(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Acs/CancelVoucherAcs")]
            CancelVoucherRequest cancelVoucherRequest,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //ToDo : Do something with req.?
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            var response = await _courierManager.CancelVoucher(cancelVoucherRequest);
            return new OkObjectResult(response);
        }


        [FunctionName("GetVoucherAcs")]
        public async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Acs/GetVoucherAcs")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var response = await _courierManager.CreateVoucher(data);
            return new OkObjectResult(response);
        }
    }
}
