using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone1 { get; set; }
        public string ContactPhone2 { get; set; }
        public string ContactEmail { get; set; }
        public string IsDomestic { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal LimitTl { get; set; }
        public string Group { get; set; }
        public ICollection<CustomerOperation> CustomerOperations { get; set; }
        public User User { get; set; }

    }
}
