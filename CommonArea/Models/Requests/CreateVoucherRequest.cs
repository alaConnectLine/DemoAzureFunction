using CommonArea.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Requests
{
    public class CreateVoucherRequest
    {

        public CreateVoucherRequest()
        {
            ExtraAttributes = new Dictionary<string, object>();
            ExtraInfo = new Dictionary<string, object>();
            CourierCredentials = new Dictionary<string, object>();
            CashOnDeliveryInfo = new CashOnDeliveryInfo();
        }


        public bool IsDemoEndPoint { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }

        public CourierType SelectedCourier { get; set; }

        /// <summary>Gets or sets the shipment date.</summary>
        /// <value>The shipment date.</value>
        public DateTime ShipmentDate { get; set; }

        /// <summary>Gets or sets the name of the recipient.</summary>
        /// <value>The name of the recipient.</value>
        public string RecipientName { get; set; }

        /// <summary>Gets or sets the name of the recipient.</summary>  kalypdo
        /// <value>The name of the recipient.</value>
        public string RecipientCompany { get; set; }

        /// <summary>Gets or sets the recipient address.</summary>
        /// <value>The recipient address.</value>
        public string RecipientAddress { get; set; }

        /// <summary>Gets or sets the recipient post code.</summary>
        /// <value>The recipient post code.</value>
        public string RecipientPostCode { get; set; }

        /// <summary>Gets or sets the recipient area.</summary>
        /// <value>The recipient area.</value>
        public string RecipientArea { get; set; }

        /// <summary>Gets or sets the recipient city.</summary>
        /// <value>The recipient city.</value>
        public string RecipientCity { get; set; }

        /// <summary>Gets or sets the recipient country.</summary>
        /// <value>The recipient country.</value>
        public string RecipientCountry { get; set; }

        /// <summary>Gets or sets the recipient phone 1.</summary>
        /// <value>The recipient phone 1.</value>
        public string RecipientPhone1 { get; set; }

        /// <summary>Gets or sets the recipient phone 2.</summary>
        /// <value>The recipient phone 2.</value>
        public string RecipientPhone2 { get; set; }

        /// <summary>Gets or sets the remarks.</summary>
        /// <value>The remarks.</value>
        public string RecipientEmail { get; set; }

        /// <summary>Gets or sets the remarks.</summary>
        /// <value>The remarks.</value>
        public string CourierRemarks { get; set; }

        /// <summary>Gets or sets the cash on delivery info (COD).</summary>
        /// <value>The cash on delivery info (COD).</value>
        public CashOnDeliveryInfo CashOnDeliveryInfo { get; set; }

        /// <summary>Gets or sets the delivery charge info.</summary>
        /// <value>The delivery charge info.</value>
        public CourierChargeInfo CourierChargeInfo { get; set; }

        /// <summary>Gets or sets or sets the extra attributes.</summary>
        /// <value>The extra attributes.</value>
        public Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>Gets or sets or sets Courier Credentials.</summary>
        /// <value> CourierCredentials.</value>
        public Dictionary<string, object> CourierCredentials { get; set; }

        /// <summary>Gets or sets Courier Extra Info.</summary>
        /// <value> CourierCredentials.</value>
        public Dictionary<string, object> ExtraInfo { get; set; }

        /// <summary>DHL Shipping</summary>
        /// <value>DHL Shipping Details.</value>
        //public DhlShipping DhlShipping { get; set; }

        /// <summary>DHL Shipping Extra Info</summary>
        /// <value>DHL Shipping Extra Inf.</value>
        //public DhlShippingExtraInfo DhlShippingExtraInfo { get; set; }

        /// <summary>S1 Country Table</summary>
        /// <value>Get data from S1 Country Table</value>
        public Country Country { get; set; }

        /// <summary>S1 SoCurrency Table</summary>
        /// <value>Get data from Country's Currency</value>
        public SoCurrency SoCurrency { get; set; }

        public List<Country> Countries { get; set; }

        public List<CourierPackage> CourierPackagesList { get; set; }
        public CustomsInformation CustomsInformation { get; set; }

    }

    public class Country
    {
        public short CountryId { get; set; }
        public string Shortcut { get; set; }
        public short CountryType { get; set; }
        public short SoCurrency { get; set; }
    }

    public class SoCurrency
    {
        public short SoCurrencyId { get; set; }
        public string Shortcut { get; set; }
    }

    public class CustomsInformation
    {
        public DateTime InvoiceDate { get; set; }
        public List<CustomsInformationItemDetail> CustomsInformationItemDetail = new List<CustomsInformationItemDetail>();
    }

    public class CustomsInformationItemDetail
    {
        public int ItemNumber { get; set; }
        public int Mtrl { get; set; }
        public double Quantity { get; set; }
        public string ItemDescription { get; set; }
        public double? UnitPrice { get; set; }
        public double? NetWeight { get; set; }
        public double? GrossWeight { get; set; }
        public string QuantityUnitOfMeasurement { get; set; }
        public string ManufacturingCountryCode { get; set; }

        public bool ShouldSerializeMtrl()
        {
            return false;
        }
    }

    public class CourierPackage
    {
        public int Id { get; set; }
        public int PackageNo { get; set; }
        public double? Weight { get; set; }
        public double? VolumeWeight { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public int IsDryIce { get; set; }
        public int IsFragile { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Reference3 { get; set; }
    }
}
