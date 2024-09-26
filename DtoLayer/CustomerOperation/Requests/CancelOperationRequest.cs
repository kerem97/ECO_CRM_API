using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Requests
{
    public class CancelOperationRequest
    {
        public int OperationId { get; set; }
        public string Status { get; set; }
        public string? CancelReason { get; set; }
    }
}
