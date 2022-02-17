using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class PizzaSize
    {
        public int PizzaSizeId { get; set; }

        public int Mass { get; set; }
        public decimal Price { get; set; }

        //public int? SizeNum { get; set; }

        public string SizeName { get; set; }

        public int? PizzaInMenuId { get; set; }
        public PizzaInMenu PizzaInMenu { get; set; }
    }
}
