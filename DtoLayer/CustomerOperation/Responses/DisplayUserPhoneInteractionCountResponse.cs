﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CustomerOperation.Responses
{
    public class DisplayUserPhoneInteractionCountResponse
    {
        public string CompanyName { get; set; }
        public int InteractionCount { get; set; }
        public int CustomerId { get; set; }
    }
}
