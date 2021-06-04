using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface IQuestionnaireEvaluator
    {
        EvaluatedQuestion EvaluateQuestion(Question question);
        int GetCorrectQuestionCount(IList<Question> questions);
        int GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount);
        void UpdateQuestion(Question question, string selectedValue);
        bool AreAllRequiredQuestionsAnswered(IList<Question> questions);
        int GetCompletedQuestionPercentage(IList<Question> questions, bool includeOptional);
    }
}
