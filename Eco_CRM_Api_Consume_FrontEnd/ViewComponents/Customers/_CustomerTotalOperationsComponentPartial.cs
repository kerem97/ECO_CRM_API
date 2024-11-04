using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerTotalOperationsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CustomerTotalOperationsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(0);  
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://sistemeco.online/api/CustomerOperations/customer/{customerId}/total-operations");

            if (response.IsSuccessStatusCode)
            {
                var totalOperations = await response.Content.ReadAsStringAsync();
                return View(int.Parse(totalOperations));
            }

            return View(0);  
        }
    }
}
