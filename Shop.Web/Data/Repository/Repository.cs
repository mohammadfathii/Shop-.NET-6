using Shop.Web.Data.Repository.Interface;

namespace Shop.Web.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ShopDBContext Context { get; set; }
        public Repository(ShopDBContext context)
        {
            Context = context;
        }
        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().FirstOrDefault(filter);
        }

        public void Remove(T Entity)
        {
            Context.Set<T>().Remove(Entity);
        }

        public void RemoveRange(List<T> Entities)
        {
            Context.Set<T>().RemoveRange(Entities);
        }

        public void Update(T Entity)
        {
            Context.Set<T>().Update(Entity);
        }
    }
}
