using System.Collections.Generic;
using System.Linq;
using Questionnaire.Csv;

namespace Questionnaire.Data
{
    public class QuestionInteractor
    {
        private readonly IQuestionnaireCreator _questionnaireCreator;
        private readonly IFileProvider _fileProvider;
        private readonly IQuestionnaireEvaluator _questionnaireEvaluator;
        private readonly ICsvMapper _csvMapper;
        private readonly ICsvSerializer _csvSerializer;

        public QuestionInteractor(
            IFileProvider fileProvider,
            IQuestionnaireCreator questionnaireCreator,
            IQuestionnaireEvaluator questionnaireEvaluator,
            ICsvMapper csvMapper,
            ICsvSerializer csvSerializer)
        {
            _questionnaireCreator = questionnaireCreator;
            _fileProvider = fileProvider;
            _questionnaireEvaluator = questionnaireEvaluator;
            _csvMapper = csvMapper;
            _csvSerializer = csvSerializer;
        }

        public IList<Question> GetQuestions(string filePath) => _questionnaireCreator.CreateQuestionnaire(_fileProvider.ReadFile(filePath));

        public void UpdateQuestion(Question question, string selectedValue) => _questionnaireEvaluator.UpdateQuestion(question, selectedValue);

        public EvaluatedQuestion EvaluateQuestion(Question question) => _questionnaireEvaluator.EvaluateQuestion(question);

        public int GetCorrectQuestionCount(IList<Question> questions) => _questionnaireEvaluator.GetCorrectQuestionCount(questions);

        public int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount) => _questionnaireEvaluator.GetCorrectQuestionPercentage(correctQuestionCount, questionCount);

        public IEnumerable<string> GetQuestionnaires(string path) => _fileProvider.GetQuestionnaires(path);

        public bool AreAllRequiredQuestionsAnswered(IList<Question> questions) => _questionnaireEvaluator.AreAllRequiredQuestionsAnswered(questions);

        public int GetCompletedQuestionPercentage(IList<Question> questions, bool includeOptional) => _questionnaireEvaluator.GetCompletedQuestionPercentage(questions, includeOptional);

        public void ExportCsv(IList<Question> questions, string path)
        {
            var doesFileExist = _fileProvider.DoesFileExist(path);
            int nexNo = 1;
            bool addHeader = true;

            if (doesFileExist)
            {
                var lines = _fileProvider.ReadFile(path);
                addHeader = lines.Length == 0;

                if (lines.Length > 0)
                {
                    var lastLine = lines.Last();
                    var lastNo = int.Parse(lastLine.Split(';')[0].Split(' ')[1]);
                    nexNo = lastNo + 1;
                }
            }
            IList<QuestionCsv> csvObjects = _csvMapper.Map(questions, nexNo);
            IList<string> serializedObjects = _csvSerializer.SerializeToCsvFile(csvObjects, addHeader);
            _fileProvider.CreateOrUpdateFile(path, serializedObjects);
        }
    }
}
