using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignment.Requests
{
    public class UpdateTaskAssignmentStatusToOfferGivenRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
