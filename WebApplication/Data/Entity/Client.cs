using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication.Data.Entity
{
    public class Client
    {
        [Key]
        [Required(ErrorMessage = "Вы не ввели телефон!")]
        [Display(Name = "Телефон:")] 
        public long Telephone { get; set; }

        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Display(Name = "Имя:")]
        public string? ClientName { get; set; }
        public ICollection<ClientOrder> ClientOrders { get; set; }
    }
}
