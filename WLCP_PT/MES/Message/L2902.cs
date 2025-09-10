using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.MES.Message
{
    public class L2902 : Header
    {
        public L2902(DateTime dateTime)
        {
            DATE_TIME = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string MESSAGE_ID
        {
            get
            {
                return "L2902";
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
                return "YTRRM01";
            }
        }

        public double CG_AREA_LENGTH { get; set; }

        public double W_BFR_FRN_AREA_WEIGHT { get; set; }

        public double FRN_AREA_DISCHARGE_TEMP { get; set; }

        public double FRN_AREA_TIME_IN_FURNACE { get; set; }

        public double PY1_TEMP_ACT { get; set; }

        public double PY1_VEL_ACT { get; set; }

        public double DESCALER_PRESSURE { get; set; }

        public double PR1_TQ_ACT { get; set; }

        public double PR1_VEL_ACT { get; set; }

        public double PR1_PINCH_TQ { get; set; }

        public double PR1_MODE { get; set; }

        public double STD1_TQ_ACT { get; set; }

        public double STD1_VEL_ACT { get; set; }

        public double STD1_MVC_PERC { get; set; }

        public double STD2_TQ_ACT { get; set; }

        public double STD2_VEL_ACT { get; set; }

        public double STD2_MVC_PERC { get; set; }

        public double STD3_TQ_ACT { get; set; }

        public double STD3_VEL_ACT { get; set; }

        public double STD3_MVC_PERC { get; set; }

        public double STD4_TQ_ACT { get; set; }

        public double STD4_VEL_ACT { get; set; }

        public double STD4_MVC_PERC { get; set; }

        public double STD5_TQ_ACT { get; set; }

        public double STD5_VEL_ACT { get; set; }

        public double STD5_MVC_PERC { get; set; }

        public double STD6_TQ_ACT { get; set; }

        public double STD6_VEL_ACT { get; set; }

        public double STD6_MVC_PERC { get; set; }

        public double SH1_VEL_CUT { get; set; }

        public double SH1_HCUT_SP { get; set; }

        public double SH1_TCUT_SP { get; set; }

        public double PY2_TEMP_ACT { get; set; }

        public double PY2_VEL_ACT { get; set; }

        public double I_FRN_EXIT_TMP { get; set; }

        public double PY3_TEMP_ACT { get; set; }

        public double PY3_VEL_ACT { get; set; }

        public double PR2_TQ_ACT { get; set; }

        public double PR2_VEL_ACT { get; set; }

        public double PR2_PINCH_TQ { get; set; }

        public double PR2_MODE { get; set; }

        public double STD7_TQ_ACT { get; set; }

        public double STD7_VEL_ACT { get; set; }

        public double STD7_MVC_PERC { get; set; }

        public double STD8_TQ_ACT { get; set; }

        public double STD8_VEL_ACT { get; set; }

        public double STD8_MVC_PERC { get; set; }

        public double STD9_TQ_ACT { get; set; }

        public double STD9_VEL_ACT { get; set; }

        public double STD9_MVC_PERC { get; set; }

        public double STD10_TQ_ACT { get; set; }

        public double STD10_VEL_ACT { get; set; }

        public double STD10_MVC_PERC { get; set; }

        public double SH2_VEL_CUT { get; set; }

        public double SH2_HCUT_SP { get; set; }

        public double SH2_TCUT_SP { get; set; }

        public double STD11_TQ_ACT { get; set; }

        public double STD11_VEL_ACT { get; set; }

        public double STD11_MVC_PERC { get; set; }

        public double STD12_TQ_ACT { get; set; }

        public double STD12_VEL_ACT { get; set; }

        public double STD12_MVC_PERC { get; set; }

        public double STD13_TQ_ACT { get; set; }

        public double STD13_VEL_ACT { get; set; }

        public double STD13_MVC_PERC { get; set; }

        public double STD14_TQ_ACT { get; set; }

        public double STD14_VEL_ACT { get; set; }

        public double STD14_MVC_PERC { get; set; }

        public double SH3_VEL_CUT { get; set; }

        public double SH3_HCUT_SP { get; set; }

        public double SH3_TCUT_SP { get; set; }

        public double UL1_LH_ACT { get; set; }

        public double STD15_TQ_ACT { get; set; }

        public double STD15_VEL_ACT { get; set; }

        public double STD15_MVC_PERC { get; set; }

        public double UL2_LH_ACT { get; set; }

        public double STD16_TQ_ACT { get; set; }

        public double STD16_VEL_ACT { get; set; }

        public double STD16_MVC_PERC { get; set; }

        public double UL3_LH_ACT { get; set; }

        public double STD17_TQ_ACT { get; set; }

        public double STD17_VEL_ACT { get; set; }

        public double STD17_MVC_PERC { get; set; }

        public double UL4_LH_ACT { get; set; }

        public double STD18_TQ_ACT { get; set; }

        public double STD18_VEL_ACT { get; set; }

        public double STD18_MVC_PERC { get; set; }

        public double UL5_LH_ACT { get; set; }

        public double STD19_TQ_ACT { get; set; }

        public double STD19_VEL_ACT { get; set; }

        public double STD19_MVC_PERC { get; set; }

        public double UL6_LH_ACT { get; set; }

        public double STD20_TQ_ACT { get; set; }

        public double STD20_VEL_ACT { get; set; }

        public double STD20_MVC_PERC { get; set; }

        public double PR3_TQ_ACT { get; set; }

        public double PR3_VEL_ACT { get; set; }

        public double PR3_PINCH_TQ { get; set; }

        public double PR3_MODE { get; set; }

        public double SH4_VEL_CUT { get; set; }

        public double SH4_HCUT_SP { get; set; }

        public double SH4_TCUT_SP { get; set; }

        public double SH_SCR_VEL_CUT { get; set; }

        public double SH_SCR_HCUT_SP { get; set; }

        public double SH_SCR_TCUT_SP { get; set; }

        public double UL7_LH_ACT { get; set; }

        public double PY7_TEMP_ACT { get; set; }

        public double PY7_VEL_ACT { get; set; }

        public double WB1_FLOW { get; set; }

        public double WB1_PRESS { get; set; }

        public double WB1_FLOW_OPN { get; set; }

        public double WB1_PRESS_OPN { get; set; }

        public double WB1_NR_DIV_OPN { get; set; }

        public double WB1_MODE { get; set; }

        public double SH5_VEL_CUT { get; set; }

        public double SH5_HCUT_SP { get; set; }

        public double SH5_TCUT_SP { get; set; }

        public double SL_LH_ACT { get; set; }

        public double PY8_TEMP_ACT { get; set; }

        public double PY8_VEL_ACT { get; set; }

        public double NTM_NRD { get; set; }

        public double NTM_TQ_ACT { get; set; }

        public double NTM_VEL_ACT { get; set; }

        public double LGAUGE1_SPEED_ACT { get; set; }

        public double PY9_TEMP_ACT { get; set; }

        public double PY9_VEL_ACT { get; set; }

        public double WB2_FLOW { get; set; }

        public double WB2_PRESS { get; set; }

        public double WB2_FLOW_OPN { get; set; }

        public double WB2_PRESS_OPN { get; set; }

        public double WB2_NR_DIV_OPN { get; set; }

        public double WB2_MODE { get; set; }

        public double GAUGE1_AREA { get; set; }

        public double GAUGE1_OVALITY { get; set; }

        public double GAUGE1_DIAMETER { get; set; }

        public double SH6_VEL_CUT { get; set; }

        public double SH6_HCUT_SP { get; set; }

        public double SH6_TCUT_SP { get; set; }

        public double PY10_TEMP_ACT { get; set; }

        public double PY10_VEL_ACT { get; set; }

        public double LGAUGE2_SPEED_ACT { get; set; }

        public double RSM_TQ_ACT { get; set; }

        public double RSM_GEAR { get; set; }

        public double RSM_NRD { get; set; }

        public double RSM_VEL_ACT { get; set; }

        public double GAUGE2_AREA { get; set; }

        public double GAUGE2_OVALITY { get; set; }

        public double GAUGE2_DIAMETER { get; set; }

        public double WB3_1_FLOW { get; set; }

        public double WB3_1_PRESS { get; set; }

        public double WB3_1_FLOW_OPN { get; set; }

        public double WB3_1_PRESS_OPN { get; set; }

        public double WB3_1_NR_DIV_OPN { get; set; }

        public double WB3_1_MODE { get; set; }

        public double WB3_2_FLOW { get; set; }

        public double WB3_2_PRESS { get; set; }

        public double WB3_2_FLOW_OPN { get; set; }

        public double WB3_2_PRESS_OPN { get; set; }

        public double WB3_2_NR_DIV_OPN { get; set; }

        public double WB3_2_MODE { get; set; }

        public double IPR_TQ_ACT { get; set; }

        public double IPR_N_ACT { get; set; }

        public double IPR_VEL_ACT { get; set; }

        public double IPR_PINCH_TQ { get; set; }

        public double IPR_MODE { get; set; }

        public double LH_TQ_ACT { get; set; }

        public double LH_N_ACT { get; set; }

        public double LH_VEL_ACT { get; set; }

        public double LGAUGE3_SPEED_ACT { get; set; }

        public double PY11_TEMP_ACT { get; set; }

        public double PY11_VEL_ACT { get; set; }

        public double DST_MASTER_SPEED { get; set; }

        public double PY4_TEMP_ACT { get; set; }

        public double PY4_VEL_ACT { get; set; }

        public double WB4_FLOW { get; set; }

        public double WB4_PRESS { get; set; }

        public double WB4_FLOW_OPN { get; set; }

        public double WB4_PRESS_OPN { get; set; }

        public double WB4_NR_DIV_OPN { get; set; }

        public double WB4_MODE { get; set; }

        public double PR4_TQ_ACT { get; set; }

        public double PR4_VEL_ACT { get; set; }

        public double PR4_PINCH_TQ { get; set; }

        public double PR4_MODE { get; set; }

        public double SH7_VEL_CUT { get; set; }

        public double SH7_HCUT_SP { get; set; }

        public double SH7_TCUT_SP { get; set; }

        public double SH_SCR2_VEL_CUT { get; set; }

        public double SH_SCR2_HCUT_SP { get; set; }

        public double SH_SCR2_TCUT_SP { get; set; }

        public double PY5_TEMP_ACT { get; set; }

        public double PY5_VEL_ACT { get; set; }

        public double PY6_TEMP_ACT { get; set; }

        public double PY6_VEL_ACT { get; set; }

        public double WS_1_BAR_COUNT { get; set; }

        public double WS_1_WEIGHT { get; set; }

        public double BAR_OUTLET_AREA_13_BAR_COUNT { get; set; }

        public double WS_2_UNLOAD { get; set; }

        public double ELECT_USAGE { get; set; }

        public double WATER_USAGE { get; set; }
    }
}