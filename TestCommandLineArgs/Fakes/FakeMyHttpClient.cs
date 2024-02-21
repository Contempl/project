namespace TestCommandLineArgs.Fakes;

public class FakeMyHttpClient : IMyHttpClient
{
    public System.Threading.Tasks.Task WriteBytesFromResource(string resource, string filePath)
    {
        return System.Threading.Tasks.Task.CompletedTask;
    }

    public Task<byte[]> WriteBytesFromResource(string resource)
    {
        return (Task<byte[]>)System.Threading.Tasks.Task.CompletedTask;
    }

    public System.Threading.Tasks.Task LogToDatabase(DateTime exceptionOccuredAt)
    {
        return System.Threading.Tasks.Task.CompletedTask;
    }
}