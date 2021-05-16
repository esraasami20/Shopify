using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Employee: ApplicationUser
    {
        public DateTime hireDate { get; set; } = DateTime.Now;

        public float Salary { get; set; }

    }
}
