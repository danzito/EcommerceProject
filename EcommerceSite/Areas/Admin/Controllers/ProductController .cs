using Ecomm.DataAccess.Repository.IRepository;
using Ecomm.Models;
using Ecomm.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHost;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();



            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            //ViewBag.CategoryList = CategoryList;
            ProductViewModel viewModel = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                //for create
                return View(viewModel);
            }
            else
            {
                //for update
                viewModel.Product = unitOfWork.Product.Get(u => u.Id == id);
                return View(viewModel);
            }


        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productsView, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHost.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productsView.Product.ImageUrl))
                    {
                        //delete old image
                        var oldimagePath = Path.Combine(wwwRootPath, productsView.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimagePath))
                        {
                            System.IO.File.Delete(oldimagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productsView.Product.ImageUrl = @"\images\product\" + fileName;
                }

                string operationMessage;
                if (productsView.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productsView.Product);
                    operationMessage = "Product created successfully";
                }
                else
                {
                    unitOfWork.Product.Update(productsView.Product);
                    operationMessage = "Product updated successfully";
                }

                unitOfWork.Save();
                TempData["success"] = operationMessage;
                return RedirectToAction("Index");
            }
            return View();

        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }

        //    Product? product = unitOfWork.Product.Get(c => c.Id == id);
        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        unitOfWork.Product.Update(product);
        //        unitOfWork.Save();
        //        TempData["success"] = "Product updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }

        //    Product? product = unitOfWork.Product.Get(c => c.Id == id);
        //    return View(product);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult DeletePost(int? Id)
        //{
        //    Product? product = unitOfWork.Product.Get(c => c.Id == Id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    unitOfWork.Product.Remove(product);
        //    unitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index");

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product? product = unitOfWork.Product.Get(c => c.Id == id);

            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }

            var oldimagePath = Path.Combine(webHost.WebRootPath, product.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldimagePath))
            {
                System.IO.File.Delete(oldimagePath);
            }

            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
            //TempData["success"] = "Product deleted successfully";

            return Json(new { success = true, message = "Message Deleted Successfully" });
        }

        #endregion
    }
}
