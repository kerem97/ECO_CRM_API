﻿using Microsoft.AspNetCore.Mvc;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerIndexHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}