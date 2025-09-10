using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLCP_PT.PLC.Model;
using WLCP_PT.RMS.Message;
using WLCP_PT.RMS.Model;

namespace WLCP_PT.RMS
{
    public class RMSAccess
    {
        //設備清單
        private Dictionary<string, PLCEquip> plcEquips;
        //紀錄事件上報時間與次數
        private DateTime messageTime_L2C62;
        private int count_L2C62;

        public RMSAccess(Dictionary<string, PLCEquip> plcEquips)
        {
            Init(plcEquips);
        }

        private void Init(Dictionary<string, PLCEquip> plcEquips)
        {
            this.plcEquips = plcEquips;
            count_L2C62 = 1;
        }

        //產生L2C62 Json
        public string GenerateData_L2C62()
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
                if (equip.PARAM_LIST.Count > 0)
                {
                    shop.EQUIP_LIST.Add(equip);
                }
            }

            //產生L2B01清單
            List<L2C62> l2C62_List = new List<L2C62>();

            //產生L2B01物件並將站別清單加入
            DateTimeCompare(ref messageTime_L2C62, ref count_L2C62);
            L2C62 l2C62 = new L2C62(messageTime_L2C62, count_L2C62);
            l2C62.SHOP_LIST = Shop_List.Values.ToList();

            //當該站別下無任何設備代碼時將其移除站別清單
            for (int i = 0; i < l2C62.SHOP_LIST.Count; i++)
            {
                if (l2C62.SHOP_LIST[i].EQUIP_LIST.Count == 0)
                {
                    l2C62.SHOP_LIST.RemoveAt(i--);
                }
            }

            //將L2B01物件加入清單中
            l2C62_List.Add(l2C62);

            return JsonConvert.SerializeObject(l2C62_List);
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
