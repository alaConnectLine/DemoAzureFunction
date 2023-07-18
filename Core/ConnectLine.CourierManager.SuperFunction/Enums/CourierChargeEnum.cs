using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectLine.CourierManager.SuperFunction.Enums
{
    /// <summary>
    /// Enumeration which describes the Courier Charge.
    /// </summary>
    public enum CourierChargeEnum
    {
        /// <summary>Charge sender.</summary>
        Sender = 0,

        /// <summary>Charge recipient.</summary>
        Recipient = 1,

        /// <summary>Charge other.</summary>
        Other = 2,
    }
}
