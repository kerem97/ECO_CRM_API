using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TaskAssignmentFile
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public int TaskAssignmentId { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public TaskAssignment TaskAssignment { get; set; }
    }

}
