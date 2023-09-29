using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("/Views/Home/Admin/Index.cshtml", pizzas);
            }
           
        }

        public IActionResult Detail(int id)
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaFounded = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaFounded == null)
                {
                    TempData["Message"] = "Nessuna Pizza trovata";
                    return RedirectToAction("Index");
                }
                return View("/Views/Home/Admin/Detail.cshtml", pizzaFounded);
            }
        }
    }
}
