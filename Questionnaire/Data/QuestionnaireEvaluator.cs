using System;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data
{
    public class QuestionnaireEvaluator : IQuestionnaireEvaluator
    {
        public EvaluatedQuestion EvaluateQuestion(Question question)
        {
            AnswerOption selectedAnswer = question.AnswerOptions.FirstOrDefault(o => o.IsSelected);
            return new EvaluatedQuestion
            {
                SelectedAnswer = selectedAnswer?.OptionText,
                CorrectAnswer = string.Join(", ", question.AnswerOptions.Where(o => o.IsCorrectAnswer).Select(o => o.OptionText)),
                RightAnswerSelected = selectedAnswer?.IsCorrectAnswer == true
            };
        }

        public void UpdateQuestion(Question question, string selectedValue)
        {
            foreach (AnswerOption answerOption in question.AnswerOptions)
            {
                answerOption.IsSelected = false;
            }
            AnswerOption selectedOption = question.AnswerOptions.First(o => o.OptionText.Equals(selectedValue));
            selectedOption.IsSelected = true;
        }

        public int GetCorrectQuestionCount(IList<Question> questions) => questions?.SelectMany(q => q.AnswerOptions).Count(a => a.IsCorrectAnswer && a.IsSelected) ?? 0;

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => questionCount == 0 ? 0 : Math.Min(( correctQuestionCount * 100 / questionCount * 100 ) / 100, 100);
    }
}
