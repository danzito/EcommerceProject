using Ecomm.DataAccess.Data;
using Ecomm.DataAccess.Repository.IRepository;
using Ecomm.Models;

namespace Ecomm.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            this.db = db;
        }
        public void Update(Product product)
        {
            var objFromDb = db.Products.FirstOrDefault(u => u.Id == product.Id);

            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100= product.Price100;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;

                if (product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }

            }


            //db.Update(product);
        }
    }
}
