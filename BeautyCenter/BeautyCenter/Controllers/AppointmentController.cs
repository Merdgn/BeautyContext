using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautyCenter.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly BeautyCenterContext _context;

        public AppointmentController(BeautyCenterContext context)
        {
            _context = context;
        }

        // Tüm Randevuları Getir
        [HttpGet]
        public IActionResult GetAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Service)   // Hizmet türünü dahil et
                .Include(a => a.Employee)  // Çalışanı dahil et
                .ToList();

            return Ok(appointments);
        }

        // Yeni Randevu Oluştur
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // AppointmentDate'i UTC'ye dönüştür
            appointment.AppointmentDate = DateTime.SpecifyKind(appointment.AppointmentDate, DateTimeKind.Utc);

            // Çakışma kontrolü
            var isConflict = _context.Appointments.Any(a =>
                a.EmployeeId == appointment.EmployeeId &&
                a.AppointmentDate == appointment.AppointmentDate);

            if (isConflict)
                return BadRequest("Bu çalışan için belirtilen tarih ve saat zaten dolu.");

            // Employee ve Service verisini çekip navigation property'e ekle
            appointment.Employee = _context.Employees.Find(appointment.EmployeeId);
            appointment.Service = _context.Services.Find(appointment.ServiceId);

            if (appointment.Employee == null || appointment.Service == null)
            {
                return BadRequest("EmployeeId veya ServiceId geçersiz.");
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CreateAppointment), new { id = appointment.Id }, appointment);
        }


        // Randevu Onaylama
        [HttpPut("{id}")]
        public IActionResult ApproveAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound(new { message = "Randevu bulunamadı." });
            }

            appointment.Status = "Onaylandı";
            appointment.IsConfirmed = true;
            _context.SaveChanges();

            return Ok(new { message = "Randevu başarıyla onaylandı.", appointment });
        }


        // Tek Bir Randevu Getir
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Employee)
                .Include(a => a.Service)
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null)
                return NotFound("Randevu bulunamadı.");

            return Ok(appointment);
        }
    }
}
