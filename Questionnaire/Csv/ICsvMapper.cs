using System.Collections.Generic;
using Questionnaire.Data;

namespace Questionnaire.Csv
{
    public interface ICsvMapper
    {
        QuestionCsv Map(Question question, int questionnaireNo);
        IList<QuestionCsv> Map(IList<Question> questions, int questionnaireNo);
    }
}
