using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace wep.Models
{
    public class Rendezvou
    {
        [NotMapped]
        public static HashSet<TimeOnly> timing = new HashSet<TimeOnly>() {
            new TimeOnly(15,0)
            ,new TimeOnly(16,0)
            ,new TimeOnly(17,0)
            ,new TimeOnly(18,0)
        };
        [Key]
        public int RendezvouID { get; set; }
        [Required]
        public int ServisID { get; set; }
 
        public int UserID { get; set; }
        [Required]
        public DateTime RandezvouTime { get; set; }     
        
        public virtual Servis servis { get; set; }

        public virtual User user { get; set; }

    }
}
//RanID ,Service        ,User
//1     ,2 -> Hair cut  ,3 -> Fulan
//2,    ,2 -> Hair cut  ,4 -> 3elan ?? Service availabily
//3,    ,1 -> Hair dye  ,4 -> 3elan ?? Service availabily

//Service 1 
//Hair cut, Employee
//