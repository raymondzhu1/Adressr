using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;
using Adressr.Library.CustomExceptions;

namespace Adressr.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileInterestRepository _profileInterestRepository;
        private readonly IProfileHobbyRepository _profileHobbyRepository;
        private readonly IExperienceService _experienceService;

        public ProfileService(IProfileRepository profileRepository, IProfileInterestRepository profileInterestRepository, IProfileHobbyRepository profileHobbyRepository, IExperienceService experienceService)
        {
            _profileRepository = profileRepository;
            _profileInterestRepository = profileInterestRepository;
            _profileHobbyRepository = profileHobbyRepository;
            _experienceService = experienceService;
        }

        public async Task<ProfileResponse?> GetByUsernameAsync(string username, int? viewerUserId)
        {
            Profile? profile = await _profileRepository.GetByUsernameAsync(username);
            if(profile is null)
            {
                return null;
            }

            bool isProfileOwner = viewerUserId == profile.UserID;
            bool isPrivate = profile.User.Preference?.ProfilePrivate ?? false;
            if(isPrivate && !isProfileOwner)
            {
                return null;
            }

            List<ExperienceResponse> experiences = await _experienceService.GetExperiencesExportedByProfileIdAsync(profile.ProfileID);
            List<ProfileInterest> interests = await _profileInterestRepository.GetInterestsByProfileIdAsync(profile.ProfileID);
            List<ProfileHobby> hobbies = await _profileHobbyRepository.GetHobbiesByProfileIdAsync(profile.ProfileID);

            ProfileResponse result = new ProfileResponse
            {
                Name = profile.Name,
                Education = profile.Education,
                ProfilePicLocation = profile.ProfilePicLocation,
                Experience = experiences,
                Interests = interests.OrderByDescending(i => i.CreatedDate).Select(i => i.InterestText).ToList(),
                Hobbies = hobbies.OrderByDescending(h => h.CreatedDate).Select(h => h.HobbyText).ToList()
            };
            return result;
        }

        public async Task UpdateProfileAsync(int userId, ProfileRequest request)
        {
            Profile? profile = await _profileRepository.GetByUserIdAsync(userId);
            if(profile is null)
            {
                throw new NotFoundException(nameof(Profile), userId);
            }
            else
            {
                profile.Name = request.Name;
                profile.Education = request.Education;
                await _profileRepository.UpdateProfileAsync(profile);
            }
        }
    }
}
