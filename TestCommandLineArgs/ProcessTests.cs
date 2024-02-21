using Moq;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Task.Classes;

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

    [Fact]
    public async System.Threading.Tasks.Task CheckIfTheRessourceIsValid()
    {
        //var bytes = await _myHttpClient.WriteBytesFromResource(resource); // test that this is called always with correct resource

        //Arrange
        var process = GetProcess();
        var commandLineArgs = new CommandLineArgs(new[]{ "https://azazlo.com", "Bydlo" });
        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("zalupa");
        //Act
        await process.Do(commandLineArgs, DateTime.Now);
        //Assert
        _myHttpClientMock.Verify(client => client.WriteBytesFromResource(commandLineArgs.FilePath), Times.Once);

    }

    [Fact]
    public async System.Threading.Tasks.Task TestStatusCodeReturning200()
    {
        //responseLog.AppendLine("Status code: 200"); // test

        //Arrange
		var process = GetProcess();
        var commandLineArgs = new CommandLineArgs(new[] { "https://example.com", "output" });

        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("request");

        //Act
        await process.Do(commandLineArgs, DateTime.Now);

        //Assert
        _myFileSystemMock.Verify(fs => fs.WriteLogsInFile(It.IsAny<string>(), It.Is<string>(content =>
            content.Contains("Status code: 200"))));
    }


	[Fact]
	public async System.Threading.Tasks.Task Do_ShouldUseCorrectTimeFormatInResult()
	{
        // Arrange
        var process = GetProcess();

		var commandLineArgs = new CommandLineArgs(new[] { "https://example.com", "outputMode" });
        var now =  DateTime.Now;

        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("request");

        _myHttpClientMock
            .Setup(client => client.WriteBytesFromResource(commandLineArgs.FilePath))
            .ReturnsAsync(new byte[0]);

        var expectedTimeFormat = $"{now:MM/dd/yyyy}: Request took";

        // Act
        await process.Do(commandLineArgs, now);

        // Assert
        _myFileSystemMock.Verify(fs => fs.WriteLogsInFile(It.IsAny<string>(), It.Is<string>(content =>
            Regex.IsMatch(content, expectedTimeFormat))), Times.Once);
    }

    [Fact]
    public async System.Threading.Tasks.Task TestingTheCorrectExceptionMessageInDoProcesses()
    {
        // Arrange
        var process = GetProcess();

        var commandLineArgs = new CommandLineArgs(new[] { "https://example.com", "arararat" });
       

        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("request");

        var fakeExceptionMessage = "Fake exception message";

        _myHttpClientMock
        .Setup(client => client.WriteBytesFromResource(commandLineArgs.FilePath))
        .ThrowsAsync(new Exception(fakeExceptionMessage));

        // Act
        await process.Do(commandLineArgs, DateTime.Now);

        // Assert
        _myFileSystemMock.Verify(fs => fs.WriteLogsInFile(It.IsAny<string>(), It.Is<string>(content =>
			content.Contains($"Electric. We are electric... and your request failed: {fakeExceptionMessage}"))), Times.Once);
	}

    [Fact]
    public async System.Threading.Tasks.Task TestingTheCorrectDateTimeWhenLoggingToTheDataBase()
    {
        // Arrange
        var process = GetProcess();

        _myFileSystemMock.Setup(fileSystem => fileSystem.GetLogsDirectoryPath())
            .Returns("request");

        var commandLineArgs = new CommandLineArgs(new[] { "https://example.com", "oraora" });
		var now = new DateTime(2023, 4, 25);

        //Act
        await process.Do(commandLineArgs, now);

        //Assert
        _myHttpClientMock.Verify(client => client.LogToDatabase(It.Is<DateTime>(dateTime => dateTime == now)), Times.Once);

	}

	private Task.Classes.Process GetProcess()
    {
        return new Task.Classes.Process(
            _myHttpClientMock.Object,
            _myFileSystemMock.Object);
    }
}