namespace Adressr.Data.Repository.Interface
{
    public interface IRepositorySaver
    {
        Task<int> SaveChangesAsync();
    }
}
