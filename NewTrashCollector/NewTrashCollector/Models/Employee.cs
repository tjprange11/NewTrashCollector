﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewTrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ZipCode { get; set; }
    }
}