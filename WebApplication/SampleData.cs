using WebApplication.Data;
using System.Linq;
using WebApplication.Data.Entity;

namespace WebApplication
{
    public static class SampleData
    {
        public static void Initialize(ApplicationContext context)
        {
            /*
               context.Pizzerias.AddRange(
                   new Pizzeria { Address = "ул. Пушкина, дом 34"}, 
                   new Pizzeria { Address = "ул. Опарина, дом 41" }
                  );

            context.Roles.AddRange(
                   new Role { RoleName = "Администратор" },
                   new Role { RoleName = "Официант" }
                  );

            context.Employees.AddRange(new Employee { Lname = "Зуев", Fname = "Артём", Mname = "Игоревич", 
                Role = context.Roles.Where(e => e.RoleId == 1).First(), 
                Pizzeria = context.Pizzerias.Where(e => e.PizzeriaId == 1).First(),
               Login = "zu", Password = "123"});

             
            context.SaveChanges(); */
        }
    }
}
