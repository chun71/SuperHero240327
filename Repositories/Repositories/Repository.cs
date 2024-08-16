
namespace Repositories.Repositories
{
    public abstract class Repository
    {
        protected static string connectionString
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDir, "sqlconnectionString.info");
                string connectionString = File.ReadAllText(filePath);

                return connectionString;
            }
        }
    }
}
