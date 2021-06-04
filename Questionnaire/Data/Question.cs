using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data
{
    public class Question
    {
        public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        public string QuestionText { get; set; }
        public bool IsOptional { get; set; }

        public QuestionType Type
        {
            get
            {
                if (AnswerOptions.Any(o => o.IsInputOption))
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

        public bool IsValid
        {
            get
            {
                return IsOptional || IsCompleted;
            }
        }

        public bool IsCompleted => AnswerOptions.Any(o => o.IsSelected);
    }
}
