
using Models.CharacterLog;

namespace Services.Services.IServices
{
    public interface ICharacterLogArrangeService
    {
        public Task CreateAsync(string tableName);

        public Task DeleteAsync(string tableName);

        public Task InsertAsync(string tableName, List<CharacterLog> characterLogs);
    }
}
