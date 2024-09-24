using Eco_CRM_Api_Consume_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Text;

namespace Eco_CRM_Api_Consume_FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var loginData = JsonConvert.SerializeObject(model);
                var content = new StringContent(loginData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:44309/api/Auths/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);
                    HttpContext.Session.SetString("Token", tokenData.Token.ToString());
                    HttpContext.Session.SetString("FullName", tokenData.FullName.ToString());

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
                }
            }
            return View(model);
        }



    }
}
