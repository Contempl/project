namespace Task.Classes
{
    public class FileSystem : IFileSystem
    {
        public string GetLogsDirectoryPath()
        {
            var folderPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        public async System.Threading.Tasks.Task WriteDataToFile(string filePath, byte[] data)
        {
            await File.WriteAllBytesAsync(filePath, data);
        }
        public async System.Threading.Tasks.Task WriteLogsInFile(string fileName, string textToWrite)
        {
            await File.WriteAllTextAsync(fileName, textToWrite);
        }
    }
}
