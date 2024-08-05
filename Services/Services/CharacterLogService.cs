
using Models.CharacterLog;
using Repositories.Repositories;

namespace Services.Services
{
    public class CharacterLogService
    {
        private readonly CharacterLogRepository characterLogRepository;

        public CharacterLogService(
            CharacterLogRepository characterLogRepository
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
