using Ecomm.DataAccess.Data;
using Ecomm.DataAccess.Models;
using Ecomm.DataAccess.Repository.IRepository;

namespace Ecomm.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }    

        public void Update(Category category)
        {
            db.Categories.Update(category);
        }
    }
}
