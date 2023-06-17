using Shop.Web.Data.Repository.Interface;
using Shop.Web.Models;

namespace Shop.Web.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopDBContext context) : base(context)
        {
        }

    }
}
