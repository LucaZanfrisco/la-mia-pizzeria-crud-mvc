﻿using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Logger;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            List<Pizza> pizzas = _db.Pizzas.ToList<Pizza>();
            return View("/Views/Home/Admin/Index.cshtml", pizzas);

        }

        public IActionResult Detail(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di dettaglii della pizza {id}");

            Pizza? pizzaFounded = _db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();
            if(pizzaFounded == null)
            {
                TempData["Message"] = "Nessuna Pizza trovata";
                return RedirectToAction("Index");
            }
            return View("/Views/Home/Admin/Detail.cshtml", pizzaFounded);

        }

        public IActionResult UserIndex()
        {
            _logger.WriteLog("Entrato nella pagina Utente");

            List<Pizza> pizzas = _db.Pizzas.ToList<Pizza>();
            return View("/Views/Home/User/UserIndex.cshtml", pizzas);

        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.WriteLog("Entrato nella creazione di una nuova pizza");
            List<Category> categories = _db.Categories.ToList<Category>();
            PizzaFormModel model = new PizzaFormModel() { Pizza = new Pizza(), Categories = categories};
            return View("/Views/Home/Admin/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                List<Category> categories = _db.Categories.ToList<Category>();
                data.Categories = categories;
                return View("/Views/Home/Admin/Create.cshtml", data);
            }

            _db.Pizzas.Add(data.Pizza);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di modifica della pizza {id}");

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

        [HttpPost]
        public IActionResult Update(int id, Pizza updatePizza)
        {
            if(!ModelState.IsValid)
            {
                return View("/Views/Home/Admin/Update.cshtml", updatePizza);
            }


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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _logger.WriteLog($"Cancellato la pizza {id}");

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
