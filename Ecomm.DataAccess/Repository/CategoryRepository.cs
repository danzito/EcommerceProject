using Ecomm.DataAccess.Data;
using Ecomm.DataAccess.Models;
using Ecomm.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
