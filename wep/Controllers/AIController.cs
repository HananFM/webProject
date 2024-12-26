using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace wep.Controllers
{
    [Authorize]

    public class AIController : Controller
    {
        private const string ApiUrl = "https://api.lightxeditor.com/external/api/v1/hairstyle";
        private const string statusApiUrl = "https://api.lightxeditor.com/external/api/v1/order-status";
        private const string ApiKey = "383fb4ea0b0b4660bea9a2c0a20b2b01_10cb6a1ce9d549e088f1520d45899bb5_andoraitools";
        HttpClient client = new HttpClient();
        string urlBase = "https://masri.uk/dania/uploaded";

        public AIController()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        }
        [HttpGet]
        public IActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo, string prompt)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Save the photo to a temporary location
            var filePath = Path.Combine("wwwroot/uploads", photo.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            if (Ftp.UploadFile(filePath))
            {
                var imageUrl = $"{urlBase}/{photo.FileName}";
                var response = await CallAIHairstyleApi(imageUrl, prompt);
                if (response != null)
                {
                    ViewData["ResultUrl"] = response.output;
                }
                ViewData["OriginalUrl"] = imageUrl;
                return View("Result");
            }
            else
            {
                return BadRequest("Failed to upload the file");
            }
        }

        private async Task<OrderStatus> CallAIHairstyleApi(string imageUrl, string prompt)
        {

            var requestData = new
            {
                imageUrl = imageUrl,
                textPrompt = prompt
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ApiUrl, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument document = JsonDocument.Parse(jsonResponse);

                if (document.RootElement.TryGetProperty("body", out JsonElement bodyElement))
                {
                    OrderResponse res = JsonSerializer.Deserialize<OrderResponse>(bodyElement.GetRawText());
                    if (res != null)
                    {
                        if (res.status == "init")
                        {
                            await Task.Delay(3000);
                            int tries = 0;
                            while (tries < 4)
                            {
                                tries++;
                                OrderStatus statusRes = await CheckOrderStatus(res.orderId);
                                if (statusRes != null)
                                {
                                    if (statusRes.status == "active")
                                    {
                                        return statusRes;
                                    }
                                }
                                await Task.Delay(3000);
                            }
                        }
                    }
                }
                else
                {
                    throw new JsonException("Response JSON does not contain a 'body' property");
                }
            }
            return null;

        }

        private async Task<OrderStatus> CheckOrderStatus(string orderId)
        {

            var requestData = new
            {
                orderId = orderId
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(statusApiUrl, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument document = JsonDocument.Parse(jsonResponse);

                if (document.RootElement.TryGetProperty("body", out JsonElement bodyElement))
                {
                    return JsonSerializer.Deserialize<OrderStatus>(bodyElement.GetRawText());
                }
                else
                {
                    throw new JsonException("Response JSON does not contain a 'body' property");
                }
            }
            return null;

        }

        public class OrderResponse
        {
            public string orderId { get; set; }
            public int maxRetriesAllowed { get; set; }
            public int avgResponseTimeInSec { get; set; }
            public string status { get; set; }
        }

        public class OrderStatus
        {
            public string orderId { get; set; }
            public string status { get; set; }
            public string output { get; set; }
        }
    }
}
