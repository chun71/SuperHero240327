
using Models.CharacterLog;

namespace Repositories.Repositories.IRepositories
{
    public interface ICharacterLogRepository
    {
        public  Task<List<CharacterLog>> QueryAsync();

        public  Task DeleteAsync(DateTime maxTime);
    }
}
