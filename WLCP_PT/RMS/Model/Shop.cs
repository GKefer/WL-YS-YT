using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCP_PT.RMS.Model
{
    public class Shop
    {
        public Shop(string SHOP_CODE)
        {
            this.SHOP_CODE = SHOP_CODE;
            EQUIP_LIST = new List<Equip>();
        }

        public string SHOP_CODE { get; set; }

        public List<Equip> EQUIP_LIST { get; set; }
    }
}