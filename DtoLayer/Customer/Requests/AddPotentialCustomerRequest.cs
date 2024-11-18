﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Customer.Requests
{
    public class AddPotentialCustomerRequest
    {
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone1 { get; set; }
        [AllowNull]
        public string? ContactPhone2 { get; set; }
        public string ContactEmail { get; set; }
        public string IsDomestic { get; set; }
        [AllowNull]
        public string? Description { get; set; }
        public decimal? LimitTl { get; set; }
        public string Group { get; set; }
        public string GroupCategoryDetail { get; set; }
        public string? PriorityLevel { get; set; }
        public string Status { get; set; }
        public string? CustomerChannel { get; set; }
        public int PostalCode { get; set; }
        public string? Website { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Sector { get; set; }
        public string? PaymentMethod { get; set; }
    }
}