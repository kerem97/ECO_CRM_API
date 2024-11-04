using DtoLayer.TaskAssignment.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerDetailApprovedTaskCountComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CustomerDetailApprovedTaskCountComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new TaskAssignmentApprovedCountResponse { ApprovedTaskCount = 0 });
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://sistemeco.online/api/TaskAssignments/customer/{customerId}/approved-task-count");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var approvedOrders = JsonConvert.DeserializeObject<TaskAssignmentApprovedCountResponse>(jsonData); // Nesne dönüyor

                return View(approvedOrders);
            }

            return View(new TaskAssignmentApprovedCountResponse { ApprovedTaskCount = 0 });
        }
    }
}
