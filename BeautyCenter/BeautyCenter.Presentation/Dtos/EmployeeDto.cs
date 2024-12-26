namespace BeautyCenter.Presentation.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeSpecialty { get; set; }
        public string AvailableHours { get; set; }
        public int SalonId { get; set; }
    }
}
