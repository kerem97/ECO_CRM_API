﻿using Microsoft.AspNetCore.Mvc;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Layout
{
    public class _LayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
