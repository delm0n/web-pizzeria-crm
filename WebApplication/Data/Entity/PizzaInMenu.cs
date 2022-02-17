using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class PizzaInMenu
    {
        public int PizzaInMenuId { get; set; }
        public string PizzaInMenuName { get; set; }
        public string Components { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PizzaSize> PizzaSize { get; set; }
        //public ICollection<ChosenPizza> ChosenPizzas { get; set; }
    }
}
