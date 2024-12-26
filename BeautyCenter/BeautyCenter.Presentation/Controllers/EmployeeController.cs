using BeautiyCenter.DataAccess.Context;
using BeautiyCenter.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyCenter.Presentation.Dtos;


namespace BeautyCenter.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BeautyCenterContext _context;

        public EmployeeController(BeautyCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees
                                    .Include(e => e.Salon)
                                    .Select(e => new EmployeeDto
                                    {
                                        Id = e.Id,
                                        EmployeeFullName = e.EmployeeFullName,
                                        EmployeeSpecialty = e.EmployeeSpecialty,
                                        AvailableHours = e.AvailableHours,
                                        SalonId = e.SalonId
                                    })
                                    .ToList();
            return Ok(employees);
        }



        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null.");
            }

            var employee = new Employee
            {
                EmployeeFullName = employeeDto.EmployeeFullName,
                EmployeeSpecialty = employeeDto.EmployeeSpecialty,
                AvailableHours = employeeDto.AvailableHours,
                SalonId = employeeDto.SalonId // Sadece SalonId kullanıyoruz
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }

    }
}
