using DtoLayer.Customer.Responses;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class CustomerOperationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerOperationController(IHttpClientFactory httpClientFactory)
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
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync($"https://localhost:44309/api/CustomerOperations/user-operations?pageNumber={pageNumber}&pageSize=10");
            var userOperations = new List<DisplayCustomerOperationResponse>();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                userOperations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationResponse>>(apiResponse);

                var totalRecordsHeader = response.Headers.GetValues("X-Total-Count").FirstOrDefault();
                if (int.TryParse(totalRecordsHeader, out int totalRecords))
                {
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / 10);
                }
            }

            ViewBag.CurrentPage = pageNumber;

            return View(userOperations);
        }
        [HttpGet]
        public async Task<IActionResult> AddOperation()
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

            var response = await client.GetAsync("https://localhost:44309/api/Customers");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<DisplayCustomerResponse>>(jsonResponse);

                var viewModel = new AddCustomerOperationWithDisplayRequest
                {
                    OperationRequest = new AddCustomerOperationRequest(),
                    Customers = customers
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Müşteriler alınırken hata oluştu.";
                return View(new AddCustomerOperationWithDisplayRequest());
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOperation(AddCustomerOperationWithDisplayRequest request)
        {
            var token = HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var jsonContent = JsonConvert.SerializeObject(request.OperationRequest);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44309/api/CustomerOperations/add", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Operasyon başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Operasyon ekleme sırasında hata oluştu.";
            return View(request);
        }

        public async Task<IActionResult> Index2()
        {
            var fullName = HttpContext.Session.GetString("FullName");

            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var response = await client.GetAsync("https://localhost:44309/api/CustomerOperations/all-operations?pageNumber=1&pageSize=10");
            var allOperations = new List<DisplayCustomerOperationResponse>();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                allOperations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationResponse>>(apiResponse);
            }

            return View(allOperations);
        }
        public async Task<IActionResult> OperationsByCustomer(int id)
        {
            var fullName = HttpContext.Session.GetString("FullName");

            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ViewBag.Token}");

            var response = await client.GetAsync($"https://localhost:44309/api/CustomerOperations/by-customer/{id}");
            var customerOperations = new List<DisplayCustomerOperationResponse>();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                customerOperations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationResponse>>(apiResponse);
            }

            return View(customerOperations);
        }

        [HttpGet]
        public async Task<IActionResult> AddOperation2()
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

            var response = await client.GetAsync("https://localhost:44309/api/Customers");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<DisplayCustomerResponse>>(jsonResponse);

                var viewModel = new AddCustomerOperationWithDisplayRequest
                {
                    OperationRequest = new AddCustomerOperationRequest(),
                    Customers = customers
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Müşteriler alınırken hata oluştu.";
                return View(new AddCustomerOperationWithDisplayRequest());
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOperation2(AddCustomerOperationWithDisplayRequest request)
        {
            var token = HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var jsonContent = JsonConvert.SerializeObject(request.OperationRequest);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44309/api/CustomerOperations/add", httpContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Operasyon başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Operasyon ekleme sırasında hata oluştu.";
            return View(request);
        }

       

    }
}
