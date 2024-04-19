using Ecomm.DataAccess.Data;
using Ecomm.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public ICategoryRepository Category { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            this.db = db;
            Category = new CategoryRepository(db);
        }
 

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
