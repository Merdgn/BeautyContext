using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyCenter.Presentation.ViewModels
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage = "Müşteri Adı girilmesi zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Müşteri İletişim bilgisi girilmesi zorunludur.")]
        public string CustomerContact { get; set; }

        [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Randevu tarihi seçimi zorunludur.")]
        public DateTime AppointmentDate { get; set; }
    }
}
