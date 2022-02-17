using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entity
{
    public class Pizzeria
    {
        public int PizzeriaId { get; set; }
        [Required] public string Address { get; set; }


        public ICollection<Employee> Employees { get; set; }
    }
}
