using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [Display(Name = "Название:")]
        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Введите название!")] 
        public string IngredientName { get; set; }

        [Display(Name = "Масса в граммах:")]
        [Required(ErrorMessage = "Введите массу!")]
        public int? Mass { get; set; }

        [Display(Name = "Цена:")]
        [Required(ErrorMessage = "Введите стоимость!")]
        public decimal? Price { get; set; }
        public bool IsActive { get; set; } = true;

       // public ICollection<ChosenPizza> ChosenPizzas { get; set; } //возможно нужен будет словарь

    }
}
