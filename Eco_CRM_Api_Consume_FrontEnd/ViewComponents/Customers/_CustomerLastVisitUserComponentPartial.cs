using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerLastVisitUserComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CustomerLastVisitUserComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new GetCustomerLastVisitUserResponse { FullName = "N/A" });
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://sistemeco.online/api/CustomerOperations/last-visit-user/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var lastVisitUser = JsonConvert.DeserializeObject<GetCustomerLastVisitUserResponse>(jsonData);

                return View(lastVisitUser);
            }

            return View(new GetCustomerLastVisitUserResponse { FullName = "N/A" });
        }
    }
}
