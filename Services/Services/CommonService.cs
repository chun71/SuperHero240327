
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

        public async Task CreateTable(string tableName)
        {
            await commonRepository.CreateTable(tableName);
        }
    }
}
