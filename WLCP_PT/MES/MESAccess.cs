using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.MES.Message;
using WLCP_PT.PLC.Model;

namespace WLCP_PT.MES
{
    public class MESAccess
    {
        //設備清單
        private Dictionary<string, PLCEquip> plcEquips;

        public MESAccess(Dictionary<string, PLCEquip> plcEquips)
        {
            Init(plcEquips);
        }

        private void Init(Dictionary<string, PLCEquip> plcEquips)
        {
            this.plcEquips = plcEquips;
        }

        //產生PT Data Json
        public L2902 GenerateData_L2902()
        {
            //產生L2902物件
            L2902 l2902 = new L2902(DateTime.Now);
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                foreach (PLCParam plcParam in plcEquip.Value.PARAM_LIST)
                {
                    //依照參數類型決定是否加入
                    if (plcParam.Type == 0 || plcParam.Type == 1)
                    {
                        foreach (PropertyInfo prop in typeof(L2902).GetProperties())
                        {
                            if (plcParam.PARAMETER_NAME == prop.Name)
                                prop.SetValue(l2902, Convert.ToDouble(plcParam.PARAMETER_VALUE));
                            else
                                continue;
                            break;
                        }
                    }
                }
            }

            return l2902;
        }

        public string GenerateData_L2602()
        {
            List<L2602> l2602_List = new List<L2602>();
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                if (plcEquip.Value.EQUIP_STATUS != null && plcEquip.Value.EQUIP_STATUS.Changed_MES)
                {
                    plcEquip.Value.EQUIP_STATUS.Changed_MES = false;
                    L2602 l2602 = new L2602(DateTime.Now);
                    l2602.EQUIP_CODE = plcEquip.Value.EQUIP_CODE;
                    l2602.EQUIP_STATUS = plcEquip.Value.EQUIP_STATUS.PARAMETER_VALUE;
                    l2602_List.Add(l2602);
                }
            }

            if (l2602_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2602_List);
            }

            return null;
        }

        public string GenerateData_L2604()
        {
            List<L2604> l2604_List = new List<L2604>();

            L2604 l2604 = new L2604(DateTime.Now);
            l2604.EQUIP_CODE = "YTRIM01";
            l2604.ALARM_TYPE = "3";
            l2604.ALARM_ID = "0101";
            l2604.ALARM_MESSAGE = "TEST FOR ALARM";
            l2604.ALARM_STATUS = "1";
            l2604_List.Add(l2604);

            if (l2604_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2604_List);
            }

            return null;
        }

        //產生Public Data Json
        public string GenerateData_L2901_01()
        {
            //產生L2901_01物件
            List<L2901_01> l2901_01_List = new List<L2901_01>();

            L2901_01 l2901_01 = new L2901_01(DateTime.Now);
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                foreach (PLCParam plcParam in plcEquip.Value.PARAM_LIST)
                {
                    //依照參數類型決定是否加入
                    if (plcParam.Type == 4)
                    {
                        foreach (PropertyInfo prop in typeof(L2901_01).GetProperties())
                        {
                            if (plcParam.FileName_MES == prop.Name)
                                prop.SetValue(l2901_01, Convert.ToDouble(plcParam.PARAMETER_VALUE));
                            else
                                continue;
                            break;
                        }
                    }
                }
            }
            l2901_01_List.Add(l2901_01);

            if (l2901_01_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2901_01_List);
            }

            return null;
        }

        //產生Public Data Json
        public string GenerateData_L2901_02()
        {
            //產生L2901_01物件
            List<L2901_02> l2901_02_List = new List<L2901_02>();

            L2901_02 l2901_02 = new L2901_02(DateTime.Now);
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                foreach (PLCParam plcParam in plcEquip.Value.PARAM_LIST)
                {
                    //依照參數類型決定是否加入
                    if (plcParam.Type == 4)
                    {
                        foreach (PropertyInfo prop in typeof(L2901_02).GetProperties())
                        {
                            if (plcParam.FileName_MES == prop.Name)
                                prop.SetValue(l2901_02, Convert.ToDouble(plcParam.PARAMETER_VALUE));
                            else
                                continue;
                            break;
                        }
                    }
                }
            }

            //MQTT數據收集頻率較慢，避免重啟後上傳空值
            if (l2901_02.GDFD_40_1_P > 0)
            {
                l2901_02_List.Add(l2901_02);
            }

            if (l2901_02_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2901_02_List);
            }

            return null;
        }
    }
}