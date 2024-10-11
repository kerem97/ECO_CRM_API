using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignmentFile.Requests
{
    public class UpdateTaskAssignmentFileRequest
    {
        public string Id { get; set; } 
        public string FileName { get; set; }
    }
}
