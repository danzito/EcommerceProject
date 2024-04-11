using EcommerceSiteRazor.Data;
using EcommerceSiteRazor.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceSiteRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                db.Categories.Add(Category);
                db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("index");
            }
            return NotFound();
        }

        public IActionResult OnPut()
        {
            var category = db.Categories.First(p => p.Id == Category.Id);

            if (category != null)
            {
                db.Categories.Update(category);
                db.SaveChanges();
                return RedirectToPage("index");
            }
            return NotFound();
        }

    }
}
