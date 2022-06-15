using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // Dependency Injection (we're injecting the "connection to the db object)
        private readonly Data.ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        // GET (This is just the view/form) - No object is passed in as an object is created within the view.
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot exactly match the Display Order.");
                ModelState.AddModelError("DisplayOrder", "The Display Order cannot exactly match the Name.");
            }

            // Server side validation (Model valid? Add to Db and redirect to Category List. Model invalid? "Do nothing"). 
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET (This is just the edit view/form) - No object is passed in as an object is created within the view.
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(category => category.Id == id); // Other way of retrieving data from db using Entity Framweork Core
            // var categoryFromDb = _db.Categories.SingleOrDefault(category => category.Id == id); // Other way of retrieving data from db using Entity Framweork Core

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot exactly match the Display Order.");
                ModelState.AddModelError("DisplayOrder", "The Display Order cannot exactly match the Name.");
            }

            // Server side validation (Model valid? Add to Db and redirect to Category List. Model invalid? "Do nothing"). 
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET (This is just the delete view/form) - No object is passed in as an object is created within the view.
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(category => category.Id == id); // Other way of retrieving data from db using Entity Framweork Core
            // var categoryFromDb = _db.Categories.SingleOrDefault(category => category.Id == id); // Other way of retrieving data from db using Entity Framweork Core

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            // For the id to not be passed in as "null", a hidden field must be added to the Delete form so that the form submission can pass the value.
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
