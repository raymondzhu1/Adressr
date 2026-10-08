using Adressr.Library.Models;
using Adressr.Service.Interface;
using AdressrNET.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<ActionResult<CompanyResponse>> CreateCompany(CompanyRequest companyRequest)
        {
            CompanyResponse response = await _companyService.CreateAsync(User.GetUserId(), companyRequest);
            return CreatedAtAction(nameof(GetCompanyByCompanyId), new { companyId = response.CompanyID }, response);
        }

        [HttpGet("{companyId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CompanyResponse>> GetCompanyByCompanyId(int companyId)
        {
            CompanyResponse? response = await _companyService.GetByIdAsync(companyId);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return response;
            }
        }

        [HttpGet("icreated")]
        public async Task<ActionResult<List<CompanyResponse>>> GetMyCompanies()
        {
            List<CompanyResponse> response = await _companyService.GetByCreatorIdAsync(User.GetUserId());
            return response;
        }
    }
}
