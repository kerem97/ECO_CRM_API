using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class TaskAssignmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskAssignmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> OfferedCustomerOperations()
        {
            var fullName = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync($"https://localhost:44309/api/CustomerOperations/status-given-offers?pageNumber=1&pageSize=10");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var operations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationsStatusGivenOffersResponse>>(jsonData);
                return View(operations);
            }

            TempData["ErrorMessage"] = "Veriler alınırken hata oluştu.";
            return View(new List<DisplayCustomerOperationsStatusGivenOffersResponse>());
        }
    }
}
