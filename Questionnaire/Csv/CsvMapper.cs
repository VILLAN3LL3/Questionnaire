using Questionnaire.Data;

namespace Questionnaire.Csv
{
    public class CsvMapper : ICsvMapper
    {
        public QuestionCsv Map(Question question, EvaluatedQuestion evaluatedQuestion, int questionnaireNo)
        {
            return new QuestionCsv
            {
                AnswerQuality = evaluatedQuestion.RightAnswersSelected ? "1" : "0",
                Title = $"Questionnaire {questionnaireNo}",
                Question = question.QuestionText.TrimEnd('?')
            };
        }
    }
}
