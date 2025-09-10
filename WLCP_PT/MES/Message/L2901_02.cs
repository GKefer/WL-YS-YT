using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.MES.Message
{
    public class L2901_02 : Header
    {
        public L2901_02(DateTime dateTime)
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
                return "02";
            }
        }

        public string DATE_TIME { get; }

        public string EQUIP_CODE
        {
            get
            {
                return "YTROL02";
            }
        }

        public double GDFD_40_1_UA { get; set; }
        public double GDFD_40_1_UB { get; set; }
        public double GDFD_40_1_UC { get; set; }
        public double GDFD_40_1_IA { get; set; }
        public double GDFD_40_1_IB { get; set; }
        public double GDFD_40_1_IC { get; set; }
        public double GDFD_40_1_P { get; set; }
        public double GDFD_40_1_F { get; set; }
        public double GDFD_40_1_POSW { get; set; }
        public double GDFD_40_2_UA { get; set; }
        public double GDFD_40_2_UB { get; set; }
        public double GDFD_40_2_UC { get; set; }
        public double GDFD_40_2_IA { get; set; }
        public double GDFD_40_2_IB { get; set; }
        public double GDFD_40_2_IC { get; set; }
        public double GDFD_40_2_P { get; set; }
        public double GDFD_40_2_F { get; set; }
        public double GDFD_40_2_POSW { get; set; }
        public double GDFD_40_3_UA { get; set; }
        public double GDFD_40_3_UB { get; set; }
        public double GDFD_40_3_UC { get; set; }
        public double GDFD_40_3_IA { get; set; }
        public double GDFD_40_3_IB { get; set; }
        public double GDFD_40_3_IC { get; set; }
        public double GDFD_40_3_P { get; set; }
        public double GDFD_40_3_F { get; set; }
        public double GDFD_40_3_POSW { get; set; }
        public double GDFD_40_4_UA { get; set; }
        public double GDFD_40_4_UB { get; set; }
        public double GDFD_40_4_UC { get; set; }
        public double GDFD_40_4_IA { get; set; }
        public double GDFD_40_4_IB { get; set; }
        public double GDFD_40_4_IC { get; set; }
        public double GDFD_40_4_P { get; set; }
        public double GDFD_40_4_F { get; set; }
        public double GDFD_40_4_POSW { get; set; }
        public double GDFD_40_5_UA { get; set; }
        public double GDFD_40_5_UB { get; set; }
        public double GDFD_40_5_UC { get; set; }
        public double GDFD_40_5_IA { get; set; }
        public double GDFD_40_5_IB { get; set; }
        public double GDFD_40_5_IC { get; set; }
        public double GDFD_40_5_P { get; set; }
        public double GDFD_40_5_F { get; set; }
        public double GDFD_40_5_POSW { get; set; }
        public double G101_UA { get; set; }
        public double G101_UB { get; set; }
        public double G101_UC { get; set; }
        public double G101_IA { get; set; }
        public double G101_IB { get; set; }
        public double G101_IC { get; set; }
        public double G101_P { get; set; }
        public double G101_F { get; set; }
        public double G101_FA { get; set; }
        public double G101_FB { get; set; }
        public double G101_FC { get; set; }
        public double G101_POSW { get; set; }
        public double G102_UA { get; set; }
        public double G102_UB { get; set; }
        public double G102_UC { get; set; }
        public double G102_IA { get; set; }
        public double G102_IB { get; set; }
        public double G102_IC { get; set; }
        public double G102_P { get; set; }
        public double G102_F { get; set; }
        public double G102_FA { get; set; }
        public double G102_FB { get; set; }
        public double G102_FC { get; set; }
        public double G102_POSW { get; set; }
        public double G103_UA { get; set; }
        public double G103_UB { get; set; }
        public double G103_UC { get; set; }
        public double G103_IA { get; set; }
        public double G103_IB { get; set; }
        public double G103_IC { get; set; }
        public double G103_P { get; set; }
        public double G103_F { get; set; }
        public double G103_FA { get; set; }
        public double G103_FB { get; set; }
        public double G103_FC { get; set; }
        public double G103_POSW { get; set; }
        public double G104_UA { get; set; }
        public double G104_UB { get; set; }
        public double G104_UC { get; set; }
        public double G104_IA { get; set; }
        public double G104_IB { get; set; }
        public double G104_IC { get; set; }
        public double G104_P { get; set; }
        public double G104_F { get; set; }
        public double G104_FA { get; set; }
        public double G104_FB { get; set; }
        public double G104_FC { get; set; }
        public double G104_POSW { get; set; }
        public double G106_UA { get; set; }
        public double G106_UB { get; set; }
        public double G106_UC { get; set; }
        public double G106_IA { get; set; }
        public double G106_IB { get; set; }
        public double G106_IC { get; set; }
        public double G106_P { get; set; }
        public double G106_F { get; set; }
        public double G106_FA { get; set; }
        public double G106_FB { get; set; }
        public double G106_FC { get; set; }
        public double G106_POSW { get; set; }
        public double G107_UA { get; set; }
        public double G107_UB { get; set; }
        public double G107_UC { get; set; }
        public double G107_IA { get; set; }
        public double G107_IB { get; set; }
        public double G107_IC { get; set; }
        public double G107_P { get; set; }
        public double G107_F { get; set; }
        public double G107_FA { get; set; }
        public double G107_FB { get; set; }
        public double G107_FC { get; set; }
        public double G107_POSW { get; set; }
        public double G109_UA { get; set; }
        public double G109_UB { get; set; }
        public double G109_UC { get; set; }
        public double G109_IA { get; set; }
        public double G109_IB { get; set; }
        public double G109_IC { get; set; }
        public double G109_P { get; set; }
        public double G109_F { get; set; }
        public double G109_FA { get; set; }
        public double G109_FB { get; set; }
        public double G109_FC { get; set; }
        public double G109_POSW { get; set; }
        public double G202_UA { get; set; }
        public double G202_UB { get; set; }
        public double G202_UC { get; set; }
        public double G202_IA { get; set; }
        public double G202_IB { get; set; }
        public double G202_IC { get; set; }
        public double G202_P { get; set; }
        public double G202_F { get; set; }
        public double G202_FA { get; set; }
        public double G202_FB { get; set; }
        public double G202_FC { get; set; }
        public double G202_POSW { get; set; }
        public double G204_UA { get; set; }
        public double G204_UB { get; set; }
        public double G204_UC { get; set; }
        public double G204_IA { get; set; }
        public double G204_IB { get; set; }
        public double G204_IC { get; set; }
        public double G204_P { get; set; }
        public double G204_F { get; set; }
        public double G204_FA { get; set; }
        public double G204_FB { get; set; }
        public double G204_FC { get; set; }
        public double G204_POSW { get; set; }
        public double G205_UA { get; set; }
        public double G205_UB { get; set; }
        public double G205_UC { get; set; }
        public double G205_IA { get; set; }
        public double G205_IB { get; set; }
        public double G205_IC { get; set; }
        public double G205_P { get; set; }
        public double G205_F { get; set; }
        public double G205_FA { get; set; }
        public double G205_FB { get; set; }
        public double G205_FC { get; set; }
        public double G205_POSW { get; set; }
        public double G206_UA { get; set; }
        public double G206_UB { get; set; }
        public double G206_UC { get; set; }
        public double G206_IA { get; set; }
        public double G206_IB { get; set; }
        public double G206_IC { get; set; }
        public double G206_P { get; set; }
        public double G206_F { get; set; }
        public double G206_FA { get; set; }
        public double G206_FB { get; set; }
        public double G206_FC { get; set; }
        public double G206_POSW { get; set; }
        public double G207_UA { get; set; }
        public double G207_UB { get; set; }
        public double G207_UC { get; set; }
        public double G207_IA { get; set; }
        public double G207_IB { get; set; }
        public double G207_IC { get; set; }
        public double G207_P { get; set; }
        public double G207_F { get; set; }
        public double G207_FA { get; set; }
        public double G207_FB { get; set; }
        public double G207_FC { get; set; }
        public double G207_POSW { get; set; }
        public double G208_UA { get; set; }
        public double G208_UB { get; set; }
        public double G208_UC { get; set; }
        public double G208_IA { get; set; }
        public double G208_IB { get; set; }
        public double G208_IC { get; set; }
        public double G208_P { get; set; }
        public double G208_F { get; set; }
        public double G208_FA { get; set; }
        public double G208_FB { get; set; }
        public double G208_FC { get; set; }
        public double G208_POSW { get; set; }
        public double G209_UA { get; set; }
        public double G209_UB { get; set; }
        public double G209_UC { get; set; }
        public double G209_IA { get; set; }
        public double G209_IB { get; set; }
        public double G209_IC { get; set; }
        public double G209_P { get; set; }
        public double G209_F { get; set; }
        public double G209_FA { get; set; }
        public double G209_FB { get; set; }
        public double G209_FC { get; set; }
        public double G209_POSW { get; set; }
        public double G210_UA { get; set; }
        public double G210_UB { get; set; }
        public double G210_UC { get; set; }
        public double G210_IA { get; set; }
        public double G210_IB { get; set; }
        public double G210_IC { get; set; }
        public double G210_P { get; set; }
        public double G210_F { get; set; }
        public double G210_FA { get; set; }
        public double G210_FB { get; set; }
        public double G210_FC { get; set; }
        public double G210_POSW { get; set; }
    }
}