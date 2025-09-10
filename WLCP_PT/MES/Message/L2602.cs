using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.MES.Message
{
    public class L2602 : Header
    {
        public L2602(DateTime dateTime)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            EQUIP_STATUS_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2602";
            }
        }

        public string MESSAGE_TIME { get; set; }

        public int STATUS_WORD
        {
            get
            {
                return 0;
            }
        }

        public string MESSAGE_ITEM
        {
            get
            {
                return "01";
            }
        }

        public string EQUIP_CODE { get; set; }

        public string EQUIP_STATUS { get; set; }

        public string EQUIP_STATUS_TIME { get; set; }

        //ALARM三欄位不做使用
        public string ALARM_TYPE { get; }

        public string ALARM_ID { get; }

        public string ALARM_MESSAGE { get; }

        public string ACK
        {
            get
            {
                return "N";
            }
        }
    }
}