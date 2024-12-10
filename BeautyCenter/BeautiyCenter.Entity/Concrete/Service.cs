using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class Service
    {
        public int ServiceId { get; set; } // Hizmetin benzersiz kimlik numarası
        public string ServiceName { get; set; } // Hizmet adı (örn. Saç Kesimi)
        public decimal ServicePrice { get; set; } // Hizmetin ücreti
        public int ServiceDuration { get; set; } // Hizmetin süresi (dakika cinsinden)
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
