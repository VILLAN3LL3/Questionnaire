using System.Collections.Generic;

namespace Questionnaire.Data
{
    public class QuestionnaireCreator : IQuestionnaireCreator
    {
        public IList<Question> CreateQuestionnaire(string[] textLines)
        {
            var listToReturn = new List<Question>();
            Question currentQuestion = null;

            foreach (string line in textLines)
            {
                if (IsQuestion(line))
                {
                    AddDontKnowAnswerOption(currentQuestion);
                    currentQuestion = new Question { QuestionText = CreateQuestionText(line) };
                    listToReturn.Add(currentQuestion);
                }
                else
                {
                    currentQuestion?.AnswerOptions.Add(CreateAnswerOption(line));
                }
            }
            AddDontKnowAnswerOption(currentQuestion);
            return listToReturn;
        }

        private bool IsQuestion(string line) => line.StartsWith('?');

        private string CreateQuestionText(string line) => line.Split('?')[1] + '?';

        private AnswerOption CreateAnswerOption(string line)
        {
            string[] splittedLine = line.Split('*');
            return splittedLine.Length == 2
                ? new AnswerOption { IsCorrectAnswer = true, OptionText = splittedLine[1] }
                : new AnswerOption { OptionText = splittedLine[0] };
        }

        private void AddDontKnowAnswerOption(Question question)
        {
            question?.AnswerOptions.Add(CreateAnswerOption("Don't know"));
        }
    }
}
