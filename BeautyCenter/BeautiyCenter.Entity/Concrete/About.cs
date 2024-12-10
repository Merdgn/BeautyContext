using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.Concrete
{
    public class About
    {
        public int AboutId { get; set; } // Benzersiz kimlik numarası
        public string Title { get; set; } // Başlık (örn. Hakkımızda)
        public string Description { get; set; } // Açıklama
        public string ImageUrl { get; set; } // Görsel URL (isteğe bağlı)
    }
}
