using Task.Classes;

namespace Task
{
    internal class Program
    {
        private static readonly DateTime Now = DateTime.Now;

        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var commandLineArguments = new CommandLineArgs(args);
            var fileSystem = new FileSystem();
            var myHttpClient = new MyHttpClient();
            var process = new Classes.Process(myHttpClient,fileSystem);
            await process.Do(commandLineArguments, Now);
        }
    }
}
