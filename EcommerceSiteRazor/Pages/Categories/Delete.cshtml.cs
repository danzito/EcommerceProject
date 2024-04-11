using EcommerceSiteRazor.Data;
using EcommerceSiteRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceSiteRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {

            var category = db.Categories.Find(Category.Id);
            if (category == null)
            {
                return NotFound();
            }


            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("index");

        }
    }
}
