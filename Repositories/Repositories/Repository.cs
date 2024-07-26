
namespace Repositories.Repositories
{
    public abstract class Repository
    {
        protected static string connectionString
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDir, "sqlconnectionString.txt");
                string connectionString = File.ReadAllText(filePath);

                return connectionString;
            }
        }
    }
}
