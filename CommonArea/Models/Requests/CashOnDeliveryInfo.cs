using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonArea.Models.Requests
{
    public class CashOnDeliveryInfo
    {
        private bool _IsCashOnDelivery;
        private bool _IsCheck;
        private string _Currency;
        private decimal _Price;

        public CashOnDeliveryInfo()
            : this(0)
        {
        }

        public CashOnDeliveryInfo(decimal price)
            : this(price, "EUR")
        {
        }

        public CashOnDeliveryInfo(decimal price, string currency)
            : this(price, currency, false)
        {
        }

        public CashOnDeliveryInfo(decimal price, string currency, bool isCheck)
        {
            _IsCashOnDelivery = price > 0;
            _IsCheck = isCheck;
            _Price = price;
            _Currency = currency;
        }

        /// <summary>
        ///  IsCashOnDelivery means Antikatavoli 
        /// </summary>
        public bool IsCashOnDelivery
        {
            get { return _IsCashOnDelivery; }
            set { _IsCashOnDelivery = value; }
        }

        /// <summary>
        ///  IsCheck means if it is cash or check
        /// </summary>
        public bool IsCheck
        {
            get { return _IsCheck; }
            set { _IsCheck = value; }
        }

        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
    }
}
