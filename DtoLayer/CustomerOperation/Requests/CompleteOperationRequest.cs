using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Requests
{
    public class CompleteOperationRequest
    {
        public int OperationId { get; set; }
        public string Status { get; set; }
        public DateTime ActualDate { get; set; }
    }
}
