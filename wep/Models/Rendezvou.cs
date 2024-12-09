using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace wep.Models
{
    public class Rendezvou
    {
        public int RendezvouID { get; set; }
        public DateTime RandezvouTime { get; set; }
        public Servis servis { get; set; }
        public User user { get; set; }
    }
}
//RanID ,Service        ,User
//1     ,2 -> Hair cut  ,3 -> Fulan
//2,    ,2 -> Hair cut  ,4 -> 3elan ?? Service availabily
//3,    ,1 -> Hair dye  ,4 -> 3elan ?? Service availabily

//Service 1 
//Hair cut, Employee
//