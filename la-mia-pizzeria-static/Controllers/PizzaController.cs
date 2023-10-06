using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Logger;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
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

            List<Pizza> pizzas = _db.Pizzas.Include(pizza => pizza.Ingredients).Include(pizza => pizza.Category).ToList();
            return View("/Views/Admin/Pizza/Index.cshtml", pizzas);

        }

        public IActionResult Detail(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di dettaglii della pizza {id}");

            Pizza? pizzaFounded = _db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).Include(pizza => pizza.Ingredients).FirstOrDefault();
            if(pizzaFounded == null)
            {
                TempData["Message"] = "Nessuna Pizza trovata";
                return RedirectToAction("Index");
            }
            return View("/Views/Admin/Pizza/Detail.cshtml", pizzaFounded);

        }

        [HttpGet]
        [Authorize(Roles ="ADMIN")]
        public IActionResult Create()
        {
            _logger.WriteLog("Entrato nella creazione di una nuova pizza");
            using(_db)
            {
                List<Category> categories = _db.Categories.ToList();

                PizzaFormModel model = new PizzaFormModel() { Pizza = new Pizza(), Categories = categories };

                List<Ingredient> databaseIngredients = _db.Ingredients.ToList();
                List<SelectListItem> listIngredients = new List<SelectListItem>();

                foreach(Ingredient ingredient in databaseIngredients)
                {
                    listIngredients.Add(new SelectListItem()
                    {
                        Text = ingredient.Name,
                        Value = ingredient.Id.ToString(),
                    });
                }

                model.Ingredients = listIngredients;

                return View("/Views/Admin/Pizza/Create.cshtml", model);
            }


        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                using(_db)
                {
                    List<Category> categories = _db.Categories.ToList();
                    List<Ingredient> dbIngredients = _db.Ingredients.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();
                    foreach(Ingredient ingredient in dbIngredients)
                    {
                        listIngredients.Add(new SelectListItem()
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                        });
                    }
                    data.Categories = categories;
                    data.Ingredients = listIngredients;

                    return View("/Views/Admin/Pizza/Create.cshtml", data);
                }

            }
            using(_db)
            {
               
                if(data.SelectedIngredients != null)
                {
                    data.Pizza.Ingredients = new List<Ingredient>();
                    foreach(string selectedIngredientString in data.SelectedIngredients)
                    {
                        int selectedIngredientId = int.Parse(selectedIngredientString);
                        Ingredient ingredient = _db.Ingredients.Where(ingredient => ingredient.Id == selectedIngredientId).FirstOrDefault();
                        data.Pizza.Ingredients.Add(ingredient);
                    }
                }
                
                
                _db.Pizzas.Add(data.Pizza);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles ="ADMIN")]
        public IActionResult Update(int id)
        {
            _logger.WriteLog($"Entrato nella pagina di modifica della pizza {id}");

            Pizza? pizza = _db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Ingredients).FirstOrDefault();


            if(pizza != null)
            {
                List<Category> categories = _db.Categories.ToList();
                PizzaFormModel pizzaModel = new PizzaFormModel() { Pizza = pizza, Categories = categories };
                List<Ingredient> dbIngredients = _db.Ingredients.ToList();
                List<SelectListItem> listIngredients = new List<SelectListItem>();

                foreach(Ingredient ingredient in dbIngredients)
                {
                    listIngredients.Add(new SelectListItem()
                    {
                        Text = ingredient.Name,
                        Value = ingredient.Id.ToString(),
                        Selected = pizza.Ingredients.Any(selected => selected.Id == ingredient.Id)
                    });
                }

                pizzaModel.Ingredients = listIngredients;
                

                return View("/Views/Admin/Pizza/Update.cshtml", pizzaModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                using(_db)
                {
                    List<Category> categories = _db.Categories.ToList();
                    data.Categories = categories;

                    List<Ingredient> dbIngredients = _db.Ingredients.ToList();
                    List<SelectListItem> listSelectIngredient = new List<SelectListItem>();
                   foreach(Ingredient ingredient in dbIngredients)
                    {
                        listSelectIngredient.Add(new SelectListItem()
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                        });
                    }

                    data.Ingredients = listSelectIngredient;
                    return View("/Views/Admin/Pizza/Update.cshtml", data);
                }
                
               
            }


            Pizza? pizzaToUpdate = _db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Ingredients).FirstOrDefault();

            if(pizzaToUpdate != null)
            {
                pizzaToUpdate.Ingredients.Clear();
                if(data.SelectedIngredients != null)
                {
                   foreach(string selectedIngredientString in data.SelectedIngredients)
                    {
                        int selectedIngredientId = int.Parse(selectedIngredientString);
                        Ingredient newIngredient = _db.Ingredients.Where(ingredient => ingredient.Id == selectedIngredientId).FirstOrDefault();
                        pizzaToUpdate.Ingredients.Add(newIngredient);
                    }
                   
                }

                pizzaToUpdate.Name = data.Pizza.Name;
                pizzaToUpdate.Description = data.Pizza.Description;
                pizzaToUpdate.Image = data.Pizza.Image;
                pizzaToUpdate.Price = data.Pizza.Price;
                pizzaToUpdate.CategoryId = data.Pizza.CategoryId;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
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
