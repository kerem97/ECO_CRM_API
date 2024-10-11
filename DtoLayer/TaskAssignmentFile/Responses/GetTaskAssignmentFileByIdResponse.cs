using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignmentFile.Responses
{
    public class GetTaskAssignmentFileByIdResponse
    {
        public string Id { get; set; } 
        public string FileName { get; set; } 
        public DateTime UploadedDate { get; set; } 
        public int TaskAssignmentId { get; set; } 
    }

}
