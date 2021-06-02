using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questionnaire.Data
{
    public class QuestionService
    {
        private readonly IList<Question> _questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Which of these animals is a mammal?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "Ant" },
                        new AnswerOption { OptionText = "Bee" },
                        new AnswerOption { OptionText = "Cat", IsCorrectAnswer = true },
                    }
                },
                new Question
                {
                    QuestionText = "What is the sum of 2+3?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "2" },
                        new AnswerOption { OptionText = "5", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "6" }
                    }
                },
                new Question
                {
                    QuestionText = "What is the answer to the ultimate question of life, the universe and everything?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "42", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Are you kiddin' me?" },
                        new AnswerOption { OptionText = "There is no such an answer"}
                    }
                },
                new Question
                {
                    QuestionText = "Which Howarts House would you be sorted to?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "Gryffindor", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Hufflepuff", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Ravenclaw", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Slytherin", IsCorrectAnswer = true }
                    }
                }
            };

        public Task<IList<Question>> GetQuestionsAsync() => Task.FromResult(_questions);

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
    }
}
