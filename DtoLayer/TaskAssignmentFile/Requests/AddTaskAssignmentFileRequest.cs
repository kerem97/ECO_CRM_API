using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignmentFile.Requests
{
    public class AddTaskAssignmentFileRequest
    {
        public IFormFile File { get; set; }
        public int TaskAssignmentId { get; set; }
    }
}
