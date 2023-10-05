using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoryController : Controller
    {
        private PizzeriaContext _db;

        public CategoryController(PizzeriaContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.Include(categories => categories.pizzas).ToList();

            List<(Category category, int id)> data = categories.Select(x => (x, x.pizzas?.Count ?? 0)).ToList();

            return View("/Views/Admin/Category/Index.cshtml", data);
        }

        public IActionResult Details(int id)
        {
            using(_db)
            {
                Category? category = _db.Categories.Where(x => x.Id == id).Include(category => category.pizzas).FirstOrDefault();

                if(category == null)
                {
                    return RedirectToAction("Index");
                }
                return View("/Views/Admin/Category/Details.cshtml", category);
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("./Views/Admin/Category/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View("/Views/Admin/Category/Create", category);
            }

            using(_db)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using(_db)
            {
                Category? foundedCategory = _db.Categories.Where(x => x.Id == id).FirstOrDefault();

                if(foundedCategory == null)
                {
                    return RedirectToAction("Index");
                }

                return View("/Views/Admin/Category/Update.cshtml", foundedCategory);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category category)
        {
            if(!ModelState.IsValid)
            {
                return View("/Views/Admin/Category/Update.cshtml", category);
            }

            using(_db)
            {
                Category? editCatgegory = _db.Categories.Find(id);

                if(editCatgegory == null)
                {
                    return RedirectToAction("Index");
                }

                editCatgegory.Name = category.Name;
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using(_db)
            {
                Category? category = _db.Categories.Where(x => x.Id == id).Include(x => x.pizzas).FirstOrDefault();

                if(category == null)
                {
                    return RedirectToAction("Index");
                }

                _db.Categories.Remove(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
