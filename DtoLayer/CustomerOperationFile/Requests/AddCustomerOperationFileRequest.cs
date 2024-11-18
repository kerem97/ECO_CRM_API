using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperationFile.Requests
{
    public class AddCustomerOperationFileRequest
    {
        public int CustomerOperationId { get; set; }
        public IFormFile File { get; set; }
    }
}
