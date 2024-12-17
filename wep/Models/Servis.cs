using System.ComponentModel.DataAnnotations;
using System.Drawing;
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

        [Display(Name = "Servis Ücreti")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int ServisFee { get; set; }

        public int EmployeeID { get; set; }
        public Employee employee { get; set; }
        public virtual ICollection<Rendezvou>? rendezvous { get; set; }
    }
}
