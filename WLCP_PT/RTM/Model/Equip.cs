using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCP_PT.RTM.Model
{
    public class Equip
    {
        public Equip(string EQUIP_CODE)
        {
            this.EQUIP_CODE = EQUIP_CODE;
            PARAM_LIST = new List<ParamName>();
        }

        public string EQUIP_CODE { get; set; }

        public List<ParamName> PARAM_LIST { get; set; }
    }
}