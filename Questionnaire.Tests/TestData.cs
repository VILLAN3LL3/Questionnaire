using System.Collections.Generic;
using Questionnaire.Data;

namespace Questionnaire.Tests
{
    public static class TestData
    {
        public static IList<Question> Questions => new List<Question>
            {
                FirstQuestion,
                new Question
                {
                    QuestionText = "What is the sum of 2+3?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "2" },
                        new AnswerOption { OptionText = "5", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "6" },
                        new AnswerOption { OptionText = "Don't know" }
                    }
                },
                new Question
                {
                    QuestionText = "What is the answer to the ultimate question of life, the universe and everything?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "42", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Are you kiddin' me?" },
                        new AnswerOption { OptionText = "There is no such an answer"},
                        new AnswerOption { OptionText = "Don't know" }
                    }
                },
                LastQuestion
            };

        private static Question FirstQuestion => new()
        {
            QuestionText = "Which of these animals is a mammal?",
            AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "Ant" },
                        new AnswerOption { OptionText = "Bee" },
                        new AnswerOption { OptionText = "Cat", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Dog", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Don't know" }
                    }
        };

        private static Question LastQuestion => new()
        {
            QuestionText = "Which Hogwarts House would you be sorted to?",
            AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "Gryffindor", IsCorrectAnswer = false },
                        new AnswerOption { OptionText = "Hufflepuff", IsCorrectAnswer = false },
                        new AnswerOption { OptionText = "Ravenclaw", IsCorrectAnswer = false },
                        new AnswerOption { OptionText = "Slytherin", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "Don't know" }
                    }
        };

        public static Question FirstQuestionWithWrongAnswer
        {
            get
            {
                Question firstQuestion = FirstQuestion;
                firstQuestion.AnswerOptions[0].IsSelected = true;
                return firstQuestion;
            }
        }

        public static Question FirstQuestionWithCorrectAnswer
        {
            get
            {
                Question firstQuestion = FirstQuestion;
                firstQuestion.AnswerOptions[2].IsSelected = true;
                firstQuestion.AnswerOptions[3].IsSelected = true;
                return firstQuestion;
            }
        }

        public static Question FirstQuestionWithOneWrongAndOneCorrectAnswer
        {
            get
            {
                Question firstQuestion = FirstQuestion;
                firstQuestion.AnswerOptions[0].IsSelected = true;
                firstQuestion.AnswerOptions[3].IsSelected = true;
                return firstQuestion;
            }
        }

        public static Question FirstQuestionWithOneCorrectAnswer
        {
            get
            {
                Question firstQuestion = FirstQuestion;
                firstQuestion.AnswerOptions[3].IsSelected = true;
                return firstQuestion;
            }
        }

        public static Question LastQuestionWithCorrectAnswer
        {
            get
            {
                Question lastQuestion = LastQuestion;
                lastQuestion.AnswerOptions[3].IsSelected = true;
                return lastQuestion;
            }
        }
    }
}
