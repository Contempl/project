using System.Diagnostics;
using System.Text;

namespace Task.Classes
{
    public class Process //conductor of the orchestra
    {
        private IMyHttpClient _myHttpClient;
        private IFileSystem _fileSystem;

        public Process(IMyHttpClient myHttpClient, IFileSystem fileSystem)
        {
            _myHttpClient = myHttpClient;
            _fileSystem = fileSystem;
        }
        public async System.Threading.Tasks.Task Do(CommandLineArgs commandLineArguments, string[] args, DateTime now)
        {
            var resource = commandLineArguments.FilePath;
            var defaultName = $"{args[0].Replace("https://", string.Empty)}_request_{now}.log";
            var fileName = args.Length == 2 ? commandLineArguments.OutputMode : defaultName;

            var responseLog = new StringBuilder();
            var result = new StringBuilder();

            var logsDirectoryPath = _fileSystem.GetLogsDirectoryPath();

            var filePath = Path.Combine(logsDirectoryPath, fileName);
            var fileForLogs = Path.Combine(logsDirectoryPath, "logs.txt");
            var responseLogPath = Path.Combine(logsDirectoryPath, "response.log");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await _myHttpClient.WriteBytesFromResource(resource);

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

                await _fileSystem.WriteLogsInFile(fileForLogs, result.ToString());
                await _fileSystem.WriteLogsInFile(responseLogPath, responseLog.ToString());
            }
        }

    }
}
