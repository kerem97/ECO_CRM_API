using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Responses
{
    public class DisplayCustomerOperationResponse
    {
        public int Id { get; set; }
        public DateTime OperationDate { get; set; }
        public string CreatedByUser { get; set; } 
        public string CustomerName { get; set; }
        public string Method { get; set; }
        public string IsNew { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string Reason { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string UpdatedStatusDescription { get; set; }
        public string CancelReason { get; set; }
        public string MeetingFeedback { get; set; }
        public string? OfferStatus { get; set; }
    }
}
