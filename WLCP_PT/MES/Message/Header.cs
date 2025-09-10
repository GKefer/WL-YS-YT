using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCP_PT.MES.Message
{
    public interface Header
    {
        string MESSAGE_ID { get; }

        string MESSAGE_TIME { get; }

        int STATUS_WORD { get; }

        string MESSAGE_ITEM { get; }
    }
}
