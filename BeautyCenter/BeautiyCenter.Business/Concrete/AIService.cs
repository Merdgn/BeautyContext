using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BeautiyCenter.Entity.DTOs;
using BeautiyCenter.Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace BeautiyCenter.Business.Concrete
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;

        public AIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<AIResponse> GetHairStyleSuggestionAsync(IFormFile image)
        {
            var url = "https://node-js-faceshape.onrender.com/api/ai?token=123";

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(image.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                content.Add(fileContent, "image", image.FileName);

                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AIResponse>(jsonResponse);
            }
        }
    }
}
