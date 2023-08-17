using System.Diagnostics;
using System.Text;

namespace Task
{
    internal class Program
    {
        private static readonly DateTime Now = DateTime.Now;

        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var commandLineArguments = new CommandLineArgs(args);

            while (true)
            {
                var resource = commandLineArguments.FilePath;
                var defaultName = $"{args[0].Replace("https://", string.Empty)}_request_{Now}.log";
                var fileName = args.Length == 2 ? commandLineArguments.OutputMode : defaultName;
                var responseLog = new StringBuilder();
                var result = new StringBuilder();

                var logsDirectoryPath = GetLogsDirectoryPath();

                var filePath = Path.Combine(logsDirectoryPath, fileName);
                var fileForLogs = Path.Combine(logsDirectoryPath, "logs.txt");
                var responseLogPath = Path.Combine(logsDirectoryPath, "response.log");

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    await WriteBytesFromResource(resource, filePath);

                    responseLog.AppendLine("Status code: 200");
                }
                catch (Exception ex)
                {
                    result.Append($"\nElectric. We are electric... and your request failed: {ex.Message}");
                }
                finally
                {
                    stopwatch.Stop();
                    result.Append($"\nRequest took {stopwatch.ElapsedMilliseconds} ms");

                    await WriteLogsInFile(fileForLogs, result.ToString());
                    await WriteLogsInFile(responseLogPath, responseLog.ToString());
                }

                if (!ExitOrContinue())
                    break;
            }
        }

        private static string GetLogsDirectoryPath()
        {
            var folderPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        private static async System.Threading.Tasks.Task WriteBytesFromResource(string resource, string filePath)
        {
            using var client = new HttpClient();
            var output = await client.GetByteArrayAsync(resource);
            await File.WriteAllBytesAsync(filePath, output);
        }

        private static async System.Threading.Tasks.Task WriteLogsInFile(string fileName, string textToWrite)
        {
            await File.WriteAllTextAsync(fileName, textToWrite);
        }

        private static bool ExitOrContinue()
        {
            Console.WriteLine("Press 'Q' to exit the program or any other key to continue running the program");
            var key = Console.ReadKey().Key;

            return key != ConsoleKey.Q;
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