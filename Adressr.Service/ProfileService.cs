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

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
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

            ProfileResponse result = new ProfileResponse
            {
                Name = profile.Name,
                Education = profile.Education,
                ProfilePicLocation = profile.ProfilePicLocation
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
