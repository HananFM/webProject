using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace wep.Models
{
    public class Rendezvou
    { 
        public int RendezvouID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Servis Adı ")]
        public string RendezvouName { get; set; }

        
        public int EmployeeID { get; set; }
        public Employee employee { get; set; }

    }
}
