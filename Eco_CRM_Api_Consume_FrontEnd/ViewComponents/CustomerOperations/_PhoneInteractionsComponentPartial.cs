using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.CustomerOperations
{
    public class _PhoneInteractionsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PhoneInteractionsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new List<DisplayPhoneInteractionCountResponse>());
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://sistemeco.online/api/CustomerOperations/top-phone-interactions");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<ApiResponseWrapper>(jsonData);

                var emailInteractions = responseObject?.Result ?? new List<DisplayPhoneInteractionCountResponse>();

                return View(emailInteractions);
            }

            return View(new List<DisplayPhoneInteractionCountResponse>());
        }

        public class ApiResponseWrapper
        {
            public List<DisplayPhoneInteractionCountResponse> Result { get; set; }
        }
    }
}
