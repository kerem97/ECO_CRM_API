using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Requests
{
    public class AddCustomerOperationRequest
    {
        public int CustomerId { get; set; }
        public string Method { get; set; }
        public bool IsNew { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string Reason { get; set; }
        public string Address { get; set; }
    }
}
