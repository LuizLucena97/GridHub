
namespace GridHub.Repository.Interface
{
    public interface IRepository<T>
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
    }
}
