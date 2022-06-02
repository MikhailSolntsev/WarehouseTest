using System.IO;

namespace WarehouseTest
{
    public static class FileHelper
    {
        public static string RandomFileName()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        public static void DeleteFileIfExists(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                File.Delete(file.FullName);
                System.Console.WriteLine($"File {file.Name} deleted");
            }
            else
            {
                System.Console.WriteLine($"File {file.Name} not found");
            }
        }
    }
}
