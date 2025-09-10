using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.RTM.Message
{
    public class L2B01 : Header
    {
        public L2B01(DateTime dateTime, int count)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            TRANSACTION_CODE = dateTime.ToString("yyyyMMddHHmmssfff") + count.ToString().PadLeft(5, '0');
            SHOP_LIST = new List<Shop>();
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2B01";
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
                return "01";
            }
        }

        public string TRANSACTION_CODE { get; }

        public List<Shop> SHOP_LIST { get; set; }
    }
}