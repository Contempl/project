using Task.Classes;
using System.Diagnostics;

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
            await process.Do(commandLineArguments, args, Now);
        }
    }
}
//create iteration2
// Change writing strings to files. (must take Stream and write bytes directly to file)
// Change logging in console to logging in other file. (Write logs in file instead of console)
// Change logic in creating files. directory: <date>_request
// file1(has request response): response.log
// file2(system info): logs.log

// Program shouldn't end after the 1st request and must exist until we stop using it

//create this as pull request in 'development'