using System.Collections.Generic;

namespace Questionnaire.Data
{
    public class QuestionInteractor
    {
        private readonly IQuestionnaireCreator _questionnaireCreator;
        private readonly IFileProvider _fileProvider;
        private readonly IQuestionnaireEvaluator _questionnaireEvaluator;

        public QuestionInteractor(
            IFileProvider fileProvider,
            IQuestionnaireCreator questionnaireCreator,
            IQuestionnaireEvaluator questionnaireEvaluator)
        {
            _questionnaireCreator = questionnaireCreator;
            _fileProvider = fileProvider;
            _questionnaireEvaluator = questionnaireEvaluator;
        }

        public IList<Question> GetQuestions(string filePath) => _questionnaireCreator.CreateQuestionnaire(_fileProvider.ReadFile(filePath));

        public void UpdateQuestion(Question question, string selectedValue) => _questionnaireEvaluator.UpdateQuestion(question, selectedValue);

        public EvaluatedQuestion EvaluateQuestion(Question question) => _questionnaireEvaluator.EvaluateQuestion(question);

        public int GetCorrectQuestionCount(IList<Question> questions) => _questionnaireEvaluator.GetCorrectQuestionCount(questions);

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => _questionnaireEvaluator.GetCorrectQuestionPercentage(correctQuestionCount, questionCount);

        public IEnumerable<string> GetQuestionnaires(string path) => _fileProvider.GetQuestionnaires(path);
    }
}
