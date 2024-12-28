using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [Display(Name = "Servis Ücreti")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int ServisFee { get; set; }
        [Display(Name = "Servis Süre")]
        public string ServisDuration { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        [ValidateNever]//Avoid validating
        public Employee employee { get; set; }
        public virtual ICollection<Rendezvou>? rendezvous { get; set; }
    }
}
