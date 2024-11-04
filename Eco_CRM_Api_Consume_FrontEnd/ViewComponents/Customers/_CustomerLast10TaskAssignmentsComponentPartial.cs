using DtoLayer.TaskAssignment.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerLast10TaskAssignmentsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CustomerLast10TaskAssignmentsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new List<GetLast10TaskAssignmentsByCustomerIdResponse>());
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://sistemeco.online/api/TaskAssignments/customer/{customerId}/last-10-task-assignments");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var last10Tasks = JsonConvert.DeserializeObject<List<GetLast10TaskAssignmentsByCustomerIdResponse>>(jsonData);

                return View(last10Tasks);
            }

            return View(new List<GetLast10TaskAssignmentsByCustomerIdResponse>());
        }
    }
}
