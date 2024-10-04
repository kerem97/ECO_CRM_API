using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LayoutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
    }
}
