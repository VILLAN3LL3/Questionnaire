using System.Collections.Generic;

namespace Questionnaire.Data
{
    public class Question
    {
        public IEnumerable<AnswerOption> AnswerOptions { get; set; }
        public string QuestionText { get; set; }
    }
}
