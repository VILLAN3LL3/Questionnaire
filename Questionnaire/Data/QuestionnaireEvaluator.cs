using System;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data
{
    public class QuestionnaireEvaluator : IQuestionnaireEvaluator
    {
        public EvaluatedQuestion EvaluateQuestion(Question question)
        {
            IEnumerable<AnswerOption> selectedAnswers = question.AnswerOptions.Where(o => o.IsSelected);
            IEnumerable<AnswerOption> correctAnswers = question.AnswerOptions.Where(o => o.IsCorrectAnswer);

            return new EvaluatedQuestion
            {
                SelectedAnswers = string.Join(", ", selectedAnswers.Where(a => a.IsSelected).Select(a => a.OptionText)),
                CorrectAnswer = string.Join(", ", correctAnswers.Select(o => o.OptionText)),
                RightAnswersSelected = correctAnswers.All(a => a.IsSelected) && selectedAnswers.All(a => a.IsCorrectAnswer)
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

        public int GetCorrectQuestionCount(IList<Question> questions) => questions?.Count(q => q.AnswerOptions.Where(o => o.IsCorrectAnswer).All(o => o.IsSelected)) ?? 0;

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => questionCount == 0 ? 0 : Math.Min(( correctQuestionCount * 100 / questionCount * 100 ) / 100, 100);
    }
}
