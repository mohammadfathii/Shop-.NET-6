using Shop.Web.Data.Repository.Interface;
using Shop.Web.Models;

namespace Shop.Web.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopDBContext context) : base(context)
        {
        }



    }
}
