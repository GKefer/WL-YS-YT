using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCP_PT.MES.Message
{
    public class L2604 : Header
    {
        public L2604(DateTime dateTime)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2604";
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

        public string SOURCE_SYSTEM
        {
            get
            {
                return "L2/EAP";
            }
        }

        public string EQUIP_CODE { get; set; }

        public string ALARM_TYPE { get; set; }

        public string ALARM_ID { get; set; }

        public string ALARM_MESSAGE { get; set; }

        public string ALARM_STATUS { get; set; }

        public string ALARM_VALUE { get; set; }

        public string ID_NO { get; set; }

        public string EXC_TYPE
        {
            get
            {
                return "ERROR";
            }
        }

        public string ACK
        {
            get
            {
                return "N";
            }
        }
    }
}
