using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Data
{
    public class QuestionService
    {
        private readonly IList<Question> _questions;
        private readonly IQuestionnaireCreator _questionnaireCreator;
        private readonly IFileProvider _fileProvider;

        public QuestionService(IFileProvider fileProvider, IQuestionnaireCreator questionnaireCreator)
        {
            _questionnaireCreator = questionnaireCreator;
            _fileProvider = fileProvider;
        }

        public Task<IList<Question>> GetQuestionsAsync()
        {
            string[] lines = _fileProvider.ReadFile("questionnaire.txt");
            return Task.FromResult(_questionnaireCreator.CreateQuestionnaire(lines));
        }

        public void UpdateQuestion(Question questionToUpdate)
        {
            foreach (Question question in _questions)
            {
                if (question.QuestionText.Equals(questionToUpdate.QuestionText))
                {
                    question.AnswerOptions = questionToUpdate.AnswerOptions;
                }
            }
        }

        public QuestionEvaluationVM EvaluateQuestion(Question question)
        {
            var vm = new QuestionEvaluationVM
            {
                SelectedAnswer = question.AnswerOptions.FirstOrDefault(o => o.IsSelected),
                CorrectAnswer = question.AnswerOptions.First(o => o.IsCorrectAnswer)
            };
            vm.RightAnswerSelected = vm.SelectedAnswer != null ? vm.SelectedAnswer.IsCorrectAnswer : false;
            return vm;
        }

        public int GetCorrectQuestionCount(IList<Question> questions) => questions?.SelectMany(q => q.AnswerOptions).Count(a => a.IsCorrectAnswer && a.IsSelected) ?? 0;

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => ( correctQuestionCount * 100 / questionCount * 100 ) / 100;
    }
}
