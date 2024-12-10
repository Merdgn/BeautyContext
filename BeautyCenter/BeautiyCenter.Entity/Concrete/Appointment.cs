using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // Benzersiz kimlik numarası
        public DateTime AppointmentDate { get; set; } // Randevu tarihi ve saati
        public string AppointmentServiceType { get; set; } // Hizmet türü
        public decimal AppointmentPrice { get; set; } // Hizmet ücreti
        public string CustomerName { get; set; } // Müşteri adı
        public string CustomerContact { get; set; } // Müşteri iletişim bilgisi
        public bool IsConfirmed { get; set; } // Randevu onay durumu
        public int EmployeeId { get; set; } // Foreign Key
        public Employee Employee { get; set; } // Navigation Property
    }
}
