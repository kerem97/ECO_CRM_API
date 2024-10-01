﻿using DtoLayer.CustomerOperation.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Eco_CRM_Api_Consume_FrontEnd.ViewComponents.CustomerOperations
{
    public class _FaceToFaceInteractionsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FaceToFaceInteractionsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return View(new List<DisplayFaceToFaceInteractionCountResponse>());
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:44309/api/CustomerOperations/top-face-to-face-interactions");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<ApiResponseWrapper>(jsonData);

                var emailInteractions = responseObject?.Result ?? new List<DisplayFaceToFaceInteractionCountResponse>();

                return View(emailInteractions);
            }

            return View(new List<DisplayFaceToFaceInteractionCountResponse>());
        }

        public class ApiResponseWrapper
        {
            public List<DisplayFaceToFaceInteractionCountResponse> Result { get; set; }
        }
    }
}
