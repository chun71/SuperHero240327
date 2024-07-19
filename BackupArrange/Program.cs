
namespace BackupArrange
{
    sealed class Program
    {
        static async Task Main(string[] args)
        {
            await Tasks.BackupArrange.Obj.Run();
        }
    }
}
