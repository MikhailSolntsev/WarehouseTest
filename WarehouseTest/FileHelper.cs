using System.IO;

namespace WarehouseTest
{
    public static class FileHelper
    {
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

        public static void WriteStringToFile(string fileName, string value)
        {
            StreamWriter text = File.CreateText(fileName);
            text.Write(value);
            text.Close();
        }

    }
}
