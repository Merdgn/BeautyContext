using Microsoft.AspNetCore.Mvc;
using BeautiyCenter.Business.Abstract;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BeautiyCenter.Presentation.Controllers
{
    public class AIController : Controller
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            return View(); // Bu, Views/AI/UploadImage.cshtml'i yükleyecek.
        }

        [HttpPost]
        public async Task<IActionResult> GetSuggestion(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ViewBag.Error = "Lütfen bir dosya yükleyin.";
                return View("UploadImage"); // Eğer hata varsa tekrar formu yükler.
            }

            var result = await _aiService.GetHairStyleSuggestionAsync(image);
            if (result.Success)
            {
                return View("SuggestionResult", result.Data); 
            }

            ViewBag.Error = "Yapay zeka önerileri alınamadı.";
            return View("UploadImage");
        }
    }
}
