using System.Linq.Expressions;

namespace Shop.Web.Data.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T GetFirstOrDefault(Expression<Func<T,bool>> filter);
        void Remove(T Entity);
        void RemoveRange(List<T> Entities);
        void Update(T Entity);
    }
}
