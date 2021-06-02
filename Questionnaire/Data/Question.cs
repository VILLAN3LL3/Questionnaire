using System.Collections.Generic;

namespace Questionnaire.Data
{
    public class Question
    {
        public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        public string QuestionText { get; set; }
    }
}
