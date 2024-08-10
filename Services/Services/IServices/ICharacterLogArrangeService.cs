
using Models.CharacterLog;

namespace Services.Services.IServices
{
    public interface ICharacterLogArrangeService
    {
        public void SetTableName(string tableName);

        public Task CreateAsync();

        public Task DeleteAsync();

        public Task InsertAsync(List<CharacterLog> characterLogs);
    }
}
