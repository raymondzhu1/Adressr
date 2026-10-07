using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;
using Adressr.Library.CustomExceptions;

namespace Adressr.Service
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IRepositorySaver _repositorySaver;

        public PreferenceService(IPreferenceRepository preferenceRepository, IRepositorySaver repositorySaver)
        {
            _preferenceRepository = preferenceRepository;
            _repositorySaver = repositorySaver;
        }

        public async Task<PreferenceDTO?> GetByUserIdAsync(int userId)
        {
            Preference? preference = await _preferenceRepository.GetByUserIdAsync(userId);
            if(preference is null)
            {
                return null;
            }

            PreferenceDTO result = new PreferenceDTO
            {
                DarkMode = preference.DarkMode,
                ProfilePrivate = preference.ProfilePrivate,
            };

            return result;
        }

        public async Task UpdatePreferenceAsync(int userId, PreferenceDTO preferenceDTO)
        {
            Preference? preference = await _preferenceRepository.GetByUserIdAsync(userId);
            if(preference is null)
            {
                throw new NotFoundException(nameof(Preference), userId);
            }

            preference.DarkMode = preferenceDTO.DarkMode;
            preference.ProfilePrivate = preferenceDTO.ProfilePrivate;

            //No UpdateAsync since the preference variable here is tracked due to the GetBy here in this method. Just need SaveChanges.

            await _repositorySaver.SaveChangesAsync();
        }
    }
}
