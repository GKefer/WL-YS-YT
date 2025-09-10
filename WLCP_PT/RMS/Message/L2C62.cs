using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RMS.Model;

namespace WLCP_PT.RMS.Message
{
    public class L2C62
    {
        public L2C62(DateTime dateTime, int count)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            TRANSACTION_CODE = dateTime.ToString("yyyyMMddHHmmssfff") + count.ToString().PadLeft(5, '0');
            SHOP_LIST = new List<Shop>();
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2C62";
            }
        }

        public string MESSAGE_TIME { get; }

        public string STATUS_WORD
        {
            get
            {
                return "0";
            }
        }

        public string MESSAGE_ITEM
        {
            get
            {
                return "02";
            }
        }

        public string TRANSACTION_CODE { get; }

        public List<Shop> SHOP_LIST { get; set; }
    }
}