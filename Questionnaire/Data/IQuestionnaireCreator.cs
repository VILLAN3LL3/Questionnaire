using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface IQuestionnaireCreator
    {
        IList<Question> CreateQuestionnaire(string[] textLines);
    }
}