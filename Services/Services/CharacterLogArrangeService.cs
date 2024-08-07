
using Models.CharacterLog;
using Repositories.Repositories.IRepositories;
using Services.Services.IServices;

namespace Services.Services
{
    public sealed class CharacterLogArrangeService : ICharacterLogArrangeService
    {
        private readonly ICharacterLogArrangeRepository characterLogArrangeRepository;

        public CharacterLogArrangeService(
            ICharacterLogArrangeRepository characterLogArrangeRepository)
        {
            this.characterLogArrangeRepository = characterLogArrangeRepository;
        }

        public async Task CreateAsync(string tableName)
        {
            await this.characterLogArrangeRepository.CreateAsync(tableName);
        }

        public async Task DeleteAsync(string tableName) 
        {
            await this.characterLogArrangeRepository.DeleteAsync(tableName);
        }

        public async Task InsertAsync(string tableName, List<CharacterLog> characterLogs) 
        {
            await this.characterLogArrangeRepository.InsertAsync(tableName, characterLogs);
        }
    }
}
