using Adressr.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        
    }
}
