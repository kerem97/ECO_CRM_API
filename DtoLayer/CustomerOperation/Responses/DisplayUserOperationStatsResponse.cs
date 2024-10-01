using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Responses
{
    public class DisplayUserOperationStatsResponse
    {
        public int PlannedCount { get; set; }
        public int CompletedCount { get; set; }
    }
}
