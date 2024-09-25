using DtoLayer.Customer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Requests
{
    public class AddCustomerOperationWithDisplayRequest
    {
        public AddCustomerOperationRequest OperationRequest { get; set; }
        public List<DisplayCustomerResponse> Customers { get; set; }
    }
}
