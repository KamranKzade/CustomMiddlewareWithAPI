namespace CustomMiddlewareWithAPI.Repositories.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
