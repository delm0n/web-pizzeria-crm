using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class Addish
    {
        public int AddishId { get; set; }

        [Display(Name = "Название:")]
        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Введите название!")]
        public string AddishName { get; set; }

        [Display(Name = "Тип закуски:")]
        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Выберите!")]
        public string TypeAddish { get; set; }

        [Display(Name = "Масса в граммах:")]
        [Required(ErrorMessage = "Введите массу!")]
        public int? Mass { get; set; }

        [Display(Name = "Цена:")]
        [Required(ErrorMessage = "Введите стоимость!")]
        public decimal? Price { get; set; }
        public bool IsActive { get; set; } = true;

       // public ICollection<ChosenAddish> ChosenAddishes { get; set; }

    }
}
