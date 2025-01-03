﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

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
        [ValidateNever]//Avoid validating
        public string UserID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime RandezvouTime { get; set; }
        [Required]
        [ForeignKey("ServisID")]
        [ValidateNever]//Avoid validating
        public virtual Servis servis { get; set; }
        [Required]
        [ForeignKey("UserID")]
        [ValidateNever]//Avoid validating
        public virtual UserDetails user { get; set; }

    }
}
//RanID ,Service        ,User
//1     ,2 -> Hair cut  ,3 -> Fulan
//2,    ,2 -> Hair cut  ,4 -> 3elan ?? Service availabily
//3,    ,1 -> Hair dye  ,4 -> 3elan ?? Service availabily

//Service 1 
//Hair cut, Employee
//