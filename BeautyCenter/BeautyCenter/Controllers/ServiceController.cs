using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BeautyCenter.Presentation.Controllers
{
    public class ServiceController : Controller
    {
        private readonly BeautyCenterContext _context;

        public ServiceController(BeautyCenterContext context)
        {
            _context = context;
        }

        // MVC View Controller
        public IActionResult Index(string category)
        {
            var imageUrls = new List<string>();

            switch (category)
            {
                case "Makeup":
                    imageUrls = new List<string>
                    {
                        "/images/1makeup.jpg",
                        "/images/2makeup.jpg",
                        "/images/3makeup.jpg",
                        "/images/4makeup.jpg",
                        "/images/5makeup.jpeg",
                        "/images/6makeup.jpeg"
                    };
                    ViewData["Title"] = "Makeup Services";
                    break;

                case "Skincare":
                    imageUrls = new List<string>
                    {
                        "/images/1skincare.jpg",
                        "/images/2skincare.jpg",
                        "/images/3skincare.jpg",
                        "/images/4skincare.jpg",
                        "/images/5skincare.jpg",
                        "/images/6skincare.jpg",
                        "/images/7skincare.jpg",
                        "/images/8skincare.jpg"
                    };
                    ViewData["Title"] = "Skincare Services";
                    break;

                case "Hair":
                    imageUrls = new List<string>
                    {
                        "/images/1hair.jpg",
                        "/images/2hair.jpg",
                        "/images/3hair.jpeg",
                        "/images/4hair.jpg",
                        "/images/5hair.jpeg",
                        "/images/6hair.jpg",
                        "/images/7hair.jpg",
                        "/images/8hair.jpg"
                    };
                    ViewData["Title"] = "Hair Services";
                    break;

                default:
                    return RedirectToAction("Index", "Home");
            }

            return View("Index", imageUrls);
        }

        // API Controller
        [Route("api/[controller]")]
        [ApiController]
        public class ServiceApiController : ControllerBase
        {
            private readonly BeautyCenterContext _context;

            public ServiceApiController(BeautyCenterContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult GetServices()
            {
                var services = _context.Services.ToList();
                return Ok(services);
            }

            [HttpPost]
            public IActionResult CreateService([FromBody] Service service)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetServices), new { id = service.Id }, service);
            }
        }
    }
}
