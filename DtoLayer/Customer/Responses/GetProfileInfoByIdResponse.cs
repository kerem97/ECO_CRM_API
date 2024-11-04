using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Customer.Responses
{
    public class GetProfileInfoByIdResponse
    {
        public string CompanyName { get; set; }
        public string? Website { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string Status { get; set; }
        public string ContactPhone1 { get; set; }
        public string ContactEmail { get; set; }
    }
}
