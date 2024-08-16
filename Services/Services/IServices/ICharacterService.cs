
using Models.Character;

namespace Services.Services.IServices
{
    public interface ICharacterService
    {
        public Task<List<CharacterView>> QueryAsync();

        public Task CreateAsync(Character character);

        public Task UpdateAsync(Character character);

        public Task DeleteAsync(long id);
    }
}
