
using Models.CharacterLog;

using Repositories.Repositories;
using Repositories.Repositories.IRepositories;
using Services.Services.IServices;

namespace Services.Services
{
    public class CharacterLogService : ICharacterLogService
    {
        private readonly ICharacterLogRepository characterLogRepository;

        public CharacterLogService(
            ICharacterLogRepository characterLogRepository
            )
        {
            this.characterLogRepository = characterLogRepository;
        }

        public async Task<List<CharacterLog>> QueryAsync()
        {
            return await this.characterLogRepository.QueryAsync();
        }

        public async Task DeleteAsync(DateTime maxTime)
        {
            await this.characterLogRepository.DeleteAsync(maxTime);
        }
    }
}
