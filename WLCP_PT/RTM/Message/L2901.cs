using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.RTM.Message
{
    public class L2901 : Header
    {
        public L2901(DateTime dateTime, int count)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            TRANSACTION_CODE = dateTime.ToString("yyyyMMddHHmmssfff") + count.ToString().PadLeft(5, '0');
            PARAM_LIST = new List<StatusValue>();
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2901";
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

        public string EQUIP_CODE { get; set; }

        public string SHOP_CODE { get; set; }

        public List<StatusValue> PARAM_LIST { get; set; }
    }
}