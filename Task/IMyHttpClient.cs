namespace Task
{
    public interface IMyHttpClient
    {
        public Task<byte[]> WriteBytesFromResource(string resource);

        public System.Threading.Tasks.Task LogToDatabase(DateTime exceptionOccuredAt);
    }
}