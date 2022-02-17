using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.EntityHelp
{
    public class PizzaAdd
    {

        [Display(Name = "Название:")]
        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Введите название!")]
        public string PizzaName { get; set; }


        [Required(ErrorMessage = "Введите массу!")]
        public int? MassSmall { get; set; }


        [Required(ErrorMessage = "Введите стоимость!")]
        public decimal? PriceSmall { get; set; }


        [Required(ErrorMessage = "Введите массу!")]
        public int? MassMedium { get; set; }


        [Required(ErrorMessage = "Введите стоимость!")]
        public decimal? PriceMedium { get; set; }


        [Required(ErrorMessage = "Введите массу!")]
        public int? MassBig { get; set; }


        [Required(ErrorMessage = "Введите стоимость!")]
        public decimal? PriceBig { get; set; }

        [Required(ErrorMessage = "Введите состав!")]
        [Display(Name = "Состав:")]
        public string? Components { get; set; }

    }
}
