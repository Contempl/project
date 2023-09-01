namespace Task
{
    public interface IMyHttpClient
    {
        public System.Threading.Tasks.Task<byte[]> WriteBytesFromResource(string resource);
    }
}