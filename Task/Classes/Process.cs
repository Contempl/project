using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Task.Classes
{
    public class Process //conductor of the orchestra
    {
        private readonly IMyHttpClient _myHttpClient;
        private readonly IFileSystem _fileSystem;

        public Process(IMyHttpClient myHttpClient, IFileSystem fileSystem)
        {
            _myHttpClient = myHttpClient;
            _fileSystem = fileSystem;
        }
        
        public async System.Threading.Tasks.Task Do(CommandLineArgs commandLineArguments, DateTime now)
        {
            var resource = commandLineArguments.FilePath;

            var responseLog = new StringBuilder();
            var result = new StringBuilder();
            var bytesArray = new byte[0];

            var logsDirectoryPath = _fileSystem.GetLogsDirectoryPath();

            var fileForLogs = Path.Combine(logsDirectoryPath, "logs.txt");
            var responseLogPath = Path.Combine(logsDirectoryPath, "response.log");
            var fileForDataPath = Path.Combine(logsDirectoryPath, "data.txt");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var bytes = await _myHttpClient.WriteBytesFromResource(resource); // test that this is called always with correct resource
                Console.WriteLine(bytes.Length.ToString());
                bytesArray = bytes;
                responseLog.AppendLine("Status code: 200"); // test

                await _myHttpClient.LogToDatabase(now); // test the same time is used
            }
            catch (Exception ex)
            {
                result.Append($"\nElectric. We are electric... and your request failed: {ex.Message}");
                // test that correct ex.Message is used
            }
            finally
            {
                stopwatch.Stop();
                result.Append($"\n{now:MM/dd/yyyy}: Request took {stopwatch.ElapsedMilliseconds} ms."); // test that correct time format is used

                await _fileSystem.WriteLogsInFile(fileForLogs, result.ToString());
                await _fileSystem.WriteLogsInFile(responseLogPath, responseLog.ToString());
                await _fileSystem.WriteDataToFile(fileForDataPath, bytesArray);
                Console.WriteLine(responseLog.ToString());
            }
        }

    }
}
