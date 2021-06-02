namespace Questionnaire.Data
{
    public interface IFileProvider
    {
        string[] ReadFile(string path);
    }
}