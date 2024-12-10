using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class Employee
    {
        public int EmployeeId { get; set; } // Benzersiz kimlik numarası
        public string EmployeeFullName { get; set; } // Çalışanın tam adı
        public string EmployeeSpecialty { get; set; } // Uzmanlık alanı
        public string AvailableHours { get; set; } // Müsaitlik saatleri
        public int SalonId { get; set; } // Foreign Key
        public Salon Salon { get; set; } // Navigation Property
    }
}
