using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Logger;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        private ICustomLogger _logger;
        private PizzeriaContext _db;

        public PizzaController(ICustomLogger logger, PizzeriaContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            _logger.WriteLog("Entrato nella index degli admin");
            using(_db)
            {
                List<Pizza> pizzas = _db.Pizzas.ToList<Pizza>();
                return View("/Views/Home/Admin/Index.cshtml", pizzas);
            }
           
        }

        public IActionResult Detail(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di dettaglii della pizza {id}");
            using(_db)
            {
                Pizza? pizzaFounded = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaFounded == null)
                {
                    TempData["Message"] = "Nessuna Pizza trovata";
                    return RedirectToAction("Index");
                }
                return View("/Views/Home/Admin/Detail.cshtml", pizzaFounded);
            }
        }

        public IActionResult UserIndex()
        {
            _logger.WriteLog("Entrato nella pagina Utente");
            using(_db)
            {
                List<Pizza> pizzas = _db.Pizzas.ToList<Pizza>();
                return View("/Views/Home/User/UserIndex.cshtml", pizzas);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.WriteLog("Entrato nella creazione di una nuova pizza");
            return View("/Views/Home/Admin/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(!ModelState.IsValid)
            {
                return View("/Views/Home/Admin/Create.cshtml", pizza);
            }

            using( _db)
            {
                _db.Pizzas.Add(pizza);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di modifica della pizza {id}");
            using(_db)
            {
                Pizza? pizza = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                
                if(pizza != null)
                {
                    return View("/Views/Home/Admin/Update.cshtml", pizza);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public IActionResult Update(int id, Pizza updatePizza) 
        {
            if(!ModelState.IsValid)
            {
                return View("/Views/Home/Admin/Update.cshtml", updatePizza);
            }

            using(_db)
            {
                Pizza? pizza = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizza != null)
                {
                    pizza.Name = updatePizza.Name;
                    pizza.Description = updatePizza.Description;
                    pizza.Image = updatePizza.Image;
                    pizza.Price = updatePizza.Price;

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _logger.WriteLog($"Cancellato la pizza {id}");
            using(_db)
            {
                Pizza? deletePizza = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(deletePizza != null) 
                {
                    _db.Pizzas.Remove(deletePizza);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
