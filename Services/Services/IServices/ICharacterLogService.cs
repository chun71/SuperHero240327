
using Models.CharacterLog;

namespace Services.Services.IServices
{
    public  interface ICharacterLogService
    {
        public Task<List<CharacterLog>> QueryAsync();

        public Task DeleteAsync(DateTime maxTime);
    }
}
