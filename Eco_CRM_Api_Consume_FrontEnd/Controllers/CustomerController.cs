using DtoLayer.Customer.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var response = await client.GetAsync($"https://localhost:44309/api/Customers/paged-customers?pageNumber={pageNumber}&pageSize=8");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var customerList = new List<DisplayCustomerResponse>();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                customerList = JsonConvert.DeserializeObject<List<DisplayCustomerResponse>>(responseContent);

                var totalRecordsHeader = response.Headers.GetValues("X-Total-Count").FirstOrDefault();
                if (int.TryParse(totalRecordsHeader, out int totalRecords))
                {
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / 15);
                }
            }

            ViewBag.CurrentPage = pageNumber;
            return View(customerList);
        }
    }
}
