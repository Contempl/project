using Moq;
using Task.Classes;
using TestCommandLineArgs.Fakes;

namespace TestCommandLineArgs;

public class ProcessTests
{
    private readonly Mock<IMyHttpClient> _myHttpClientMock = new();
    private readonly Mock<IFileSystem> _myFileSystemMock = new();

    [Fact]
    public async System.Threading.Tasks.Task DoCatchesExceptionThrownByHttpClient()
    {
        //Arrange
        var args = new string[] { "http://huy", "zalupa2" };

        _myHttpClientMock.Setup(httpClient =>
                httpClient.WriteBytesFromResource(
                    It.Is<string>(value => value == "http://huy")))
            .Throws<Exception>();
        
        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("zalupa");

        var process = GetProcess();
        
        //Act
        await process.Do(new CommandLineArgs(args), DateTime.Now);

        //Assert
        _myFileSystemMock.Verify(fileSystem => fileSystem.WriteLogsInFile(
            It.IsAny<string>(),
            It.Is<string>(value => value.Contains("Electric."))));
    }
    
    [Fact]
    public async System.Threading.Tasks.Task DoLogsBothResultAndResponseLog()
    {
        //Arrange
        var args = new string[] { "http://google.com", "zalupa2" };
        
        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("zalupa");

        var process = GetProcess();
        
        //Act
        await process.Do(new CommandLineArgs(args), DateTime.Now);

        //Assert
        _myFileSystemMock.Verify(fileSystem => fileSystem.WriteLogsInFile(
            It.IsAny<string>(),
            It.IsAny<string>()), Times.Exactly(2));
    }

    private Process GetProcess()
    {
        return new Process(
            _myHttpClientMock.Object,
            _myFileSystemMock.Object);
    }
}