using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data
{
    public class Question
    {
        public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        public string QuestionText { get; set; }

        public bool IsMultiSelect => AnswerOptions.Count(o => o.IsCorrectAnswer) > 1;
    }
}
