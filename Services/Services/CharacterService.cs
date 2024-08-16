
using Enums.Enums;
using Models.Character;
using Models.CharacterLog;
using Repositories.Repositories.IRepositories;
using Services.Services.IServices;

namespace Services.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository characterRepository;

        private readonly ICharacterLogRepository characterLogRepository;

        public CharacterService
            (
            ICharacterRepository characterRepository,
            ICharacterLogRepository characterLogRepository
            )
        {
            this.characterRepository = characterRepository;
            this.characterLogRepository = characterLogRepository;
        }


        public async Task<List<CharacterView>> QueryAsync()
        {
            var characters = await this.characterRepository.QueryAsync();

            var characterViews = new List<CharacterView>();

            foreach (var character in characters)
            {
                var characterView = new CharacterView
                {
                    ID = character.ID,
                    Name = character.Name,
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    Place = character.Place
                };

                characterViews.Add(characterView);
            }

            return characterViews;
        }


        public async Task CreateAsync(Character character)
        {
            character.CreateTime = DateTime.Now;

            await this.characterRepository.CreateAsync(character);

            var result = await this.characterRepository.QueryFirstOrDefaultAsync(character.CreateTime);

            var characterLog = new CharacterLog()
            {
                CharacterID = result.ID,
                Name = result.Name,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Place = result.Place,
                Action = ActionType.Create,
                CreateTime = result.CreateTime
            };

            await this.characterLogRepository.CreateAsync(characterLog);
        }


        public async Task UpdateAsync(Character character)
        {
            character.UpdateTime = DateTime.Now;

            await this.characterRepository.UpdateAsync(character);

            var result = await this.characterRepository.QueryFirstOrDefaultAsync(character.CreateTime);

            var characterLog = new CharacterLog()
            {
                CharacterID = result.ID,
                Name = result.Name,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Place = result.Place,
                Action = ActionType.Update,
                CreateTime = result.UpdateTime.HasValue ? result.UpdateTime.Value : DateTime.Now
            };

            await this.characterLogRepository.CreateAsync(characterLog);
        }


        public async Task DeleteAsync(long id)
        {
            var result = await this.characterRepository.QueryFirstOrDefaultAsync(id);

            var characterLog = new CharacterLog()
            {
                CharacterID = result.ID,
                Name = result.Name,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Place = result.Place,
                Action = ActionType.Delete,
                CreateTime = DateTime.Now
            };

            await this.characterLogRepository.CreateAsync(characterLog);

            await this.characterRepository.DeleteAsync(id);
        }
    }
}
