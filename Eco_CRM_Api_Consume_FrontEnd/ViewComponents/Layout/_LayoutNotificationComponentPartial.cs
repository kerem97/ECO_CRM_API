﻿using Microsoft.AspNetCore.Mvc;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Layout
{
    public class _LayoutNotificationComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}