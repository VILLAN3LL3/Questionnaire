using System.Collections.Generic;
using Questionnaire.Data;

namespace Questionnaire.Csv
{
    public class CsvMapper : ICsvMapper
    {
        private readonly IQuestionnaireEvaluator _questionnaireEvaluator;

        public CsvMapper(IQuestionnaireEvaluator questionnaireEvaluator)
        {
            _questionnaireEvaluator = questionnaireEvaluator;
        }

        public QuestionCsv Map(Question question, int questionnaireNo)
        {
            EvaluatedQuestion evaluated = _questionnaireEvaluator.EvaluateQuestion(question);
            return new QuestionCsv
            {
                AnswerQuality = evaluated.RightAnswersSelected ? "1" : "0",
                Title = $"Questionnaire {questionnaireNo}",
                Question = question.QuestionText.TrimEnd('?')
            };
        }

        public IList<QuestionCsv> Map(IList<Question> questions, int questionnaireNo)
        {
            var list = new List<QuestionCsv>();
            foreach (var question in questions)
            {
                list.Add(Map(question, questionnaireNo));
            }
            return list;
        }
    }
}
