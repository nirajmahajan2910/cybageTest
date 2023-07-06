using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text;
using CybageTest.Models;

namespace CybageTest.Controllers
{
    public class DefaultController : Controller
    {
        protected async Task<ResponseModel> MakeHttpRequestAsync<T>(string url, HttpMethod httpMethod, object requestBody, bool handleErrors = false)
        {
            ResponseModel result = new ResponseModel();
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url)
                };
                if (requestBody != null)
                {
                    var json = JsonSerializer.Serialize(requestBody);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
                var response = await client.SendAsync(request);
                if (handleErrors && !response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                    result.StatusCode = response.StatusCode.ToString();
                    return result;
                }
                var responseJson = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseJson))
                {
                    var responseData = JsonSerializer.Deserialize<T>(responseJson);
                    result.Data = responseData;
                }
                result.IsSuccess = true;
                return result;
            }
        }
    }
}
