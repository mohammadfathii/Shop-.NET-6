using Shop.Web.Data.Repository.Interface;
using Shop.Web.Models;

namespace Shop.Web.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopDBContext context) : base(context)
        {
        }

    }
}
