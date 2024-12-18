﻿using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> ExistedCustomers(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var response = await client.GetAsync($"https://localhost:44309/api/Customers/paged-existed-customers?pageNumber={pageNumber}&pageSize=8");

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
        public async Task<IActionResult> ExistedCustomersCard(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            var response = await client.GetAsync($"https://localhost:44309/api/Customers/paged-existed-customers?pageNumber={pageNumber}&pageSize=8");

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
        public async Task<IActionResult> PotentialCustomers(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var response = await client.GetAsync($"https://localhost:44309/api/Customers/paged-potential-customers?pageNumber={pageNumber}&pageSize=8");
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
        public async Task<IActionResult> PotentialCustomersCard(int pageNumber = 1)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var response = await client.GetAsync($"https://localhost:44309/api/Customers/paged-potential-customers?pageNumber={pageNumber}&pageSize=8");
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
        [HttpGet]
        public IActionResult NewCustomer()
        {
            var fullName = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewCustomer(AddCustomerRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44309/api/Customers/", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Müşteri eklenirken hata oluştu: {errorContent}";
                return View(request);
            }

            return RedirectToAction("ExistedCustomersCard", "Customer");
        }


        [HttpGet]
        public IActionResult NewPotentialCustomer()
        {
            var fullName = HttpContext.Session.GetString("FullName");
            if (string.IsNullOrEmpty(fullName))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.FullName = fullName;
            ViewBag.Token = HttpContext.Session.GetString("Token");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPotentialCustomer(AddPotentialCustomerRequest request)
        {
            request.Status = "Aday";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44309/api/Customers/add-potential-customers", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorContent);
                TempData["ErrorMessage"] = $"Müşteri eklenirken hata oluştu: {errorContent}";
                return View(request);
            }

            return RedirectToAction("PotentialCustomersCard", "Customer");
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> CustomerDetail(int id)
        {
            var fullName = HttpContext.Session.GetString("FullName");
            ViewBag.FullName = fullName;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");
            ViewBag.Token = HttpContext.Session.GetString("Token");
            ViewBag.CustomerId = id;
            return View();
        }
    }
}

