using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Responses
{
    public class GetByCustomerIdLast10OperationsResponse
    {
        public int Id { get; set; }
        public DateTime? ActualDate { get; set; }
        public string CreatedByUser { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string Reason { get; set; }
        public string MeetingFeedback { get; set; }
        public string? OfferStatus { get; set; }
    }
}
