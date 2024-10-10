using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Responses
{
    public class DisplayCustomerOperationsStatusGivenOffersResponse
    {
        public int Id { get; set; }
        public DateTime OperationDate { get; set; }
        public string CreatedByUser { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string? OfferStatus { get; set; }
    }
}
