using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;
using Adressr.Data.Model;

namespace Adressr.Service
{
    public class CompanyService : ICompanyService
    {
        private const string DefaultCompanyLogoLocation = "/some/place/here/defaultCompanyLogo_small.png";

        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        private static CompanyResponse ToResponse(Company company)
        {
            return new CompanyResponse
            {
                CompanyID = company.CompanyID,
                Name = company.Name,
                Description = company.Description
            };
        }

        private static DashboardCompanyResponse ToDashboardResponse(Company company)
        {
            return new DashboardCompanyResponse
            {
                CompanyID = company.CompanyID,
                Name = company.Name,
                CompanyIconImageLocation = company.CompanyIconImageLocation
            };
        }

        public async Task<CompanyResponse> CreateAsync(int creatorId, CompanyRequest companyRequest)
        {
            Company company = new Company
            {
                Name = companyRequest.Name,
                Description = companyRequest.Description,
                CreatorID = creatorId,
                CompanyIconImageLocation = DefaultCompanyLogoLocation
            };

            await _companyRepository.AddAsync(company);

            return ToResponse(company);
        }

        public async Task<CompanyResponse?> GetByIdAsync(int companyId)
        {
            Company? company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if(company is null)
            {
                return null;
            }
            else
            {
                return ToResponse(company);
            }
        }

        public async Task<List<CompanyResponse>> GetByCreatorIdAsync(int creatorId)
        {
            List<Company> companyList = await _companyRepository.GetByCreatorIdAsync(creatorId);
            return companyList.Select(ToResponse).ToList();
        }

        public async Task<List<DashboardCompanyResponse>> GetDashboardCompaniesByCreatorIdAsync(int creatorId)
        {
            List<Company> companyList = await _companyRepository.GetByCreatorIdAsync(creatorId);
            return companyList.Select(ToDashboardResponse).ToList();
        }
    }
}
