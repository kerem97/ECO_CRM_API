﻿using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class CustomerOperationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerOperationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
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

            var response = await client.GetAsync("https://localhost:44309/api/CustomerOperation/all-operations");
            var allOperations = new List<DisplayCustomerOperationResponse>();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                allOperations = JsonConvert.DeserializeObject<List<DisplayCustomerOperationResponse>>(apiResponse);
            }

            return View(allOperations);
        }
    }
}
