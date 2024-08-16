
using Models.Character;

namespace Repositories.Repositories.IRepositories
{
    public interface ICharacterRepository
    {
        public Task<List<Character>> QueryAsync();

        public Task<Character> QueryFirstOrDefaultAsync(DateTime createTime);

        public  Task<Character> QueryFirstOrDefaultAsync(long id);

        public Task CreateAsync(Character character);

        public Task UpdateAsync(Character character);

        public Task DeleteAsync(long id);
    }
}
