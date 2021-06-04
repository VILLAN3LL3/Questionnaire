using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface IFileProvider
    {
        string[] ReadFile(string path);
        IEnumerable<string> GetQuestionnaires(string path);
        void CreateOrUpdateFile(string path, IEnumerable<string> newLines);
        bool DoesFileExist(string path);
    }
}
