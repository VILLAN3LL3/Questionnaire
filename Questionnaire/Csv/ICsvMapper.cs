using Questionnaire.Data;

namespace Questionnaire.Csv
{
    public interface ICsvMapper
    {
        QuestionCsv Map(Question question, EvaluatedQuestion evaluatedQuestion, int questionnaireNo);
    }
}
