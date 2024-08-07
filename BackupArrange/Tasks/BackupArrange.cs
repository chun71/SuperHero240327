
using System.Globalization;
using Repositories.Repositories;
using Services.Services;
using Services.Services.IServices;

namespace BackupArrange.Tasks
{
    /// <summary>
    /// 備份資料整理
    /// </summary>
    public sealed class BackupArrange
    {
        /// <summary>
        /// BackupArrange 實作物件
        /// </summary>
        public static BackupArrange Obj
        {
            get
            {
                return new BackupArrange(
                    new CharacterLogArrangeService(new CharacterLogArrangeRepository()),
                    new CharacterLogService(new CharacterLogRepository())
                    );
            }
        }

        private readonly ICharacterLogArrangeService characterLogArrangeService;

        private readonly CharacterLogService characterLogService;

        private BackupArrange(
            ICharacterLogArrangeService characterLogArrangeService,
            CharacterLogService characterLogService
            )
        {
            this.characterLogArrangeService = characterLogArrangeService;
            this.characterLogService = characterLogService;
        }

        /// <summary>
        /// 功能執行
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            DateTime today = DateTime.Now;
            int year = today.Year;
            int month = today.Month;
            string tableName = "CharacterLog";
            DateTime maxTime = DateTime.ParseExact($"{year}-{month.ToString().PadLeft(2, '0')}-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (month == 1)
            {
                year = year - 1;
            }

            tableName = $"{tableName}_{year}";

            await this.characterLogArrangeService.CreateAsync(tableName);

            var characterLogs = await this.characterLogService.QueryAsync();

            await this.characterLogArrangeService.DeleteAsync(tableName);

            await this.characterLogArrangeService.InsertAsync(tableName, characterLogs);

            if (month == 1)
            {
                await this.characterLogService.DeleteAsync(maxTime);
            }
        }
    }
}
