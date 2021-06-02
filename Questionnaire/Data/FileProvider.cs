using System.IO;

namespace Questionnaire.Data
{
    public class FileProvider : IFileProvider
    {
        public string[] ReadFile(string path) => File.ReadAllLines(path);
    }
}
