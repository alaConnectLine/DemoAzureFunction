using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Responses
{
    public class CancelVoucherResponse : BaseCourierResponse
    {
        public CancelVoucherResponse()
        {
            DeleteResults = new List<CancelVoucherResult>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVoucherResponse"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exceptionInfo">The exception information.</param>
        /// <param name="courierResponse">The courier response.</param>
        public CancelVoucherResponse(int errorCode, string errorMessage, Exception exceptionInfo, string courierResponse)
            : base(errorCode, errorMessage, exceptionInfo, courierResponse)
        {
            DeleteResults = new List<CancelVoucherResult>();
        }

        /// <summary>Gets or sets the delete results for each voucher provided.</summary>
        /// <value>The delete results for each voucher provided.</value>
        public List<CancelVoucherResult> DeleteResults { get; set; }

        public override string ToString()
        {
            return string.Format("CancelVoucherResponse - Error: {0}, Message: {1}, Exception: {2}, Response: {3}",
                ErrorCode,
                ErrorMessage,
                ExceptionInfo == null ? "" : ExceptionInfo.ToString(),
                CourierResponse);
        }
    }

    public class CancelVoucherResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelVoucherResult"/> class.
        /// </summary>
        /// <param name="voucher">The voucher.</param>
        public CancelVoucherResult(string voucher)
            : this(voucher, true, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelVoucherResult"/> class.
        /// </summary>
        /// <param name="voucherNumber">The voucher number.</param>
        /// <param name="deleted">if set to <c>true</c> it is deleted.</param>
        /// <param name="errorMessage">The error message.</param>
        public CancelVoucherResult(string voucherNumber, bool deleted, string errorMessage)
        {
            VoucherNumber = voucherNumber;
            Deleted = deleted;
            ErrorMessage = errorMessage;
        }

        /// <summary>Gets or sets the voucher number.</summary>
        /// <value>The voucher number.</value>
        public string VoucherNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="CancelVoucherResult"/> is deleted.</summary>
        /// <value><c>true</c> if it is deleted, otherwise <c>false</c>.</value>
        public bool Deleted { get; set; }

        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
    }

}
