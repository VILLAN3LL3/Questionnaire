using System.Collections.Generic;
using System.IO;

namespace Questionnaire.Data
{
    public class FileProvider : IFileProvider
    {
        public string[] ReadFile(string path) => File.ReadAllLines(path);

        public IEnumerable<string> GetQuestionnaires(string path) => Directory.EnumerateFiles(path, "*.txt");

        public void CreateOrUpdateFile(string path, IEnumerable<string> newLines) => File.AppendAllLines(path, newLines);

        public bool DoesFileExist(string path) => File.Exists(path);
    }
}
