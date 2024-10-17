using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignment.Responses
{
    public class GetLast10TaskAssignmentsByCustomerIdResponse
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } 
        public int AbasId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
