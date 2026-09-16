namespace Adressr.Library.Interfaces
{
    public interface IRepository<T> where T : IModel
    {
        Task AddAsync(T model);
    }
}
