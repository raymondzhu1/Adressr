using Adressr.Library.Models;
using Adressr.Service.Interface;
using AdressrNET.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IPositionService _positionService;

        public CompanyController(ICompanyService companyService, IPositionService positionService)
        {
            _companyService = companyService;
            _positionService = positionService;
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
            if (response is null)
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

        [HttpGet("{companyId:int}/positions")]
        public async Task<ActionResult<List<PositionResponse>>> GetPositions(int companyId)
        {
            List<PositionResponse>? response = await _positionService.GetPositionsByCompanyIdAsync(User.GetUserId(), companyId);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return response;
            }
        }

        [HttpGet("{companyId:int}/positions/{positionId:int}")]
        public async Task<ActionResult<PositionResponse>> GetPosition(int companyId, int positionId)
        {
            PositionResponse? response = await _positionService.GetPositionByPositionIdAsync(User.GetUserId(), companyId, positionId);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return response;
            }
        }

        [HttpPost("{companyId:int}/positions")]
        public async Task<ActionResult<PositionResponse>> CreatePosition(int companyId, PositionRequest positionRequest)
        {
            PositionResponse? response = await _positionService.CreatePositionByCompanyIdAsync(User.GetUserId(), companyId, positionRequest);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return CreatedAtAction(nameof(GetPosition), new { companyId, positionId = response.PositionID }, response);
            }
        }
    }
}
