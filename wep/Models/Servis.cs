using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace wep.Models
{
    public class Servis
    {
        [Key]
        public int ServisID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Servis Adı ")]
        public string ServisName { get; set; }

        [Required]
        [Display(Name = "Servis Ücreti")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public string ServisFee { get; set; }

        public int EmployeeID { get; set; }
        public Employee employee { get; set; }

    }
}
