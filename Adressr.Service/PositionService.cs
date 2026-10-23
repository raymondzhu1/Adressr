using Adressr.Data.Repository.Interface;
using Adressr.Data.Model;
using Adressr.Library.Models;
using Adressr.Service.Interface;
using Adressr.Library.CustomExceptions;

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
                Username = user.Username
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

        private async Task<bool> IsPermittedGeneral(int requesterId, int companyId, int? creatorId = null, bool isEmployeeAllowed = true)
        {
            int creatorIdChecked = creatorId ?? await _companyRepository.GetCreatorIdAsync(companyId);
            if(creatorIdChecked == 0)
            {
                return false;
            }

            bool isCreator = creatorIdChecked == requesterId;

            if (isCreator)
            {
                return true;
            }
            else
            {
                bool isEmployee = await _positionRepository.IsRequesterPartOfCompanyAsync(requesterId, companyId);
                if (isEmployeeAllowed && isEmployee)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<PositionResponse>?> GetPositionsByCompanyIdAsync(int requesterId, int companyId)
        {
            //REWRITE: Code rewritten so that now instead of going through the foreach loop, I'm just doing three queries.
            // Saves on an insane number of queries that the foreach loop would run for each individual position from the positionResults. Batching the ids into one query solves this.
            // Need to check requester before doing this.

            Company? company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if (company is null)
            {
                return null;
            }
            bool isPermitted = await IsPermittedGeneral(requesterId, companyId, company.CreatorID);
            if (!isPermitted)
            {
                throw new ForbiddenException("You are not allowed to see positions for this company.", "Some name here");
            }

            List<Position> positionResults = await _positionRepository.GetPositionsByCompanyIdAsync(companyId);
            if(positionResults.Count == 0)
            {
                return [];
            }

            List<int> positionIds = positionResults.Select(position => position.PositionID).ToList();
            Dictionary<int, int> employeeCounts = await _positionRepository.GetEmployeeCountsByMultiplePositionIdsAsync(positionIds);
            Dictionary<int, int> openingCounts = await _openingRepository.GetNumberOfOpeningsByMultiplePositionIdsAsync(positionIds);

            return positionResults.Select(position => new PositionResponse
            {
                PositionID = position.PositionID,
                Title = position.Title,
                Summary = position.Summary,
                Description = position.Description,
                NumInCompany = employeeCounts.GetValueOrDefault(position.PositionID),
                NumberOfOpenings = openingCounts.GetValueOrDefault(position.PositionID)
            }).ToList();
        }

        public async Task<PositionResponse?> CreatePositionByCompanyIdAsync(int requesterId, int companyId, PositionRequest positionRequest)
        {
            //Check requester tobeadded.
            Company? company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if (company is null)
            {
                return null;
            }
            bool isPermitted = await IsPermittedGeneral(requesterId, companyId, company.CreatorID);
            if (!isPermitted)
            {
                throw new ForbiddenException("You are not allowed to create a position within this company.", "Some name here");
            }

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

        public async Task<PositionResponse?> GetPositionByPositionIdAsync(int requesterId, int companyId, int positionId)
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
                if (position is null || position.CompanyID != companyId)
                {
                    return null;
                }
                else
                {
                    //bool isCreator = company.CreatorID == requesterId;
                    //bool isEmployee = await _positionRepository.IsRequesterPartOfCompanyAsync(requesterId, companyId);
                    bool isPermitted = await IsPermittedGeneral(requesterId, companyId, company.CreatorID);
                    if (!isPermitted)
                    {
                        throw new ForbiddenException(company.Name);
                    }

                    PositionResponse toReturn = BasicToResponse(position);
                    toReturn.NumberOfOpenings = await _openingRepository.GetNumberOfOpeningsByPositionIdAsync(position.PositionID);
                    List<User> siteEmployees = await _positionRepository.GetRegisterEmployeesByPositionIdAsync(position.PositionID);
                    List<PlainTextEmployee> plainTexts = await _positionRepository.GetPlainTextEmployeesByPositionIdAsync(position.PositionID);
                    toReturn.SiteAvailableEmployees = siteEmployees.Select(ToUserResponse).ToList();
                    toReturn.PlainTextEmployees = plainTexts.Select(ToPlainTextResponse).ToList();
                    toReturn.NumInCompany = siteEmployees.Count + plainTexts.Count;

                    return toReturn;
                }
            }
        }
    }
}
