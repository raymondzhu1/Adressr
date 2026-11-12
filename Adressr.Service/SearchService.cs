using Adressr.Data.Repository.Interface;
using Adressr.Data.Repository.Model;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class SearchService : ISearchService
    {
        private const int MinTermLength = 1;
        private const int MaxResults = 8;

        private readonly IProfileRepository _profileRepository;
        private readonly ICompanyRepository _companyRepository;

        public SearchService(IProfileRepository profileRepository, ICompanyRepository companyRepository)
        {
            _profileRepository = profileRepository;
            _companyRepository = companyRepository;
        }

        private static int MatchRank(string term, params string[] fields)
        {
            return fields.Min(field => field.Equals(term, StringComparison.OrdinalIgnoreCase) ? 0 : field.StartsWith(term, StringComparison.OrdinalIgnoreCase) ? 1 : 2);
        }

        public async Task<List<SearchResultResponse>> SearchAsync(string searchTerm)
        {
            string term = searchTerm.Trim();
            if(term.Length < MinTermLength)
            {
                return [];
            }

            List<UserSearchHit> usersFound = await _profileRepository.SearchUsersAsync(term, MaxResults);
            List<CompanySearchHit> companiesFound = await _companyRepository.SearchCompanyAsync(term, MaxResults);

            IEnumerable<(int Rank, SearchResultResponse Result)> userHits = usersFound.Select(user => (
                Rank: MatchRank(term, user.Name),
                Result: new SearchResultResponse
                {
                    SearchResultType = SearchResultTypes.User,
                    Key = user.Username,
                    Name = user.Name,
                    ImageLocation = user.ProfilePicLocation
                }));
            IEnumerable<(int Rank, SearchResultResponse Result)> companyHits = companiesFound.Select(company => (
                Rank: MatchRank(term, company.Name),
                Result: new SearchResultResponse
                {
                    SearchResultType= SearchResultTypes.Company,
                    Key = company.CompanyID.ToString(),
                    Name = company.Name,
                    ImageLocation = company.CompanyIconImageLocation
                }));

            return userHits.Concat(companyHits).OrderBy(hit => hit.Rank).ThenBy(hit => hit.Result.Name, StringComparer.OrdinalIgnoreCase).Take(MaxResults).Select(hit => hit.Result).ToList();
        }
    }
}
