using Adressr.Data.Repository.Interface;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class OpeningService : IOpeningService
    {
        private readonly IOpeningRepository _openingRepository;

        public OpeningService(IOpeningRepository openingRepository)
        {
            _openingRepository = openingRepository;
        }


    }
}
