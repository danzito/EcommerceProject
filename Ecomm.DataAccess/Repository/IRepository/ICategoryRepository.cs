using Ecomm.DataAccess.Models;

namespace Ecomm.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
