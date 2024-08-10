
using Models.CharacterLog;
using Repositories.Repositories.IRepositories;
using Services.Services.IServices;

namespace Services.Services
{
    public sealed class CharacterLogArrangeService : ICharacterLogArrangeService
    {
        private readonly ICharacterLogArrangeRepository characterLogArrangeRepository;

        private string tableName;

        public void SetTableName(string tableName)
        {
            this.tableName = tableName;
        }

        public CharacterLogArrangeService(
            ICharacterLogArrangeRepository characterLogArrangeRepository)
        {
            this.characterLogArrangeRepository = characterLogArrangeRepository;
        }

        public async Task CreateAsync()
        {
            if (string.IsNullOrWhiteSpace(this.tableName) == false)
            {
                await this.characterLogArrangeRepository.CreateAsync(this.tableName);
            }
        }

        public async Task DeleteAsync()
        {
            if (string.IsNullOrWhiteSpace(this.tableName) == false)
            {
                await this.characterLogArrangeRepository.DeleteAsync(this.tableName);
            }
        }

        public async Task InsertAsync(List<CharacterLog> characterLogs)
        {
            if (string.IsNullOrWhiteSpace(this.tableName) == false)
            {
                await this.characterLogArrangeRepository.InsertAsync(this.tableName, characterLogs);
            }
        }
    }
}
