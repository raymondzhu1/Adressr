using Adressr.Library.Models;
using Adressr.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdressrNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SearchResultResponse>>> Search([FromQuery, MinLength(1), MaxLength(50)] string query)
        {
            List<SearchResultResponse> results = await _searchService.SearchAsync(query);
            return results;
        }
    }
}
