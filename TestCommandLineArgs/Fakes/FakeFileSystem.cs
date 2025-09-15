namespace TestCommandLineArgs.Fakes;

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

    public System.Threading.Tasks.Task WriteLogsInFile(string fileName, string textToWrite)
    {
		return System.Threading.Tasks.Task.CompletedTask;
	}
}