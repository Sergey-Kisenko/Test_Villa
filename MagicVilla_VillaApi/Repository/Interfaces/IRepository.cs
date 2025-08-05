namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IRepository <T> where T : class
    {
        public Task<T?> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task Create(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task Save();
    }
}
