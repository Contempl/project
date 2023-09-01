namespace Task
{
    public interface IFileSystem
    {
        public string GetLogsDirectoryPath();
        public System.Threading.Tasks.Task WriteDataToFile(string filePath, byte[] data);
        public System.Threading.Tasks.Task WriteLogsInFile(string fileName, string textToWrite);
    }
}
