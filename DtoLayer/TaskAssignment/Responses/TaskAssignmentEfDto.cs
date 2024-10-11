using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.TaskAssignment.Responses
{
    public class TaskAssignmentEfDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CreatedByUser { get; set; }
        public int AbasId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Quantity1 { get; set; }
        public int? Quantity2 { get; set; }
        public int? Quantity3 { get; set; }
        public int? Quantity4 { get; set; }
        public int? Quantity5 { get; set; }
        public int? Quantity6 { get; set; }
        public int? Quantity7 { get; set; }
        public int? Quantity8 { get; set; }
        public int? Quantity9 { get; set; }
        public int? Quantity10 { get; set; }
    }

}
