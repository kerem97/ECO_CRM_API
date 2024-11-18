using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperationFile.Responses
{
    public class DisplayCustomerOperationFileResponse
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
