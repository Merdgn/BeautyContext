using Microsoft.AspNetCore.Mvc;
using BeautiyCenter.DataAccess.Context;

namespace BeautyCenter.Presentation.Controllers
{
    public class AboutController : Controller
    {
        private readonly BeautyCenterContext _context;

        public AboutController(BeautyCenterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aboutInfo = _context.Abouts.FirstOrDefault();
            return View(aboutInfo);
        }
    }
}
