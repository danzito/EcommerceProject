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
            db.Update(product);
        }
    }
}
