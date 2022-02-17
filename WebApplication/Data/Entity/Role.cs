using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entity
{
    public class Role
    {
        public int RoleId { get; set; }
        [MaxLength(20)] [Required] public string RoleName { get; set; }
        
        public ICollection<Employee> Employees { get; set; }
    }
}
