using Adressr.Data.Repository.Interface;
using Adressr.Data.Model;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IOpeningRepository _openingRepository;
        private readonly ICompanyRepository _companyRepository;

        public PositionService(IPositionRepository positionRepository, IOpeningRepository openingRepository, ICompanyRepository companyRepository)
        {
            _positionRepository = positionRepository;
            _openingRepository = openingRepository;
            _companyRepository = companyRepository;
        }

        private static PositionResponse BasicToResponse(Position position)
        {
            return new PositionResponse
            {
                PositionID = position.PositionID,
                Description = position.Description,
                Title = position.Title,
                Summary = position.Summary
            };
        }

        private static UserDTO ToUserResponse(User user)
        {
            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email
            };
        }

        private static PlainTextEmployeeResponse ToPlainTextResponse(PlainTextEmployee plain)
        {
            return new PlainTextEmployeeResponse
            {
                FullName = plain.FullName,
                Email = plain.Email,
                HireDate = plain.HireDate
            };
        }

        public async Task<List<PositionResponse>> GetPositionsByCompanyIdAsync(int companyId)
        {
            //REWRITE: Rewrite to be more efficient, especially that dang foreach loop.
            //Description and summary usually aren't both needed at the same time, take one or the other.

            List<PositionResponse> toReturn = new List<PositionResponse>();
            List<Position> positionResults = await _positionRepository.GetPositionsByCompanyIdAsync(companyId);
            foreach (Position pos in positionResults)
            {
                toReturn.Add(new PositionResponse
                {
                    PositionID = pos.PositionID,
                    Description = pos.Description,
                    Title = pos.Title,
                    Summary = pos.Summary,
                    NumInCompany = await _positionRepository.GetNumberOfEmployeesForPositionByPositionIdAsync(pos.PositionID),
                    NumberOfOpenings = await _openingRepository.GetNumberOfOpeningsByPositionIdAsync(pos.PositionID)
                });
            }
            return toReturn;
        }

        public async Task<PositionResponse> CreatePositionByCompanyIdAsync(int companyId, PositionRequest positionRequest)
        {
            int summaryMaxLength = 200; 

            Position toAdd = new Position
            {
                Title = positionRequest.Title,
                Description = positionRequest.Description,
                Summary = positionRequest.Description[..Math.Min(summaryMaxLength, positionRequest.Description.Length)]
            };

            await _positionRepository.AddPositionToCompanyAsync(toAdd);

            return BasicToResponse(toAdd);
        }

        public async Task<PositionResponse?> GetPositionByPositionIdAsync(int companyId, int positionId)
        {
            //REWRITE: Has a similar issue as GetPositions. Rewrite this later.
            //First need to verify the companyId for this positionId is legit. After that can use the position ID.

            Company? company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if(company is null)
            {
                return null;
            }
            else
            {
                Position? position = await _positionRepository.GetPositionByPositionIdAsync(positionId);
                if (position is null)
                {
                    return null;
                }
                else
                {
                    PositionResponse toReturn = BasicToResponse(position);
                    //toReturn.NumInCompany = await _positionRepository.GetNumberOfEmployeesForPositionByPositionIdAsync(position.PositionID);
                    toReturn.NumberOfOpenings = await _openingRepository.GetNumberOfOpeningsByPositionIdAsync(position.PositionID);
                    //toReturn.SiteAvailableEmployees = ToUserResponse(await _positionRepository.GetRegisterEmployeesByPositionIdAsync(position.PositionID));

                    List<User> siteEmployees = await _positionRepository.GetRegisterEmployeesByPositionIdAsync(position.PositionID);
                    List<UserDTO> siteResponseEmployees = new List<UserDTO>();
                    foreach (User user in siteEmployees)
                    {
                        siteResponseEmployees.Add(ToUserResponse(user));
                    }
                    toReturn.SiteAvailableEmployees = siteResponseEmployees;

                    List<PlainTextEmployeeResponse> plainies = new List<PlainTextEmployeeResponse>();
                    List<PlainTextEmployee> plainTexts = await _positionRepository.GetPlainTextEmployeesByPositionIdAsync(position.PositionID);
                    foreach (PlainTextEmployee plains in plainTexts)
                    {
                        plainies.Add(ToPlainTextResponse(plains));
                    }
                    toReturn.PlainTextEmployees = plainies;

                    toReturn.NumInCompany = siteResponseEmployees.Count + plainies.Count;

                    return toReturn;
                }
            }
        }
    }
}
