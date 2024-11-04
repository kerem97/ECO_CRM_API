using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.CustomerOperations
{
    public class _UserPhoneInteractionsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserPhoneInteractionsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HttpContext.Session.GetString("Token")}");

            var response = await client.GetAsync("https://sistemeco.online/api/CustomerOperations/user-phone-interactions");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var emailInteractions = JsonConvert.DeserializeObject<List<DisplayUserPhoneInteractionCountResponse>>(jsonData);

                return View(emailInteractions);
            }
            return View(new List<DisplayUserPhoneInteractionCountResponse>());
        }
    }
}
