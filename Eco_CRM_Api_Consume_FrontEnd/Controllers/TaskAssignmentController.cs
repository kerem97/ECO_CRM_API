using DtoLayer.CustomerOperation.Responses;
using DtoLayer.TaskAssignment.Responses;
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
        [HttpGet]
        public async Task<IActionResult> PendingTaskAssignments(int pageNumber = 1, int pageSize = 10)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync($"https://localhost:44309/api/TaskAssignments/pending-tasks?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<GetPendingTaskAssignmentResponse>>(jsonData);
                return View(tasks);  
            }

            TempData["ErrorMessage"] = "Görevler alınırken bir hata oluştu.";
            return View(new List<GetPendingTaskAssignmentResponse>());
        }
        [HttpGet]
        public async Task<IActionResult> GivenOfferTaskAssignments(int pageNumber = 1, int pageSize = 10)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync($"https://localhost:44309/api/TaskAssignments/proposal-given-tasks?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<GetProposalGivenTaskAssignmentResponse>>(jsonData);
                return View(tasks);
            }

            TempData["ErrorMessage"] = "Görevler alınırken bir hata oluştu.";
            return View(new List<GetProposalGivenTaskAssignmentResponse>());
        }

        [HttpGet]
        public async Task<IActionResult> ComplatedTaskAssignments(int pageNumber = 1, int pageSize = 10)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync($"https://localhost:44309/api/TaskAssignments/completed-tasks?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<GetComplatedTaskAssignmentResponse>>(jsonData);
                return View(tasks);
            }

            TempData["ErrorMessage"] = "Görevler alınırken bir hata oluştu.";
            return View(new List<GetComplatedTaskAssignmentResponse>());
        }



    }
}
