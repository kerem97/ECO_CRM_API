using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");

            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var response = await client.GetAsync($"https://localhost:44309/api/CustomerOperations/user-operations?pageNumber={pageNumber}&pageSize=10");
            var userOperations = new List<DisplayCustomerOperationResponse>();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                userOperations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationResponse>>(apiResponse);
                var totalRecordsHeader = response.Headers.GetValues("X-Total-Count").FirstOrDefault();
                if (int.TryParse(totalRecordsHeader, out int totalRecords))
                {
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / 10); // Sayfa sayısını hesapla
                }
            }
            ViewBag.CurrentPage = pageNumber;
            return View(userOperations);
        }

      
    }
}
