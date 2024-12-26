using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.Entity.Concrete;
using BeautyCenter.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeautyCenter.Presentation.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly BeautyCenterContext _context;

        public AppointmentController(BeautyCenterContext context)
        {
            _context = context;
        }

        // -------------------------
        // MVC Aksiyonları (Razor View)
        // -------------------------

        [HttpGet]
        public IActionResult RandevuAl()
        {
            // Hizmetleri ViewBag ile gönder
            var services = _context.Services.ToList();
            ViewBag.Services = services;

            return View();
        }

        [HttpPost]
        public IActionResult RandevuAl(AppointmentViewModel model)
        {
            // Form validasyonu
            if (!ModelState.IsValid)
            {
                var services = _context.Services.ToList();
                ViewBag.Services = services;
                return View(model);
            }

            // Çakışma kontrolü:
            // Employee seçimi formda yoksa sabit bir değer kullanabiliriz. Örnek: EmployeeId = 1
            int employeeId = 1;

            bool isConflict = _context.Appointments.Any(a =>
    a.EmployeeId == employeeId &&
    a.AppointmentDate == DateTime.SpecifyKind(model.AppointmentDate, DateTimeKind.Utc));


            if (isConflict)
            {
                ModelState.AddModelError("", "Bu çalışan için belirtilen tarih ve saat dolu. Lütfen başka bir zaman seçiniz.");
                var services = _context.Services.ToList();
                ViewBag.Services = services;
                return View(model);
            }

            // Seçilen servis
            var service = _context.Services.Find(model.ServiceId);
            if (service == null)
            {
                ModelState.AddModelError("", "Geçersiz bir hizmet seçtiniz.");
                var services = _context.Services.ToList();
                ViewBag.Services = services;
                return View(model);
            }

            // Appointment oluştur
            var appointment = new Appointment
            {
                CustomerName = model.CustomerName,
                CustomerContact = model.CustomerContact,
                ServiceId = model.ServiceId,
                EmployeeId = employeeId,
                AppointmentDate = DateTime.SpecifyKind(model.AppointmentDate, DateTimeKind.Utc),
                Status = "Pending",
                IsConfirmed = false,
                AppointmentPrice = service.ServicePrice,
                Duration = service.ServiceDuration,
                AppointmentServiceType = service.ServiceName // Burada uygun bir değer atanıyor
            };


            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            TempData["Success"] = "Randevunuz başarıyla oluşturuldu!";
            return RedirectToAction("RandevuAl");
        }

        // -------------------------
        // API Aksiyonları (JSON Sonuç)
        // -------------------------

        [HttpGet]
        public JsonResult GetAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Employee)
                .ToList();

            return Json(appointments);
        }

        [HttpPost]
        public JsonResult CreateAppointmentJson([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "Model valid değil." });
            }

            appointment.AppointmentDate = DateTime.SpecifyKind(appointment.AppointmentDate, DateTimeKind.Utc);

            var isConflict = _context.Appointments.Any(a =>
                a.EmployeeId == appointment.EmployeeId &&
                a.AppointmentDate == appointment.AppointmentDate);

            if (isConflict)
                return Json(new { error = "Bu çalışan için belirtilen tarih ve saat zaten dolu." });

            appointment.Employee = _context.Employees.Find(appointment.EmployeeId);
            appointment.Service = _context.Services.Find(appointment.ServiceId);

            if (appointment.Employee == null || appointment.Service == null)
            {
                return Json(new { error = "EmployeeId veya ServiceId geçersiz." });
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Json(new { message = "Randevu oluşturuldu.", appointment });
        }

        [HttpPut]
        public JsonResult ApproveAppointmentJson(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return Json(new { error = "Randevu bulunamadı." });
            }

            appointment.Status = "Onaylandı";
            appointment.IsConfirmed = true;
            _context.SaveChanges();

            return Json(new { message = "Randevu onaylandı.", appointment });
        }

        [HttpGet]
        public JsonResult GetAppointmentByIdJson(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Employee)
                .Include(a => a.Service)
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null)
                return Json(new { error = "Randevu bulunamadı." });

            return Json(appointment);
        }
    }
}
