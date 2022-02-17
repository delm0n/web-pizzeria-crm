using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using System.Linq;
using WebApplication.Data.Entity;
using WebApplication.Data.EntityHelp;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Web;


namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        static public int idPizzaInUser;
        static public int idUser;

        public static List<string> staticPizzasList = new List<string>();
        public static List<string> staticIngredsList = new List<string>();
        public static List<int> staticMassList = new List<int>();
        public static List<decimal> staticPriceList = new List<decimal>();

        public void Cleaner()
        {
            staticMassList.Clear();
            staticPriceList.Clear();
            staticPizzasList.Clear();
            staticIngredsList.Clear();

            HttpContext.Session.Remove("clientAddishNames");
            HttpContext.Session.Remove("clientAddishPrice");
            HttpContext.Session.Remove("clientAddishMass");

            HttpContext.Session.Remove("clientPizza");
            HttpContext.Session.Remove("clientPizzaMass");
            HttpContext.Session.Remove("clientPizzaPrice");


            //HttpContext.Session.Clear();
        }

        public void AddInSession(string totalPizzaName, string? totalOrder, decimal totalPrice, int totalMass)
        {
            staticPizzasList.Add(totalPizzaName);
            staticMassList.Add(totalMass);
            staticPriceList.Add(totalPrice);

            if (String.IsNullOrEmpty(totalOrder))
                staticIngredsList.Add("Без доп. ингредиентов");
            else
                staticIngredsList.Add(totalOrder);

            //int getId = int.Parse(totalPizzaName.Split(' ')[0]);

            string containerPizzas = String.Join(", ", staticPizzasList.ToArray());
            string containerIngreds = String.Join(", ", staticIngredsList.ToArray());
            string containerMass = staticMassList.ToArray().Sum().ToString();
            string containerPrice = staticPriceList.ToArray().Sum().ToString();

            HttpContext.Session.SetString("clientPizza", $"{containerPizzas} + {containerIngreds} ");
            HttpContext.Session.SetString("clientPizzaMass", containerMass);
            HttpContext.Session.SetString("clientPizzaPrice", containerPrice);

        }



        public IActionResult Login_app() 
        {
            return View("~/Views/Home/Login.cshtml"); 
        }


        [HttpPost]
        public async Task<IActionResult> Login_app(string login, string password)
        {
            Employee employee = await db.Employees.Where(e => e.IsActive == true)
                .FirstOrDefaultAsync(e => e.Login == login && e.Password == password);

            if (employee == null)
            {
                ModelState.AddModelError(nameof(login), " ");
                ModelState.AddModelError(nameof(password), "Пользователь не найден...");
            }

            if (ModelState.IsValid)
            {
                ViewBag.NameOne = employee.Fname;
                ViewBag.NameTwo = employee.Mname;
                HttpContext.Session.SetString("employeeName", $"{employee.Lname} {employee.Fname}");
                idUser = employee.EmployeeId;

                if (employee.PizzeriaId != null) //для хранения глобальной переменной - id пиццерии у user
                {
                    idPizzaInUser = int.Parse(employee.PizzeriaId.ToString());
                    ViewBag.PizzIdBag = idPizzaInUser;
                }

                if (employee.RoleId == 1)
                    return View("~/Views/Home/HomeAdmin/HomeAdmin.cshtml", employee);

                else
                    return View("~/Views/Home/HomeWaiter/HomeWaiter.cshtml", employee);
            }

             else
                return View("~/Views/Home/Login.cshtml");
        }


        public IActionResult Logout() //функцию очистки вставить
        {
            Cleaner();
            return View("~/Views/Home/Login.cshtml");
        }





        #region Empls


        [HttpPost]
        public async Task<IActionResult> Empls_panel()
        {
            // админу должны быть видны только работники его пиццерии
            return View("~/Views/Home/HomeAdmin/Empls_panel.cshtml",
                await db.Employees
                .Where(e => e.IsActive == true && e.Pizzeria.PizzeriaId.Equals(idPizzaInUser)
                && e.Role.RoleId.Equals(2)).OrderBy(e => e.Lname).ToListAsync());
        }


        [HttpPost]
        public IActionResult Empls_panel_form()
        {
            return View("~/Views/Home/HomeAdmin/Forms/Empls_form.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add_new_empl(Employee model)
        {

            ModelState.Remove("Role");
            ModelState.Remove("Pizzeria");
            ModelState.Remove("ClientOrders");

            if (await db.Employees.Where(e => e.Login == model.Login).FirstOrDefaultAsync() != null )
            {
                ModelState.AddModelError(nameof(model.Login), "Такой логин уже есть!");
            }


            if (ModelState.IsValid)
            {
                Employee employee = new Employee{ 
                    Lname = model.Lname,
                    Fname = model.Fname,
                    Mname = model.Mname,
                    Login = model.Login,
                    Password = model.Password,
                    Pizzeria = await db.Pizzerias.Where(p => p.PizzeriaId == idPizzaInUser).FirstAsync(),
                    Role = await db.Roles.Where(r => r.RoleName == "Официант").FirstAsync(),
                    IsActive = model.IsActive
                };


                db.Employees.Add(employee);
                await db.SaveChangesAsync();

                return View("~/Views/Home/HomeAdmin/Empls_panel.cshtml",
                await db.Employees
                .Where(e => e.IsActive == true && e.Pizzeria.PizzeriaId.Equals(idPizzaInUser)
                && e.Role.RoleId.Equals(2)).OrderBy(e => e.Lname).ToListAsync());
            }

            else
            {
                return View("~/Views/Home/HomeAdmin/Forms/Empls_form.cshtml");
            }
        }
 
        public async Task<IActionResult> Empls_reactive(int? id)
        {

            if (id != null)
            {
                Employee employee = await db.Employees.FirstAsync(e => e.EmployeeId == id);
                if (employee != null)
                {
                    if (employee.IsActive == true)
                        employee.IsActive = false;
                    else employee.IsActive = true;

                    await db.SaveChangesAsync();

                    return View("~/Views/Home/HomeAdmin/Empls_panel.cshtml",
                await db.Employees
                .Where(e => e.IsActive == true && e.Pizzeria.PizzeriaId.Equals(idPizzaInUser)
                && e.Role.RoleId.Equals(2)).OrderBy(e => e.Lname).ToListAsync());
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Empls_edit_page(int? id)
        {
            if(id != null)
            {
                Employee employee = await db.Employees.FirstAsync(e => e.EmployeeId == id);


                //re
                ViewBag.EmplId = id;
                ViewBag.EmplLn = employee.Lname;
                ViewBag.EmplFn = employee.Fname;
                ViewBag.EmplMn = employee.Mname;
                ViewBag.EmplLo = employee.Login;
                ViewBag.EmplPa = employee.Password;

                return View("~/Views/Home/HomeAdmin/Forms/Empls_re.cshtml");
            }
            
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Empls_edit(EmployeeRe model)
        {
            if (model.EmployeeId != null)
            {
                Employee employee = await db.Employees.FirstAsync(e => e.EmployeeId == model.EmployeeId);

                if (model.Lname != null || model.Fname != null || model.Mname != null ||
                    model.Password != null || model.Login != null)
                {
                    if (model.Lname != null) employee.Lname = model.Lname;
                    if (model.Fname != null) employee.Fname = model.Fname;
                    if (model.Mname != null) employee.Mname = model.Mname;
                    if (model.Password != null) employee.Password = model.Password;

                    if(model.Login != null)
                    {
                        if (db.Employees.Where(e => e.Login == model.Login).FirstOrDefault() != null)
                            ModelState.AddModelError(nameof(model.Login), "Такой логин уже есть!");
                        else employee.Login = model.Login;
                    }

                    if (ModelState.IsValid)
                    {
                        await db.SaveChangesAsync();
                    } 

                    else
                    {
                        ViewBag.EmplId = model.EmployeeId;
                        ViewBag.EmplLn = employee.Lname;
                        ViewBag.EmplFn = employee.Fname;
                        ViewBag.EmplMn = employee.Mname;
                        ViewBag.EmplLo = employee.Login;
                        ViewBag.EmplPa = employee.Password;
                        return View("~/Views/Home/HomeAdmin/Forms/Empls_re.cshtml");
                    }
                }
            }


            return View("~/Views/Home/HomeAdmin/Empls_panel.cshtml",
                await db.Employees
                .Where(e => e.IsActive == true && e.Pizzeria.PizzeriaId.Equals(idPizzaInUser)
                && e.Role.RoleId.Equals(2)).OrderBy(e => e.Lname).ToListAsync());
        }

        public async Task<IActionResult> Empls_unactive()
        {
            return View("~/Views/Home/HomeAdmin/SubPage/Empls_unactive.cshtml",
                await db.Employees.Where(e => e.IsActive == false 
                && e.Pizzeria.PizzeriaId.Equals(idPizzaInUser)
                && e.Role.RoleId.Equals(2)).OrderBy(e => e.Lname).ToListAsync());

        }



        #endregion



        #region Addishes 
        

        [HttpPost]
        public async Task<IActionResult> Addishes_panel()
        {        
            return View("~/Views/Home/HomeAdmin/Addishes_panel.cshtml",
                await db.Addishes
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.TypeAddish)
                .ToListAsync());
        }

        [HttpPost]
        public IActionResult Addishes_panel_form()
        {
            return View("~/Views/Home/HomeAdmin/Forms/Addishes_form.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add_new_addish(Addish addish)
        {

            if (ModelState.IsValid)
            {
                db.Addishes.Add(addish);
                await db.SaveChangesAsync();

                return View("~/Views/Home/HomeAdmin/Addishes_panel.cshtml",
                await db.Addishes
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.TypeAddish)
                .ToListAsync());
            }

            else
            {
                return View("~/Views/Home/HomeAdmin/Forms/Addishes_form.cshtml");
            }
        }

        public async Task<IActionResult> Addishes_reactive(int? id)
        {

            if (id != null)
            {
                Addish addish = await db.Addishes.FirstAsync(e => e.AddishId == id);
                if (addish != null)
                {
                    if (addish.IsActive == true)
                        addish.IsActive = false;
                    else addish.IsActive = true;

                    await db.SaveChangesAsync();

                    return View("~/Views/Home/HomeAdmin/Addishes_panel.cshtml",
                await db.Addishes
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.TypeAddish)
                .ToListAsync());
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Addishes_unactive()
        {
            return View("~/Views/Home/HomeAdmin/SubPage/Addishes_unactive.cshtml",
                await db.Addishes
                .Where(e => e.IsActive == false)
                .OrderBy(e => e.TypeAddish)
                .ToListAsync());

        }
        #endregion



        #region Ingredients

        [HttpPost]
        public async Task<IActionResult> Ingreds_panel()
        {
            return View("~/Views/Home/HomeAdmin/Ingreds_panel.cshtml",
                await db.Ingredients
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.IngredientName)
                .ToListAsync());
        }


        [HttpPost]
        public IActionResult Ingreds_panel_form()
        {
            return View("~/Views/Home/HomeAdmin/Forms/Ingreds_form.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Add_new_ingred(Ingredient ingred)
        {

            //ModelState.Remove("ChosenPizzas");

            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingred);
                await db.SaveChangesAsync();

                return View("~/Views/Home/HomeAdmin/Ingreds_panel.cshtml",
                await db.Ingredients
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.IngredientName)
                .ToListAsync());
            }

            else
            {
                return View("~/Views/Home/HomeAdmin/Forms/Ingreds_form.cshtml");
            }
        }

        public async Task<IActionResult> Ingreds_unactive()
        {
            return View("~/Views/Home/HomeAdmin/SubPage/Ingreds_unactive.cshtml",
                await db.Ingredients
                .Where(e => e.IsActive == false)
                .OrderBy(e => e.IngredientName)
                .ToListAsync());

        }

        public async Task<IActionResult> Ingreds_reactive(int? id)
        {

            if (id != null)
            {
                Ingredient ingredient = await db.Ingredients.FirstAsync(e => e.IngredientId == id);
                if (ingredient != null)
                {
                    if (ingredient.IsActive == true)
                        ingredient.IsActive = false;
                    else ingredient.IsActive = true;

                    await db.SaveChangesAsync();

                    return View("~/Views/Home/HomeAdmin/Ingreds_panel.cshtml",
                await db.Ingredients
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.IngredientName)
                .ToListAsync());
                }
            }

            return NotFound();
        }

        #endregion



        #region Pizzas


        [HttpPost]
        public async Task<IActionResult> Pizzas_panel()
        {
            return View("~/Views/Home/HomeAdmin/Pizzas_panel.cshtml",
                await db.Pizzas
                .Where(e => e.IsActive == true)
                .ToListAsync());
        }

        [HttpPost]
        public IActionResult Pizzas_panel_form()
        {
            return View("~/Views/Home/HomeAdmin/Forms/Pizzas_form.cshtml");
        }

        [HttpPost]
        public IActionResult Add_new_pizza(PizzaAdd pizzaform)
        {

            if (db.Pizzas.Where(p => p.PizzaInMenuName == pizzaform.PizzaName).FirstOrDefault() != null)
            {
                ModelState.AddModelError(nameof(pizzaform.PizzaName), "Такая пицца уже есть!");
            }


            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                    
                        PizzaInMenu pizza = new PizzaInMenu
                        {
                            PizzaInMenuName = pizzaform.PizzaName,
                            Components = (string)pizzaform.Components
                        };

                        db.Pizzas.Add(pizza);
                        db.SaveChanges(); 

                        PizzaSize pizzaSizeSmall = new PizzaSize
                        {
                            Mass = (int)pizzaform.MassSmall,
                            Price = (decimal)pizzaform.PriceSmall,
                            SizeName = "Маленькая",
                            PizzaInMenu = db.Pizzas.Where(p => p.PizzaInMenuName == pizzaform.PizzaName).First()
                        };

                        PizzaSize pizzaSizeMedium = new PizzaSize
                        {
                            Mass = (int)pizzaform.MassMedium,
                            Price = (decimal)pizzaform.PriceMedium,
                            SizeName = "Средняя",
                            PizzaInMenu = db.Pizzas.Where(p => p.PizzaInMenuName == pizzaform.PizzaName).First()
                        };

                        PizzaSize pizzaSizeBig = new PizzaSize
                        {
                            Mass = (int)pizzaform.MassBig,
                            Price = (decimal)pizzaform.PriceBig,
                            SizeName = "Большая",
                            PizzaInMenu = db.Pizzas.Where(p => p.PizzaInMenuName == pizzaform.PizzaName).First()
                        };

                        db.PizzaSizes.AddRange(pizzaSizeSmall, pizzaSizeMedium, pizzaSizeBig);
                        db.SaveChanges();
                        transaction.Commit();
                        return View("~/Views/Home/HomeAdmin/Pizzas_panel.cshtml",
                        db.Pizzas
                        .Where(e => e.IsActive == true)
                        .ToList());
                        
                    }
                    catch
                    {
                        transaction.Rollback();
                        return View("~/Views/Home/Error_page.cshtml");
                    }
                }
            }
            else return View("~/Views/Home/HomeAdmin/Forms/Pizzas_form.cshtml");

        }

        public async Task<IActionResult> Pizzas_unactive()
        {
            return View("~/Views/Home/HomeAdmin/SubPage/Pizzas_unactive.cshtml",
                await db.Pizzas
                .Where(e => e.IsActive == false)
                .ToListAsync());

        }

        public async Task<IActionResult> Pizzas_reactive(int? id)
        {

            if (id != null)
            {
                PizzaInMenu pizza = await db.Pizzas.FirstAsync(e => e.PizzaInMenuId == id);
                if (pizza != null)
                {
                    if (pizza.IsActive == true)
                        pizza.IsActive = false;
                    else pizza.IsActive = true;

                    await db.SaveChangesAsync();

                    return View("~/Views/Home/HomeAdmin/Pizzas_panel.cshtml",
                await db.Pizzas
                .Where(e => e.IsActive == true)
                .ToListAsync());
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Pizzas_detali(int? id)
        {

            if (id != null)
            {
                PizzaInMenu pizza = await db.Pizzas.FirstAsync(e => e.PizzaInMenuId == id);
                if (pizza != null)
                {
                    ViewBag.NamePizza = pizza.PizzaInMenuName;
                    return View("~/Views/Home/HomeAdmin/SubPage/Pizzas_detali.cshtml",
                await db.PizzaSizes
                .Where(s => s.PizzaInMenu.PizzaInMenuId == id)
                .ToListAsync());
                }
            }

            return NotFound();
        }

        #endregion


        [HttpPost]
        public async Task<IActionResult> Orders_panel()
        {
            return View("~/Views/Home/HomeAdmin/Orders_panel.cshtml",
                await db.ClientOrders
                .ToListAsync());
        }



        #region Works
        /*------------------------ WORKS START ------------------------ */

        [HttpPost] 
        public IActionResult Work_panel()
        {
            return View("~/Views/Home/HomeWaiter/Startwork_panel.cshtml"); 
        }

        [HttpPost] //страница ввода данных клиента
        public async Task<IActionResult> Add_new_client(Client model)
        {
            ModelState.Remove("ClientOrders");

            Client ifExist = await db.Clients.Where(c => c.Telephone == model.Telephone).FirstOrDefaultAsync();

            if (ifExist == null)
            {
                //если клиента с таким номером нет

                if (model.ClientName == null)
                    ModelState.AddModelError(nameof(model.ClientName), "Введите имя!");

                if (ModelState.IsValid)
                {
                    Client client = new Client
                    {
                        Telephone = model.Telephone,
                        ClientName = model.ClientName
                    };

                    db.Clients.Add(client);
                    await db.SaveChangesAsync();

                    HttpContext.Session.SetString("clientName", model.ClientName); 
                    HttpContext.Session.SetString("clientTel", model.Telephone.ToString()); 

                    return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");


                }
                else //если при регистрации не продит проверку на валидность 
                    return View("~/Views/Home/HomeWaiter/Startwork_panel.cshtml");
            }

            else
            {
                //если киент с таким номером уже есть
                HttpContext.Session.SetString("clientName", ifExist.ClientName);
                HttpContext.Session.SetString("clientTel", ifExist.Telephone.ToString());
                return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");

            }
            return NotFound();
        }


        [HttpPost] //кнопка назад
        public IActionResult Order_back()
        {
            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }


        [HttpPost]
        public IActionResult New_client() //отмена заказа и удаление сохраненных промежуточных данных
        {
            Cleaner();
            //clientTel
            HttpContext.Session.Remove("clientTel");
            HttpContext.Session.Remove("clientName");
            return View("~/Views/Home/HomeWaiter/Startwork_panel.cshtml");
        }
        
         
        [HttpPost] //панель с дополнительными блюдами
        public async Task<IActionResult> Addish_order()
        {
            return View("~/Views/Home/HomeWaiter/Addish_panel.cshtml",
                        await db.Addishes
                        .Where(e => e.IsActive == true)
                        .OrderBy(e => e.TypeAddish)
                        .ToListAsync());
        }


        [HttpPost] //кнопка отправки данных в session 
        public IActionResult Get_addish_order(string totalOrder, decimal totalPrice, int totalMass)
        {
            if (!String.IsNullOrEmpty(totalOrder))
            {
                HttpContext.Session.SetString("clientAddishNames", totalOrder);
                HttpContext.Session.SetString("clientAddishPrice", totalPrice.ToString());
                HttpContext.Session.SetString("clientAddishMass", totalMass.ToString());
            }

            else
            {
                HttpContext.Session.Remove("clientAddishNames");
                HttpContext.Session.Remove("clientAddishPrice");
                HttpContext.Session.Remove("clientAddishMass");
            }

            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }


        [HttpPost] //панель с пицца join размер 
        public async Task<IActionResult> Pizza_order()
        {
            if(staticPizzasList.Count == 0)
            {
                List<PizzaInMenu> pizzaInMenu = await db.Pizzas.Where(p => p.IsActive == true).OrderBy(p => p.PizzaInMenuName).ToListAsync();
                List<PizzaSize> pizzaSize = await db.PizzaSizes.ToListAsync();

                var linq = from s in pizzaSize
                           join m in pizzaInMenu on s.PizzaInMenuId equals m.PizzaInMenuId
                           select new PizzaAndSize { PizzaSize = s, PizzaInMenu = m };


                return View("~/Views/Home/HomeWaiter/Pizzas_panel.cshtml", linq);
            }
            else //если мы выбралибольше одной пиццы появится панель Pizzas_panel_two с нашими выбранными пиццами
            //там появится возможность внести изменения в набор ингредиентов
            {
                return View("~/Views/Home/HomeWaiter/Pizzas_panel_two.cshtml", staticPizzasList);
            }
        }


        [HttpPost] //панель с пицца join размер при повторном вызове
        public async Task<IActionResult> Pizza_order_two()
        {
                List<PizzaInMenu> pizzaInMenu = await db.Pizzas.Where(p => p.IsActive == true).OrderBy(p => p.PizzaInMenuName).ToListAsync();
                List<PizzaSize> pizzaSize = await db.PizzaSizes.ToListAsync();

                var linq = from s in pizzaSize
                           join m in pizzaInMenu on s.PizzaInMenuId equals m.PizzaInMenuId
                           select new PizzaAndSize { PizzaSize = s, PizzaInMenu = m };


                return View("~/Views/Home/HomeWaiter/Pizzas_panel.cshtml", linq);
        }


        public async Task<IActionResult> Pizza_add_order(int? id) //первый раз добавляем пиццу
        {

            PizzaSize pizzaSize = await db.PizzaSizes.Where(s => s.PizzaSizeId == id).FirstAsync();
            PizzaInMenu pizzaInMenu = await db.Pizzas.Where(p => p.PizzaInMenuId == pizzaSize.PizzaInMenuId).FirstAsync();

            ViewBag.IdPizz = pizzaSize.PizzaSizeId;
            ViewBag.Name = pizzaInMenu.PizzaInMenuName;
            ViewBag.Size = pizzaSize.SizeName;
            ViewBag.Mass = pizzaSize.Mass;
            ViewBag.Price = pizzaSize.Price;

            return View("~/Views/Home/HomeWaiter/PizzaIngreds_panel.cshtml",
                    await db.Ingredients
                    .Where(e => e.IsActive == true)
                    .ToListAsync());
        }


        public async Task<IActionResult> Pizza_change(int? id) //изменение набора доп. ингредиентов к пицце
        {
            if (id != null)
            {
                //int.Parse(staticPizzasList[(int)id].Split(' ')[0]);

                //получаем id пиццы из БД, которую мы выбрали
                int idPizza = int.Parse(staticPizzasList[(int)id].Split(' ')[0]);

                PizzaSize pizzaSize = await db.PizzaSizes.Where(s => s.PizzaSizeId == idPizza).FirstAsync();
                PizzaInMenu pizzaInMenu = await db.Pizzas.Where(p => p.PizzaInMenuId == pizzaSize.PizzaInMenuId).FirstAsync();

                ViewBag.IdPizz = pizzaSize.PizzaSizeId;
                ViewBag.Name = pizzaInMenu.PizzaInMenuName;
                ViewBag.Size = pizzaSize.SizeName;
                ViewBag.Mass = pizzaSize.Mass;
                ViewBag.Price = pizzaSize.Price;

                ViewBag.IdInStatic = id;


                ViewBag.Ingreds = staticIngredsList[(int)id];

                return View("~/Views/Home/HomeWaiter/PizzaIngreds_panel_two.cshtml",
                        await db.Ingredients
                        .Where(e => e.IsActive == true)
                        .ToListAsync());

            }

            return NotFound();

        }
        

        
        [HttpPost] //добавляем пиццы в session
        public async Task<IActionResult> Get_pizza_order(string totalPizzaName, string? totalOrder, decimal totalPrice, int totalMass)
        {

            AddInSession(totalPizzaName, totalOrder, totalPrice, totalMass);

            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }



        [HttpPost] //замена пиццы
        public async Task<IActionResult> Get_pizza_order_two(int id, string totalPizzaName, string? totalOrder, decimal totalPrice, int totalMass)
        {

            staticPizzasList.RemoveAt(id);
            staticIngredsList.RemoveAt(id);
            staticPriceList.RemoveAt(id);
            staticMassList.RemoveAt(id);

            AddInSession(totalPizzaName, totalOrder, totalPrice, totalMass);


            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }

        public IActionResult Pizza_delete(int id)
        {

            staticPizzasList.RemoveAt(id);
            staticIngredsList.RemoveAt(id);
            staticPriceList.RemoveAt(id);
            staticMassList.RemoveAt(id);

            string containerPizzas = String.Join(", ", staticPizzasList.ToArray());
            string containerIngreds = String.Join(", ", staticIngredsList.ToArray());
            string containerMass = staticMassList.ToArray().Sum().ToString();
            string containerPrice = staticPriceList.ToArray().Sum().ToString();

            HttpContext.Session.SetString("clientPizza", $"{containerPizzas} + {containerIngreds} ");
            HttpContext.Session.SetString("clientPizzaMass", containerMass);
            HttpContext.Session.SetString("clientPizzaPrice", containerPrice);

            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }

        public async Task<IActionResult> Done_order()
        {
            if (staticPizzasList.Count() > 0 || HttpContext.Session.GetString("clientAddishNames") != null)
            {

            
                string text = "Клиент: " + HttpContext.Session.GetString("clientName") + "\n";

                double price = 0;
                int mass = 0;

                if (staticPizzasList.Count() > 0)
                {
                    price += Double.Parse(HttpContext.Session.GetString("clientPizzaPrice"));
                    mass += Int32.Parse(HttpContext.Session.GetString("clientPizzaMass"));
                    for (int i = 0; i < staticPizzasList.Count(); i++)
                        text += staticPizzasList[i].Substring(2) + " --> " + staticIngredsList[i].Trim(new char[] { '{', '}' }) + "\n";
                }

                if (HttpContext.Session.GetString("clientAddishNames") != null)
                {
                    price += Double.Parse(HttpContext.Session.GetString("clientAddishPrice"));
                    mass += Int32.Parse(HttpContext.Session.GetString("clientAddishMass"));
                    text += HttpContext.Session.GetString("clientAddishNames").Trim(new char[] { '{', '}' }) + "\n";
                }

                Employee employee = await db.Employees.Where(e => e.EmployeeId == idUser).FirstAsync();

                ClientOrder clientOrder = new ClientOrder
                {
                    Telephone = Int64.Parse(HttpContext.Session.GetString("clientTel")),
                    Client = await db.Clients.Where(c => c.Telephone == Int64.Parse(HttpContext.Session.GetString("clientTel"))).FirstAsync(),
                    Employee = employee,
                    NameWaiter = $"{employee.Lname} {employee.Fname}",
                    TextOrder = text,
                    LastMass = mass,
                    LastPrice = price

                };

                db.ClientOrders.Add(clientOrder);
                await db.SaveChangesAsync();

                return View("~/Views/Home/HomeWaiter/Order_done.cshtml");

        
            }

            else return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }

        public IActionResult Clear_order()
        {
            Cleaner();
            return View("~/Views/Home/HomeWaiter/Order_panel.cshtml");
        }


        /*------------------------ WORKS END ------------------------ */
        #endregion


    }
}
