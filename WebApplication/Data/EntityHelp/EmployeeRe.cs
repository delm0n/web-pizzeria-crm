using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication.Data.EntityHelp
{
    public class EmployeeRe
    {
        public int? EmployeeId { get; set; }

        [MaxLength(20)]
        [Display(Name = "Фамилия:")]
        public string? Lname { get; set; }

        [MaxLength(20)]
        [Display(Name = "Имя:")]
        public string? Fname { get; set; }

        [MaxLength(20)]
        [Display(Name = "Отчество:")]
        public string? Mname { get; set; }

        [MaxLength(20)]
        [Display(Name = "Логин:")]
        public string? Login { get; set; }

        [MaxLength(20)]
        [Display(Name = "Пароль:")]
        public string? Password { get; set; }

    }
}
