using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewTrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int ZipCode { get; set; }
    }
}