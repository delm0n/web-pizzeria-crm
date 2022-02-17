using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication.Data.Entity
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Вы не ввели фамилию!")]
        [Display(Name = "Фамилия:")]
        public string Lname { get; set; }

        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Вы не ввели имя!")]
        [Display(Name = "Имя:")]
        public string Fname { get; set; }

        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Display(Name = "Отчество:")]
        public string? Mname { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public int? PizzeriaId { get; set; }
        public Pizzeria Pizzeria { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Вы не ввели логин!")]
        [Display(Name = "Логин:")]
        public string Login { get; set; }

        //[MaxLength(20)]
        [StringLength(20, ErrorMessage = "Недопустимое количество символов!")]
        [Required(ErrorMessage = "Вы не ввели пароль!")]
        [UIHint("Password")]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ClientOrder> ClientOrders { get; set; }

    }
}
