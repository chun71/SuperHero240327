
using Models.CharacterLog;

namespace Repositories.Repositories.IRepositories
{
    public interface ICharacterLogArrangeRepository
    {
        public Task CreateAsync(string tableName);

        public Task DeleteAsync(string tableName);

        public Task InsertAsync(string tableName, List<CharacterLog> characterLogs);
    }
}
