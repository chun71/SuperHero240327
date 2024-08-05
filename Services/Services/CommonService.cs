
using Models.CharacterLog;
using Repositories.Repositories;

namespace Services.Services
{
    public sealed class CommonService
    {
        private readonly CommonRepository commonRepository;

        public CommonService(
            CommonRepository commonRepository)
        {
            this.commonRepository = commonRepository;
        }

        public async Task CreateAsync(string tableName)
        {
            await this.commonRepository.CreateAsync(tableName);
        }

        public async Task DeleteAsync(string tableName) 
        {
            await this.commonRepository.DeleteAsync(tableName);
        }

        public async Task InsertAsync(string tableName, List<CharacterLog> characterLogs) 
        {
            await this.commonRepository.InsertAsync(tableName, characterLogs);
        }
    }
}
