using CommonArea.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Requests
{
    /// <summary>
    /// Class which contains the charge information for the delivery.
    /// </summary>
    public class CourierChargeInfo
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="CourierChargeInfo" /> class.
        /// </summary>
        public CourierChargeInfo()
            : this(CourierChargeEnum.Sender)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CourierChargeInfo" /> class.
        /// </summary>
        /// <param name="courierCharge">The courier charge.</param>
        public CourierChargeInfo(CourierChargeEnum courierCharge)
            : this(courierCharge, "")
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CourierChargeInfo" /> class.
        /// </summary>
        /// <param name="courierCharge">The courier charge.</param>
        /// <param name="recipientOrOtherCode">The recipient's or other's code who will be charged.</param>
        public CourierChargeInfo(CourierChargeEnum courierCharge, string recipientOrOtherCode)
        {
            CourierCharge = courierCharge;
            RecipientOrOtherCode = recipientOrOtherCode;
        }

        /// <summary>Gets the courier charge.</summary>
        /// <value>The courier charge.</value>
        public CourierChargeEnum CourierCharge { get; set; }

        /// <summary>Gets the recipient's or other's code who will be charged.</summary>
        /// <value>The recipient's or other's code who will be charged.</value>
        public string RecipientOrOtherCode { get; set; }
    }
}
