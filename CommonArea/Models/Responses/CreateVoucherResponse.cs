using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Responses
{
    public class CreateVoucherResponse : BaseCourierResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVoucherResponse"/> class.
        /// </summary>
        public CreateVoucherResponse()
        {
            Vouchers = new List<string>();
            ExtraCourierFieldsText = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVoucherResponse"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exceptionInfo">The exception information.</param>
        /// <param name="courierResponse">The courier response.</param>
        public CreateVoucherResponse(int errorCode, string errorMessage, Exception exceptionInfo, string courierResponse)
            : this(errorCode, errorMessage, exceptionInfo, courierResponse, new List<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVoucherResponse"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exceptionInfo">The exception information.</param>
        /// <param name="courierResponse">The courier response.</param>
        /// <param name="vouchers">The vouchers which have been created.</param>
        public CreateVoucherResponse(int errorCode, string errorMessage, Exception exceptionInfo, string courierResponse, List<string> vouchers)
            : base(errorCode, errorMessage, exceptionInfo, courierResponse)
        {
            if (vouchers == null)
                Vouchers = new List<string>();
            else
                Vouchers = vouchers;
        }

        /// <summary>Gets or sets the vouchers which have been created.</summary>
        /// <value>The vouchers.</value>
        public List<string> Vouchers { get; set; }

        /// <summary>Gets or sets the extra courier fields (text).</summary>
        /// <value>The extra courier fields (text).</value>
        public Dictionary<string, string> ExtraCourierFieldsText { get; set; }

        public override string ToString()
        {
            return string.Format("CreateVoucherResponse - Error: {0}, Message: {1}, Exception: {2}, Response: {3}, Vouchers: {4}",
                ErrorCode,
                ErrorMessage,
                ExceptionInfo == null ? "" : ExceptionInfo.ToString(),
                CourierResponse,
                string.Join(",", Vouchers));

        }
    }
}
