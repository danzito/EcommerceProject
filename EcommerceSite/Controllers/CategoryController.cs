using EcommerceSite.Data;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = db.Categories.ToList();


            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category? category = db.Categories.Find(Id);

            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
