using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SalonController : Controller
{
    private readonly BeautyCenterContext _context;

    public SalonController(BeautyCenterContext context)
    {
        _context = context;
    }

    // Salon Listeleme
    public IActionResult Index()
    {
        var salons = _context.Salons.Include(s => s.Services).ToList();
        return View(salons);
    }

    // Yeni Salon Ekleme
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Salon salon)
    {
        if (ModelState.IsValid)
        {
            _context.Salons.Add(salon);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(salon);
    }

    // Salon Detayları (İşlemler ve Çalışanları Gösterme)
    public IActionResult Details(int id)
    {
        var salon = _context.Salons
            .Include(s => s.Services)
            .Include(s => s.Employees)
            .FirstOrDefault(s => s.Id == id);

        if (salon == null) return NotFound();
        return View(salon);
    }

    // Salon Silme
    public IActionResult Delete(int id)
    {
        var salon = _context.Salons.Find(id);
        if (salon != null)
        {
            _context.Salons.Remove(salon);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
