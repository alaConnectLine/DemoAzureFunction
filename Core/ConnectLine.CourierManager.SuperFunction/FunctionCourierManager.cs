using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ConnectLine.CourierManager.SuperFunction.Handlers;
using Microsoft.Extensions.Configuration;
using CommonArea.Models.Requests;
using CommonArea.Models.Responses;

namespace ConnectLine.CourierManager.SuperFunction
{
    public class FunctionCourierManager
    {
        private readonly IConfiguration _configuration;
        private readonly CourierHandler _handler;
        public FunctionCourierManager(CourierHandler handler, IConfiguration configuration)
        {
            _configuration = configuration;
            _handler = handler;
        }


        [FunctionName("CreateVoucher")]
        public async Task<IActionResult> RunCreate(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            CreateVoucherRequest createVoucherRequest,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger CreateVoucher function processed a request.");

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            CreateVoucherResponse response = await _handler.ProccedCreateVoucherRequest(createVoucherRequest);

            return new OkObjectResult(response);
        }

        [FunctionName("CancelVoucher")]
        public async Task<IActionResult> RunCancel(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            CancelVoucherRequest cancelVoucherRequest,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger CancelVoucher function processed a request.");

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            CancelVoucherResponse response = await _handler.ProccedCancelVoucherRequest(cancelVoucherRequest);
            
            return new OkObjectResult("OK");
        }

        [FunctionName("GetVoucher")]
        public async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger GetVoucher function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            CreateVoucherResponse response = _handler.ProccedGetVoucherRequest(data);
            
            return new OkObjectResult("OK");
        }
    }
}
