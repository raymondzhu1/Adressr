using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface ISearchService
    {
        Task<List<SearchResultResponse>> SearchAsync(string searchTerm);
    }
}
