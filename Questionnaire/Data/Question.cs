using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data
{
    public class Question
    {
        public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        public string QuestionText { get; set; }

        public QuestionType Type
        {
            get
            {
                if (AnswerOptions.Count == 1)
                {
                    return QuestionType.TextInput;
                }
                if (AnswerOptions.Count(o => o.IsCorrectAnswer) > 1)
                {
                    return QuestionType.MultiSelect;
                }
                return QuestionType.SingleSelect;
            }
        }
    }
}
