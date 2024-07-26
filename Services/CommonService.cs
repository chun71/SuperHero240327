
using Repositories;

namespace Services
{
    public sealed class CommonService
    {
        private readonly Repository repository;

        public CommonService(
            Repository repository)
        {
            this.repository = repository;
        }

        public async Task CreateTable(string tableName)
        {
            await this.repository.CreateTable(tableName);
        }
    }
}
