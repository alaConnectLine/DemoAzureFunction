using AzureFun.Courier.Acs.Models;
using CommonArea.Enums;
using CommonArea.Handlers;
using CommonArea.Interfaces;
using CommonArea.Models.Requests;
using CommonArea.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFun.Courier.Acs.Handlers
{
    public class AcsManagerHandler : IManagerCourier
    {

        public async Task<CancelVoucherResponse> CancelVoucher(CancelVoucherRequest cancelVoucherRequest)
        {;
            CancelVoucherResponse response = new();
            try
            {
                //ToDo : Validate cancelVoucherRequest
                response = ValidateCancelVoucher(cancelVoucherRequest);
                if (response.ErrorDetails.Count > 0) return response;

                CancelVoucherRequestAcs cancelVoucherRequestAcs = new();
                cancelVoucherRequestAcs.ACSInputParameters = new ACSInputParameters();
                cancelVoucherRequestAcs.ACSInputParameters.Company_ID = cancelVoucherRequest.CourierCredentials.GetValueOrDefault("Username").ToString();
                cancelVoucherRequestAcs.ACSInputParameters.Company_Password = cancelVoucherRequest.CourierCredentials.GetValueOrDefault("Password").ToString();
                cancelVoucherRequestAcs.ACSInputParameters.User_ID = cancelVoucherRequest.CourierCredentials.GetValueOrDefault("Username").ToString();
                cancelVoucherRequestAcs.ACSInputParameters.User_Password = cancelVoucherRequest.CourierCredentials.GetValueOrDefault("Password").ToString();
                cancelVoucherRequestAcs.ACSInputParameters.Language = null;

                //cancelVoucherRequestAcs.ACSInputParameters.Voucher_No = "7400851430";
                cancelVoucherRequestAcs.ACSInputParameters.Voucher_No = cancelVoucherRequest.VoucherNumbers.FirstOrDefault();

                Dictionary<string, object> acsHeaders = new()
                {
                    { "AcsApiKey", cancelVoucherRequest.CourierCredentials.GetValueOrDefault("AppKey").ToString() }
                };


                Uri createdUri = null;
                //string appKey = request.CourierCredentials.AppKey;
                if (!Uri.TryCreate(cancelVoucherRequest.CourierCredentials.GetValueOrDefault("Uri").ToString(), UriKind.Absolute, out createdUri))
                {
                    response.ErrorDetails.Add("Uri it is not in the proper format.");
                    return response;
                }

                string responseAcsStr = await WebServicesHandler.CallWebService(createdUri, JsonConvert.SerializeObject(cancelVoucherRequestAcs), WebServiceMethod.POST, acsHeaders);
                CancelVoucherResponseAcs responseAcs = JsonConvert.DeserializeObject<CancelVoucherResponseAcs>(responseAcsStr);

                if (responseAcsStr == null)
                {
                    for (int i = 0; i < cancelVoucherRequest.VoucherNumbers.Count; i++)
                    {
                        response.DeleteResults.Add(new CancelVoucherResult(cancelVoucherRequest.VoucherNumbers[i], false, $"ACS - Αποτυχία διαγραφής voucher! Δεν υπάρχει response από την ACS"));
                    }
                    return response;
                }

                //if (deleteResponse.ACSExecution_HasError)
                //{
                //    for (int i = 0; i < request.VoucherNumbers.Count; i++)
                //    {
                //        cancelVoucherResponse.DeleteResults.Add(new CancelVoucherResult(request.VoucherNumbers[i], false, $"ACS - Αποτυχία διαγραφής voucher! {deleteResponse.ACSExecutionErrorMessage}"));
                //    }
                //}
                //else if (!string.IsNullOrWhiteSpace(deleteResponse.ACSOutputResponce.ACSValueOutput[0].Error_Message))
                //{
                //    for (int i = 0; i < request.VoucherNumbers.Count; i++)
                //    {
                //        cancelVoucherResponse.DeleteResults.Add(new CancelVoucherResult(request.VoucherNumbers[i], false, $"ACS - Αποτυχία διαγραφής voucher! {deleteResponse.ACSOutputResponce.ACSValueOutput[0].Error_Message}"));
                //    }
                //}
                //else if (deleteResponse.ACSOutputResponce == null)
                //{
                //    for (int i = 0; i < request.VoucherNumbers.Count; i++)
                //    {
                //        cancelVoucherResponse.DeleteResults.Add(new CancelVoucherResult(request.VoucherNumbers[i], false, "ACS - Αποτυχία διαγραφής voucher! Δεν λάβαμε πληροφορία voucher από την ACS!"));
                //    }
                //}
                //else if (string.IsNullOrWhiteSpace(deleteResponse.ACSOutputResponce.ACSValueOutput[0].Error_Message))
                //{
                //    for (int i = 0; i < request.VoucherNumbers.Count; i++)
                //    {
                //        cancelVoucherResponse.DeleteResults.Add(new CancelVoucherResult(request.VoucherNumbers[i]));
                //    }
                //}
                //
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorDetails.Add($"Internal exception with message : {ex.Message}");
                return response;
            }
        }

        public async Task<CreateVoucherResponse> CreateVoucher(CreateVoucherRequest createVoucherRequest)
        {
            CreateVoucherResponse response = new();
            try
            {

                response = ValidateCreateVoucher(createVoucherRequest);
                if (response.ErrorDetails.Count > 0) return response;

                CreateVoucherRequestAcs requestAcs = new();
                requestAcs.ACSInputParameters = new ACSInputParameters();
                requestAcs.ACSInputParameters.Company_ID = createVoucherRequest.CourierCredentials.GetValueOrDefault("Username").ToString();
                requestAcs.ACSInputParameters.User_ID = createVoucherRequest.CourierCredentials.GetValueOrDefault("Username").ToString();
                requestAcs.ACSInputParameters.User_Password = createVoucherRequest.CourierCredentials.GetValueOrDefault("Password").ToString();
                requestAcs.ACSInputParameters.Company_Password = createVoucherRequest.CourierCredentials.GetValueOrDefault("Password").ToString();

                Dictionary<string, object> acsHeaders = new()
                {
                    { "AcsApiKey", createVoucherRequest.CourierCredentials.GetValueOrDefault("AppKey").ToString() }
                };


                requestAcs.ACSInputParameters.Appointment_Until_Time = createVoucherRequest.ExtraInfo.GetValueOrDefault("Appointment_Until_Time")?.ToString();
                requestAcs.ACSInputParameters.With_Return_Voucher = createVoucherRequest.ExtraInfo.GetValueOrDefault("With_Return_Voucher")?.ToString();
                if(decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Insurance_Ammount")?.ToString(), out decimal Insurance_Ammount))
                    requestAcs.ACSInputParameters.Insurance_Ammount = Insurance_Ammount;
                requestAcs.ACSInputParameters.Cost_Center_Code = createVoucherRequest.ExtraInfo.GetValueOrDefault("Cost_Center_Code")?.ToString();
                requestAcs.ACSInputParameters.Language = createVoucherRequest.ExtraInfo.GetValueOrDefault("Language")?.ToString();
                requestAcs.ACSInputParameters.Cod_Payment_Way = createVoucherRequest.ExtraInfo.GetValueOrDefault("Cod_Payment_Way")?.ToString();
                requestAcs.ACSInputParameters.Billing_Code = createVoucherRequest.ExtraInfo.GetValueOrDefault("Billing_Code")?.ToString();
                if (decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Cod_Ammount")?.ToString(), out decimal Cod_Ammount))
                    requestAcs.ACSInputParameters.Cod_Ammount = Cod_Ammount;
                if (int.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Charge_Type")?.ToString(), out int Charge_Type))
                    requestAcs.ACSInputParameters.Charge_Type = Charge_Type;
                if (decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Weight")?.ToString(), out decimal Weight))
                    requestAcs.ACSInputParameters.Weight = Weight;

                requestAcs.ACSInputParameters.Acs_Delivery_Products = createVoucherRequest.ExtraInfo.GetValueOrDefault("Acs_Delivery_Products")?.ToString();
                requestAcs.ACSInputParameters.Sender = createVoucherRequest.ExtraInfo.GetValueOrDefault("Sender")?.ToString();
                requestAcs.ACSInputParameters.Recipient_Address_Number = createVoucherRequest.ExtraInfo.GetValueOrDefault("Recipient_Address_Number")?.ToString();
                requestAcs.ACSInputParameters.Recipient_Floor = createVoucherRequest.ExtraInfo.GetValueOrDefault("Recipient_Floor")?.ToString();
                requestAcs.ACSInputParameters.Acs_Station_Destination = createVoucherRequest.ExtraInfo.GetValueOrDefault("Acs_Station_Destination")?.ToString();
                requestAcs.ACSInputParameters.Acs_Station_Branch_Destination = createVoucherRequest.ExtraInfo.GetValueOrDefault("Acs_Station_Branch_Destination")?.ToString();

                if (decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Dimension_X_In_Cm")?.ToString(), out decimal Dimension_X_In_Cm))
                    requestAcs.ACSInputParameters.Dimension_X_In_Cm = Dimension_X_In_Cm;
                if (decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Dimension_Y_in_Cm")?.ToString(), out decimal Dimension_Y_in_Cm))
                    requestAcs.ACSInputParameters.Dimension_Y_in_Cm = Dimension_Y_in_Cm;
                if (decimal.TryParse(createVoucherRequest.ExtraInfo.GetValueOrDefault("Dimension_Z_in_Cm")?.ToString(), out decimal Dimension_Z_in_Cm))
                    requestAcs.ACSInputParameters.Dimension_Z_in_Cm = Dimension_Z_in_Cm;

                requestAcs.ACSInputParameters.Recipient_Email = createVoucherRequest.ExtraInfo.GetValueOrDefault("Recipient_Email")?.ToString();
                requestAcs.ACSInputParameters.Reference_Key1 = createVoucherRequest.ExtraInfo.GetValueOrDefault("Reference_Key1")?.ToString();
                requestAcs.ACSInputParameters.Reference_Key2 = createVoucherRequest.ExtraInfo.GetValueOrDefault("Reference_Key2")?.ToString();

                requestAcs.ACSInputParameters.Pickup_Date = createVoucherRequest?.ShipmentDate == null ? null : string.Format("{0:yyyy-MM-dd}", createVoucherRequest.ShipmentDate);
                requestAcs.ACSInputParameters.Recipient_Name = createVoucherRequest?.RecipientName;
                requestAcs.ACSInputParameters.Recipient_Address = createVoucherRequest?.RecipientAddress;
                requestAcs.ACSInputParameters.Recipient_Zipcode = createVoucherRequest?.RecipientPostCode;
                requestAcs.ACSInputParameters.Recipient_Region = createVoucherRequest?.RecipientArea;
                requestAcs.ACSInputParameters.Recipient_Phone = createVoucherRequest?.RecipientPhone1;
                requestAcs.ACSInputParameters.Recipient_Cell_Phone = createVoucherRequest?.RecipientPhone2;
                requestAcs.ACSInputParameters.Recipient_Company_Name = createVoucherRequest?.RecipientCompany;
                requestAcs.ACSInputParameters.Recipient_Country = createVoucherRequest?.RecipientCountry;
                requestAcs.ACSInputParameters.Item_Quantity = createVoucherRequest.CourierPackagesList?.Count;
                requestAcs.ACSInputParameters.Delivery_Notes = createVoucherRequest?.CourierRemarks;

                Uri createdUri = null;
                if (!Uri.TryCreate(createVoucherRequest.CourierCredentials.GetValueOrDefault("Uri")?.ToString(), UriKind.Absolute, out createdUri))
                {
                    response.ErrorDetails.Add("Uri it is not in the proper format.");
                    return response;
                }
                //var x = JsonConvert.SerializeObject(requestAcs, Formatting.Indented);

                string responseAcsStr = await WebServicesHandler.CallWebService(createdUri, JsonConvert.SerializeObject(requestAcs), WebServiceMethod.POST, acsHeaders);
                //response.CourierResponse = res;
                //CreateVoucherResponseAcsForMulti responseMultiAcs = new CreateVoucherResponseAcsForMulti();
                //if (!responseAcs.ACSExecution_HasError && responseAcs.ACSOutputResponce.ACSValueOutput[0].Error_Message?.Length == 0)
                //{
                //    string mainVoucher = responseAcs.ACSOutputResponce.ACSValueOutput[0].Voucher_No;
                //    if (request.CourierPackagesList.Count > 1)
                //    {
                //        CreateVoucherRequestAcsMulti requestAcsMulti = new CreateVoucherRequestAcsMulti()
                //        {
                //            ACSInputParameters = new ACSInputParametersForMulti()
                //            {
                //                Company_ID = _RequestContext.CompanyId,
                //                Company_Password = _RequestContext.CompanyPassword,
                //                User_ID = _RequestContext.UserName,
                //                User_Password = _RequestContext.Password,
                //                Main_Voucher_No = mainVoucher,
                //                Language = null
                //            }
                //        };
                //        responseMultiAcs = CreateVoucherInternalMulti(requestAcsMulti);
                //    }
                //}
                CreateVoucherResponseAcs responseAcs = JsonConvert.DeserializeObject<CreateVoucherResponseAcs>(responseAcsStr);
                response = MapCreateVoucherResponse<CreateVoucherResponseAcs>(responseAcs);

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorDetails.Add($"Internal exception with message : {ex.Message}");
                return response;
            }
        }

        public CancelVoucherResponse MapCancelVoucherResponse<T>(dynamic obj)
        {
            throw new NotImplementedException();
        }

        public CreateVoucherResponse MapCreateVoucherResponse<T>(dynamic obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("acsResponse");
            }

            CreateVoucherResponse response = new CreateVoucherResponse();
            if (obj.ACSExecution_HasError)
            {
                response.ErrorCode = 499;
                response.ErrorMessage = "ACS - Αποτυχία δημιουργίας voucher!";
                response.ErrorDetails.Add(obj.ACSExecutionErrorMessage);
            }
            else
            {
                if (obj.ACSOutputResponce == null)
                {
                    response.ErrorCode = 499;
                    response.ErrorMessage = "ACS - Αποτυχία δημιουργίας voucher!";
                    response.ErrorDetails.Add("Δεν λάβαμε πληροφορία voucher από την ACS!");
                }
                else
                {
                    if (obj.ACSOutputResponce.ACSValueOutput[0].Voucher_No == null)
                    {
                        response.ErrorCode = 499;
                        response.ErrorMessage = "ACS - Αποτυχία δημιουργίας voucher!";
                        response.ErrorDetails.Add(obj.ACSOutputResponce.ACSValueOutput[0].Error_Message);
                    }
                    else
                    {
                        response.ErrorCode = 0;
                        response.ErrorMessage = "";
                        foreach (ACSValueOutput output in obj.ACSOutputResponce.ACSValueOutput)
                        {
                            response.Vouchers.Add(output.Voucher_No);
                        }
                    }
                }
            }
            //if (packages > 1)
            //{
            //    if (acsResponseForMulti == null)
            //        throw new ArgumentNullException("acsResponseForMulti");
            //
            //    CreateVoucherResponse responseM = new CreateVoucherResponse();
            //    if (acsResponseForMulti.ACSExecution_HasError)
            //    {
            //        responseM.ErrorCode = 499;
            //        responseM.ErrorMessage = "ACS - Αποτυχία δημιουργίας voucher!";
            //        responseM.ErrorDetails.Add(acsResponseForMulti.ACSExecutionErrorMessage);
            //    }
            //    else
            //    {
            //        if (acsResponseForMulti.ACSOutputResponce == null)
            //        {
            //            responseM.ErrorCode = 499;
            //            responseM.ErrorMessage = "ACS - Αποτυχία δημιουργίας voucher!";
            //            responseM.ErrorDetails.Add("Δεν λάβαμε πληροφορία voucher από την ACS!");
            //        }
            //        else
            //        {
            //            if (acsResponseForMulti.ACSOutputResponce.ACSTableOutput.Table_Data == null)
            //            {
            //                responseM.ErrorCode = 499;
            //                responseM.ErrorMessage = "ACS - Αποτυχία δημιουργίας πολλαπλού voucher!";
            //                responseM.ErrorDetails.Add(acsResponse.ACSOutputResponce.ACSValueOutput[0].Error_Message);
            //            }
            //            else
            //            {
            //                responseM.ErrorCode = 0;
            //                responseM.ErrorMessage = "";
            //                foreach (Table_Data table_Data in acsResponseForMulti.ACSOutputResponce.ACSTableOutput.Table_Data)
            //                {
            //                    responseM.Vouchers.Add(table_Data.MultiPart_Voucher_No);
            //                    acsResponse.ACSOutputResponce.ACSValueOutput[0].MultiPart_Voucher_No = "," + table_Data.MultiPart_Voucher_No;
            //                    response.Vouchers.Add(table_Data.MultiPart_Voucher_No);
            //                }
            //            }
            //        }
            //    }
            //}

            //AssemblyHelper asmHelper = new AssemblyHelper();
            //response.CourierResponse = asmHelper.ExecuteSerializeObject(acsResponse, _CourierType);

            return response;

        }



        private CreateVoucherResponse ValidateCreateVoucher(CreateVoucherRequest createVoucherRequest)
        {
            CreateVoucherResponse response = new();

            if (createVoucherRequest == null)
                response.ErrorDetails.Add("Null request.");

            if (createVoucherRequest?.CourierCredentials == null)
                response.ErrorDetails.Add("Couldn't find courier credentials.");

            if (createVoucherRequest?.ExtraInfo == null)
                response.ErrorDetails.Add("Couldn't find courier extra info.");

            if (!createVoucherRequest.CourierCredentials.TryGetValue("Uri", out object uri) && string.IsNullOrEmpty(uri?.ToString()))
                response.ErrorDetails.Add("Uri is required.");
            if (!createVoucherRequest.CourierCredentials.TryGetValue("Username", out object username) && string.IsNullOrEmpty(username?.ToString()))
                response.ErrorDetails.Add("Username is required.");
            if (!createVoucherRequest.CourierCredentials.TryGetValue("Password", out object password) && string.IsNullOrEmpty(password?.ToString()))
                response.ErrorDetails.Add("Password is required.");
            if (!createVoucherRequest.CourierCredentials.TryGetValue("AppKey", out object appkey) && string.IsNullOrEmpty(appkey?.ToString()))
                response.ErrorDetails.Add("AppKey is required.");
            
            return response;
        }

        private CancelVoucherResponse ValidateCancelVoucher(CancelVoucherRequest cancelVoucherRequest)
        {
            CancelVoucherResponse response = new();

            if (cancelVoucherRequest == null)
                response.ErrorDetails.Add("Null request.");

            if (cancelVoucherRequest?.CourierCredentials == null)
                response.ErrorDetails.Add("Couldn't find courier credentials.");

            if (cancelVoucherRequest?.ExtraInfo == null)
                response.ErrorDetails.Add("Couldn't find courier extra info.");

            if (cancelVoucherRequest.CourierCredentials.TryGetValue("Uri", out object uri) && uri != null)
                response.ErrorDetails.Add("Uri is required.");
            if (cancelVoucherRequest.CourierCredentials.TryGetValue("Username", out object username) && username != null)
                response.ErrorDetails.Add("Username is required.");
            if (cancelVoucherRequest.CourierCredentials.TryGetValue("Password", out object password) && password != null)
                response.ErrorDetails.Add("Password is required.");
            if (cancelVoucherRequest.CourierCredentials.TryGetValue("AppKey", out object appkey) && appkey != null)
                response.ErrorDetails.Add("AppKey is required.");

            return response;
        }
    }
}
