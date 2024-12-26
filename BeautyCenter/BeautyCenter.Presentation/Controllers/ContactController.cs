using Microsoft.AspNetCore.Mvc;
using BeautiyCenter.Entity.Concrete;
using BeautiyCenter.DataAccess.Context;

namespace BeautyCenter.Presentation.Controllers
{
    public class ContactController : Controller
    {
        private readonly BeautyCenterContext _context;

        public ContactController(BeautyCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                // Form verisini veritabanına ekle
                _context.Contacts.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Your message has been sent successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

