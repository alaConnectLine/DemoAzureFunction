using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Responses
{
    public class BaseCourierResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCourierResponse"/> class.
        /// </summary>
        public BaseCourierResponse()
            : this(0, "", null, "")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCourierResponse"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exceptionInfo">The exception information.</param>
        /// <param name="courierResponse">The courier response.</param>
        public BaseCourierResponse(int errorCode, string errorMessage, Exception exceptionInfo, string courierResponse)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorDetails = new List<string>();
            ExceptionInfo = exceptionInfo;
            CourierResponse = courierResponse;
        }

        /// <summary>Gets or sets the error code.</summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }

        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>Gets or sets the error details.</summary>
        /// <value>The error details.</value>
        public List<string> ErrorDetails { get; set; }

        /// <summary>Gets the error details string separated by new line.</summary>
        /// <value>The error details string.</value>
        public string ErrorDetailsString
        {
            get
            {
                if (ErrorDetails == null || ErrorDetails.Count == 0)
                    return "";

                return string.Join("\r\n", ErrorDetails);
            }
        }

        /// <summary>Gets or sets the exception information.</summary>
        /// <value>The exception information.</value>
        public Exception ExceptionInfo { get; set; }

        /// <summary>Gets or sets the courier response.</summary>
        /// <value>The courier response.</value>
        public string CourierResponse { get; set; }
    }
}
