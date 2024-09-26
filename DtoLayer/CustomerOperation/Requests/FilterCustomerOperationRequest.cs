using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Requests
{
    public class FilterCustomerOperationRequest
    {
        public string? CompanyName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string? Method { get; set; }
        public string? PerformedBy { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
    }

}
