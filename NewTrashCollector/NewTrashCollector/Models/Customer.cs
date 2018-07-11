using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewTrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {
                FullName = value;
            }
        }
        [ForeignKey("PickUpDay")]
        public int PickUpDayId { get; set; }
        public PickUpDay PickUpDay { get; set; }

        public DateTime? ExtraPickUpDay { get; set; }

        public double AmountOwed { get; set; }

    }
}