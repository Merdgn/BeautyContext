using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class Category
    {
        public int CategoryId { get; set; } // Benzersiz kimlik numarası
        public string CategoryName { get; set; } // Hizmet türü adı (örn. Makeup, Skincare, Hair)

        // Hizmetler ile ilişki
        public ICollection<Service> Services { get; set; }
    }
}
