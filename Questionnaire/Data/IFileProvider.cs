using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface IFileProvider
    {
        string[] ReadFile(string path);
        IEnumerable<string> GetQuestionnaires(string path);
    }
}
