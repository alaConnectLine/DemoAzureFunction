using CommonArea.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Requests
{
    public class CancelVoucherRequest
    {
        public CancelVoucherRequest(List<string> voucherNumbers)
        {
            VoucherNumbers = voucherNumbers;
            ExtraInfo = new Dictionary<string, object>();
            CourierCredentials = new Dictionary<string, object>();
        }
        public bool IsDemoEndPoint { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }

        public CourierType SelectedCourier { get; set; }

        /// <summary>Gets or sets or sets Courier Credentials.</summary>
        /// <value> CourierCredentials.</value>
        public Dictionary<string, object> CourierCredentials { get; set; }

        /// <summary>Gets or sets Courier Extra Info.</summary>
        /// <value> CourierCredentials.</value>
        public Dictionary<string, object> ExtraInfo { get; set; }
        /// <summary>Gets or sets the voucher numbers list.</summary>
        /// <value>The voucher numbers list.</value>
        public List<string> VoucherNumbers { get; set; }

        internal bool IsValid(out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (VoucherNumbers == null || VoucherNumbers.Count <= 0)
            {
                validationErrors.Add("Πρέπει να ορίσετε τουλάχιστον ένα Voucher!");
            }

            return validationErrors.Count == 0;
        }
    }
}
