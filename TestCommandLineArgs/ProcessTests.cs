using System.Threading.Tasks;
namespace TestCommandLineArgs
{
    public class ProcessTests
    {
        [Fact]
        public async System.Threading.Tasks.Task TestDo()
        {
            //Arrange
            var fakeHttpClient = new FakeMyHttpClient();
            var process = new Process(fakeHttpClient);
            //Act
            //process.Do();

            //Assert
        }
    }
    public class FakeMyHttpClient : IMyHttpClient
    {
        public System.Threading.Tasks.Task WriteBytesFromResource(string resource, string filePath)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
    public class FakeFileSystem : IFileSystem
    {
        public string GetLogsDirectoryPath()
        {
            return string.Empty;
        }

        public System.Threading.Tasks.Task WriteDataToFile(string filePath, byte[] data)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
