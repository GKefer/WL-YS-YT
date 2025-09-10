using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.MES.Message
{
    public class L2901_01 : Header
    {
        public L2901_01(DateTime dateTime)
        {
            MESSAGE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff");
            DATE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2901";
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

        public string DATE_TIME { get; }

        public string EQUIP_CODE
        {
            get
            {
                return "YTROL01";
            }
        }

        public double FT101 { get; set; }

        public double FT102 { get; set; }

        public double FT104 { get; set; }

        public double FT110 { get; set; }

        public double FT106 { get; set; }

        public double FT107 { get; set; }

        public double FT105 { get; set; }

        public double FT111 { get; set; }

        public double FT109 { get; set; }

        public double FET202 { get; set; }

        public double FT113 { get; set; }

        [JsonProperty("F108.1")]
        public double F108_1 { get; set; }

        [JsonProperty("F108.2")]
        public double F108_2 { get; set; }

        public double ROL_AIR { get; set; }

        public double ROL_AIR_Total { get; set; }

        public double WATER_AIR { get; set; }

        public double WATER_AIR_Total { get; set; }

        public double HPS_AIR { get; set; }

        public double HPS_AIR_Total { get; set; }

        public double BAR_AIR { get; set; }

        public double BAR_AIR_Total { get; set; }

        public double G1103 { get; set; }

        public double G1104 { get; set; }

        public double G1105 { get; set; }

        public double G1106 { get; set; }

        public double PCC13_EP1 { get; set; }

        public double PCC23_EP2 { get; set; }

        public double G1107 { get; set; }

        public double G1108 { get; set; }

        public double G1109 { get; set; }

        public double G1111 { get; set; }

        public double G1112 { get; set; }

        public double G1203 { get; set; }

        public double G1204 { get; set; }

        public double G1205 { get; set; }

        public double G1207 { get; set; }

        public double G1208 { get; set; }

        public double G1210 { get; set; }

        public double G1211 { get; set; }

        public double G1212 { get; set; }
    }
}