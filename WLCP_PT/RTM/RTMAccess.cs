using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.PLC.Model;
using WLCP_PT.RTM.Message;
using WLCP_PT.RTM.Model;

namespace WLCP_PT.RTM
{
    public class RTMAccess
    {
        //設備清單
        private Dictionary<string, PLCEquip> plcEquips;
        //紀錄事件上報時間與次數
        private DateTime messageTime_L2B01, messageTime_L2901, messageTime_L2902;
        private int count_L2B01, count_L2901, count_L2902;

        public RTMAccess(Dictionary<string, PLCEquip> plcEquips)
        {
            Init(plcEquips);
        }

        private void Init(Dictionary<string, PLCEquip> plcEquips)
        {
            this.plcEquips = plcEquips;
            count_L2B01 = 1;
            count_L2902 = 1;
        }

        //產生L2B01 Json
        public string GenerateData_L2B01()
        {
            //依照SHOP_CODE產生站別清單
            Dictionary<string, Shop> Shop_List = new Dictionary<string, Shop>();
            //將每個設備依序放進從屬的站別下
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                //如站別清單中不存在該SHOP_CODE則新增一個站別
                string SHOP_CODE = plcEquip.Value.SHOP_CODE;
                if (!Shop_List.TryGetValue(SHOP_CODE, out Shop shop))
                {
                    shop = new Shop(SHOP_CODE);
                    Shop_List.Add(SHOP_CODE, shop);
                }

                //新增一個設備放入站別清單中
                Equip equip = new Equip(plcEquip.Value.EQUIP_CODE);
                foreach (PLCParam plcParam in plcEquip.Value.PARAM_LIST)
                {
                    //依照參數類型決定是否加入
                    if (plcParam.Type == 0 || plcParam.Type == 2)
                    {
                        ParamName paramName = new ParamName();
                        paramName.PARAMETER_NAME = plcParam.PARAMETER_NAME;
                        equip.PARAM_LIST.Add(paramName);
                    }
                }

                //有參數的L2902物件才加入清單中
                if (equip.PARAM_LIST.Count > 0 || plcEquip.Value.EQUIP_STATUS != null)
                {
                    shop.EQUIP_LIST.Add(equip);
                } else
                {
                    Console.WriteLine(equip.EQUIP_CODE);
                }
            }

            //產生L2B01清單
            List<L2B01> l2B01_List = new List<L2B01>();

            //產生L2B01物件並將站別清單加入
            DateTimeCompare(ref messageTime_L2B01, ref count_L2B01);
            L2B01 l2B01 = new L2B01(messageTime_L2B01, count_L2B01);
            l2B01.SHOP_LIST = Shop_List.Values.ToList();

            //當該站別下無任何設備代碼時將其移除站別清單
            for (int i = 0; i < l2B01.SHOP_LIST.Count; i++)
            {
                if (l2B01.SHOP_LIST[i].EQUIP_LIST.Count == 0)
                {
                    l2B01.SHOP_LIST.RemoveAt(i--);
                }
            }

            //將L2B01物件加入清單中
            l2B01_List.Add(l2B01);

            return JsonConvert.SerializeObject(l2B01_List);
        }

        //產生L2901 Json
        public string GenerateData_L2901()
        {
            List<L2901> l2901_List = new List<L2901>();
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                if (plcEquip.Value.EQUIP_STATUS != null && plcEquip.Value.EQUIP_STATUS.Changed_RTM)
                {
                    plcEquip.Value.EQUIP_STATUS.Changed_RTM = false;

                    //產生L2901物件
                    DateTimeCompare(ref messageTime_L2901, ref count_L2901);
                    L2901 l2901 = new L2901(messageTime_L2901, count_L2901);
                    l2901.EQUIP_CODE = plcEquip.Value.EQUIP_CODE;
                    l2901.SHOP_CODE = plcEquip.Value.SHOP_CODE;
                    StatusValue statusValue = new StatusValue();
                    statusValue.EQUIP_STATUS = plcEquip.Value.EQUIP_STATUS.PARAMETER_VALUE;
                    statusValue.REMARK = "";
                    l2901.PARAM_LIST.Add(statusValue);
                    l2901_List.Add(l2901);
                }
            }

            if (l2901_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2901_List);
            }

            return null;
        }

        //產生L2902 Json
        public string GenerateData_L2902()
        {
            //產生L2902清單
            List<L2902> l2902_List = new List<L2902>();

            //依據每個設備產生L2902物件
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                //產生L2902物件
                DateTimeCompare(ref messageTime_L2902, ref count_L2902);
                L2902 l2902 = new L2902(messageTime_L2902, count_L2902);

                //將設備EQUIP_CODE與SHOP_CODE加入L2902物件中
                l2902.EQUIP_CODE = plcEquip.Value.EQUIP_CODE;
                l2902.SHOP_CODE = plcEquip.Value.SHOP_CODE;

                //將設備PARAMETER加入L2902物件中
                foreach (PLCParam plcParam in plcEquip.Value.PARAM_LIST)
                {
                    //依照參數類型決定是否加入
                    if ((plcParam.Type == 0 || plcParam.Type == 2) && plcParam.Changed_RTM)
                    {
                        plcParam.Changed_RTM = false;

                        ParamValue paramValue = new ParamValue();
                        paramValue.PARAMETER_NAME = plcParam.PARAMETER_NAME;
                        paramValue.PARAMETER_VALUE = plcParam.PARAMETER_VALUE;
                        l2902.PARAM_LIST.Add(paramValue);
                    }
                }

                //有參數的L2902物件才加入清單中
                if (l2902.PARAM_LIST.Count > 0)
                {
                    l2902_List.Add(l2902);
                }
            }

            if (l2902_List.Count > 0)
            {
                return JsonConvert.SerializeObject(l2902_List);
            }
            else 
            { 
                return null;
            }
        }

        //比較事件上次上報時間與目前時間，如相符則流水號遞延
        private void DateTimeCompare(ref DateTime messageTime, ref int count)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Compare(messageTime, now) == 0)
            {
                count++;
            }
            else
            {
                messageTime = now;
                count = 1;
            }
        }
    }
}