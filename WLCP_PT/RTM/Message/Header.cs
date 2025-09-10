using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCP_PT.RTM.Message
{
    public interface Header
    {
        string MESSAGE_ID { get; }

        string MESSAGE_TIME { get; }

        string STATUS_WORD { get; }

        string MESSAGE_ITEM { get; }

        string TRANSACTION_CODE { get; }
    }
}
