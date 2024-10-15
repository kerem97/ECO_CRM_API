using DtoLayer.TaskAssignment.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.Customers
{
    public class _CustomerTaskAssignmentCountComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CustomerTaskAssignmentCountComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new GetTotalTaskAssignmentCountByCustomerIdResponse { TaskCount = 0 });
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:44309/api/TaskAssignments/customer/{customerId}/task-assignment-count");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var taskCount = JsonConvert.DeserializeObject<GetTotalTaskAssignmentCountByCustomerIdResponse>(jsonData);

                return View(taskCount);
            }

            return View(new GetTotalTaskAssignmentCountByCustomerIdResponse { TaskCount = 0 });
        }
    }
}
