﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Employee
    {


        [Key, ForeignKey("ApplicationUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmployeeId { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime hireDate { get; set; } = DateTime.Now;

        public float Salary { get; set; }

    }
}
