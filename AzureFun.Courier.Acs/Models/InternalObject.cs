using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFun.Courier.Acs.Models
{
    public class CreateVoucherRequestAcs
    {
        public string ACSAlias { get { return "ACS_Create_Voucher"; } }

        public ACSInputParameters ACSInputParameters { get; set; }
    }

    public class AddressValidationRequestAcs
    {
        public string ACSAlias { get { return "ACS_Address_Validation"; } }

        public ACSInputParameters ACSInputParameters { get; set; }
    }

    public class CreateVoucherRequestAcsMulti
    {
        public string ACSAlias { get { return "ACS_Get_Multipart_Vouchers"; } }

        public ACSInputParametersForMulti ACSInputParameters { get; set; }
    }

    public class ACSInputParameters
    {
        public string Company_ID { get; set; }
        public string Company_Password { get; set; }
        public string User_ID { get; set; }
        public string User_Password { get; set; }
        public string Pickup_Date { get; set; }
        public string Sender { get; set; }
        public string Recipient_Name { get; set; }
        public string Recipient_Address { get; set; }
        public string Recipient_Address_Number { get; set; }
        public string Recipient_Zipcode { get; set; }
        public string Recipient_Region { get; set; }
        public string Recipient_Phone { get; set; }
        public string Recipient_Cell_Phone { get; set; }
        public string Recipient_Floor { get; set; }
        public string Recipient_Company_Name { get; set; }
        public string Recipient_Country { get; set; }
        public string Acs_Station_Destination { get; set; }
        public string Acs_Station_Branch_Destination { get; set; }
        public string Billing_Code { get; set; }
        public int? Charge_Type { get; set; }
        public string Cost_Center_Code { get; set; }
        public int? Item_Quantity { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Dimension_X_In_Cm { get; set; }
        public decimal? Dimension_Y_in_Cm { get; set; }
        public decimal? Dimension_Z_in_Cm { get; set; }
        public decimal? Cod_Ammount { get; set; }
        public string Cod_Payment_Way { get; set; }
        public string Acs_Delivery_Products { get; set; }
        public decimal? Insurance_Ammount { get; set; }
        public string Delivery_Notes { get; set; }
        public string Appointment_Until_Time { get; set; }
        public string Recipient_Email { get; set; }
        public string Reference_Key1 { get; set; }
        public string Reference_Key2 { get; set; }
        public string With_Return_Voucher { get; set; }
        public string Language { get; set; }
        public string Voucher_No { get; set; }
        public string AddressID { get; set; }
        public string Address { get; set; }

    }

    public class ACSInputParametersForMulti
    {
        public string Company_ID { get; set; }
        public string Company_Password { get; set; }
        public string User_ID { get; set; }
        public string User_Password { get; set; }
        public string Language { get; set; }
        public string Main_Voucher_No { get; set; }

    }

    public class CreateVoucherResponseAcs
    {
        public bool? ACSExecution_HasError { get; set; }

        public string ACSExecutionErrorMessage { get; set; }

        public ACSOutputResponce ACSOutputResponce { get; set; }
    }

    public class ACSOutputResponce
    {
        public ACSValueOutput[] ACSValueOutput { get; set; }

        public ACSTableOutput ACSTableOutput { get; set; }
    }

    public class ACSValueOutput
    {
        public string Voucher_No { get; set; }

        public string Voucher_No_Return { get; set; }

        public string Error_Message { get; set; }

        public string MultiPart_Voucher_No { get; set; }

        public ACSObjectOutput[] ACSObjectOutput { get; set; }

    }

    public class ACSObjectOutput
    {
        public string Resolved_Street { get; set; }
        public int? Resolved_Confidence { get; set; }
        public int? Resolved_GeoRegionType { get; set; }
    }

    public class ACSTableOutput
    {

    }
    public class CreateVoucherResponseAcsForMulti
    {
        public bool? ACSExecution_HasError { get; set; }

        public string ACSExecutionErrorMessage { get; set; }

        public ACSOutputResponceForMulti ACSOutputResponce { get; set; }
    }

    public class ACSOutputResponceForMulti
    {
        public ACSValueOutputForMulti[] ACSValueOutput { get; set; }

        public ACSTableOutputforMulti ACSTableOutput { get; set; }
    }

    public class ACSValueOutputForMulti
    {
        public string Error_Message { get; set; }

    }

    public class ACSTableOutputforMulti
    {
        public Table_Data[] Table_Data { get; set; }
    }

    public class Table_Data
    {
        public string MultiPart_Voucher_No { get; set; }
    }

    public class CancelVoucherRequestAcs
    {
        public string ACSAlias { get { return "ACS_Delete_Voucher"; } }

        public ACSInputParameters ACSInputParameters { get; set; }
    }

    public class CancelVoucherResponseAcs
    {
        public bool? ACSExecution_HasError { get; set; }

        public string ACSExecutionErrorMessage { get; set; }

        public ACSOutputResponce ACSOutputResponce { get; set; }
    }

    public class AddressValidationResponseAcs
    {
        public bool? ACSExecution_HasError { get; set; }

        public string ACSExecutionErrorMessage { get; set; }

        public ACSOutputResponce ACSOutputResponce { get; set; }
    }
}
