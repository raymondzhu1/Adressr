using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;
using Adressr.Data.Model;

namespace Adressr.Service
{
    public class CompanyService : ICompanyService
    {
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

        public async Task<CompanyResponse> CreateAsync(int creatorId, CompanyRequest companyRequest)
        {
            Company company = new Company
            {
                Name = companyRequest.Name,
                Description = companyRequest.Description,
                CreatorID = creatorId
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
            //List<CompanyResponse> ToReturn = new List<CompanyResponse>();
            //foreach (Company company in companyList)
            //{
            //    ToReturn.Add(ToResponse(company));
            //}
            //return ToReturn;
        }
    }
}
