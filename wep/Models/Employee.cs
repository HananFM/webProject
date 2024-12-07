using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace wep.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = " Çalışan Ad")]
        public string EmployeeName { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Çalışan Soyad")]
        public string EmployeeSurname { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Çalışan Uzmanlık Alanı")]
        public string EmployeeExperience { get; set; }
        [Required]
        [Display(Name = "Çalışma saatleri")]
        public int workingHours{ get; set; }

    }
}
