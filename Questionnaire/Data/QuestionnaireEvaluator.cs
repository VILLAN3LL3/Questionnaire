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
            AnswerOption selectedOption = question.AnswerOptions.FirstOrDefault(o => o.OptionText.Equals(selectedValue));
            if (selectedOption is null)
            {
                question.AnswerOptions.Add(new AnswerOption { OptionText = selectedValue, IsSelected = true });
            }
            else
            {
                selectedOption.IsSelected = true;
            }
        }

        public int GetCorrectQuestionCount(IList<Question> questions) => questions?
            .Count(q => q.AnswerOptions
                .Where(o => o.IsCorrectAnswer)
                .All(o => o.IsSelected)) ?? 0;

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => questionCount == 0
            ? 0
            : Math.Min((int)(((double)correctQuestionCount / (double)questionCount) * 100), 100);

        public bool AreAllRequiredQuestionsAnswered(IList<Question> questions) => questions.All(q => q.IsValid);

        public int GetCompletedQuestionPercentage(IList<Question> questions, bool includeOptional) => includeOptional
            ? (int)((double)questions.Count(q => q.IsCompleted) / (double)(questions.Count) * 100)
            : (int)((double)questions.Count(q => q.IsCompleted && !q.IsOptional) / (double)(questions.Count(q => !q.IsOptional)) * 100);
    }
}
