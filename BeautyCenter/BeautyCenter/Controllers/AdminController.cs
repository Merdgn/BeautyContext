using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautiyCenter.Business.Abstract;
using BeautiyCenter.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BeautyCenter.Presentation.Controllers
{
    [Authorize] // Tüm giriş yapmış kullanıcılar erişebilir
    public class AdminController : Controller

    {
        private readonly ISalonService _salonService;
        private readonly IEmployeeService _employeeService;
        private readonly IAppointmentService _appointmentService;

        public AdminController(ISalonService salonService, IEmployeeService employeeService, IAppointmentService appointmentService)
        {
            _salonService = salonService;
            _employeeService = employeeService;
            _appointmentService = appointmentService;
        }

        // --- Salon Yönetimi ---
        public IActionResult SalonManagement()
        {
            var salons = _salonService.TGetAll();
            return View(salons);
        }

        [HttpPost]
        public IActionResult AddSalon(Salon salon)
        {
            if (!ModelState.IsValid) return View("SalonManagement", _salonService.TGetAll());

            _salonService.TInsert(salon);
            return RedirectToAction("SalonManagement");
        }

        [HttpPost]
        public IActionResult DeleteSalon(int id)
        {
            _salonService.TDelete(id);
            return RedirectToAction("SalonManagement");
        }

        // --- Çalışan Yönetimi ---
        public IActionResult EmployeeManagement()
        {
            ViewBag.Salons = _salonService.TGetAll();
            var employees = _employeeService.TGetAll();
            return View(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Salons = _salonService.TGetAll();
                return View("EmployeeManagement", _employeeService.TGetAll());
            }

            _employeeService.TInsert(employee);
            return RedirectToAction("EmployeeManagement");
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeService.TDelete(id);
            return RedirectToAction("EmployeeManagement");
        }

        // --- Randevu Yönetimi ---
        public IActionResult AppointmentManagement()
        {
            var appointments = _appointmentService.GetAllWithDetails(); // Service ve Employee dahil
            return View(appointments);
        }


        [HttpPost]
        public IActionResult ApproveAppointment(int id)
        {
            var appointment = _appointmentService.TGetById(id);
            if (appointment != null)
            {
                appointment.Status = "Onaylandı";
                appointment.IsConfirmed = true;
                _appointmentService.TUpdate(appointment);
            }
            return RedirectToAction("AppointmentManagement");
        }

        [HttpPost]
        public IActionResult DeleteAppointment(int id)
        {
            _appointmentService.TDelete(id);
            return RedirectToAction("AppointmentManagement");
        }
    }
}
