using DentalManagementSystem.Models;

namespace DentalManagementSystem.DAL
{
    public abstract class DBContext<T> : DentalSystemDbContext
    {
        public abstract void Add(T entity);
        public abstract void Delete(long Id);
        public abstract void Update(T entity);
        public abstract T Get(long id);
        public abstract List<T> ListAll();
        public abstract List<T> ListAll(long OwnerId);
    }
}
