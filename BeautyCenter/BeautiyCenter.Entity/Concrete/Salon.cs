using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class Salon
    {
        public int SalonId { get; set; } // Benzersiz kimlik numarası
        public string SalonName { get; set; } // Salon adı
        public string SalonAddress { get; set; } // Salon adresi
        public string SalonPhoneNumber { get; set; } // Telefon numarası
        public string WorkingHours { get; set; } // Çalışma saatleri
    }
}
