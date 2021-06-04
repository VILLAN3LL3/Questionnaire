using System.Collections.Generic;
using NUnit.Framework;
using Questionnaire.Data;

namespace Questionnaire.Tests
{
    public static class TestData
    {
        public static IList<Question> Questions => new List<Question>
            {
                MultiSelectQuestion,
                OptionalQuestion,
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
                SingleSelectQuestion,
                TextInputQuestion
            };

        public static Question OptionalQuestion
        {
            get
            {
                return new Question
                {
                    QuestionText = "What is the sum of 2+3?",
                    AnswerOptions = new List<AnswerOption>
                    {
                        new AnswerOption { OptionText = "2" },
                        new AnswerOption { OptionText = "5", IsCorrectAnswer = true },
                        new AnswerOption { OptionText = "6" },
                        new AnswerOption { OptionText = "Don't know" }
                    },
                    IsOptional = true
                };
            }
        }

        public static Question MultiSelectQuestion => new()
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

        public static Question SingleSelectQuestion => new()
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

        public static Question TextInputQuestion => new()
        {
            QuestionText = "What kind of animal is \"Cisco\" in \"Dances with Wolves\"?",
            AnswerOptions = new List<AnswerOption>
            {
                new AnswerOption { OptionText = "Horse", IsCorrectAnswer = true }
            }
        };

        public static Question MultiSelectQuestionWithWrongAnswer
        {
            get
            {
                Question question = MultiSelectQuestion;
                question.AnswerOptions[0].IsSelected = true;
                return question;
            }
        }

        public static Question MultiSelectQuestionWithCorrectAnswer
        {
            get
            {
                Question question = MultiSelectQuestion;
                question.AnswerOptions[2].IsSelected = true;
                question.AnswerOptions[3].IsSelected = true;
                return question;
            }
        }

        public static Question MultiSelectQuestionWithOneWrongAndOneCorrectAnswer
        {
            get
            {
                Question question = MultiSelectQuestion;
                question.AnswerOptions[0].IsSelected = true;
                question.AnswerOptions[3].IsSelected = true;
                return question;
            }
        }

        public static Question MultiSelectQuestionWithOneCorrectAnswer
        {
            get
            {
                Question question = MultiSelectQuestion;
                question.AnswerOptions[3].IsSelected = true;
                return question;
            }
        }

        public static Question SingleSelectQuestionWithCorrectAnswer
        {
            get
            {
                Question question = SingleSelectQuestion;
                question.AnswerOptions[3].IsSelected = true;
                return question;
            }
        }

        public static Question SingleSelectQuestionWithWrongAnswer
        {
            get
            {
                Question question = SingleSelectQuestion;
                question.AnswerOptions[2].IsSelected = true;
                return question;
            }
        }

        public static Question TextInputQuestionWithWrongAnswer
        {
            get
            {
                Question question = TextInputQuestion;
                question.AnswerOptions.Add(new AnswerOption { OptionText = "Dog", IsSelected = true });
                return question;
            }
        }

        public static Question TextInputQuestionWithCorrectAnswer
        {
            get
            {
                Question question = TextInputQuestion;
                question.AnswerOptions[0].IsSelected = true;
                return question;
            }
        }

        public static IEnumerable<TestCaseData> EvaluatedQuestionTestData
        {
            get
            {
                yield return new TestCaseData(TestData.SingleSelectQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Slytherin",
                    SelectedAnswers = "Slytherin",
                    RightAnswersSelected = true
                });
                yield return new TestCaseData(TestData.SingleSelectQuestionWithWrongAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Slytherin",
                    SelectedAnswers = "Ravenclaw",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.MultiSelectQuestionWithWrongAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Ant",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.MultiSelectQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Cat, Dog",
                    RightAnswersSelected = true
                });
                yield return new TestCaseData(TestData.MultiSelectQuestionWithOneWrongAndOneCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Ant, Dog",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.MultiSelectQuestionWithOneCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Dog",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.TextInputQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Horse",
                    SelectedAnswers = "Horse",
                    RightAnswersSelected = true
                });
                yield return new TestCaseData(TestData.TextInputQuestionWithWrongAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Horse",
                    SelectedAnswers = "Dog",
                    RightAnswersSelected = false
                });
            }
        }

        public static IEnumerable<TestCaseData> QuestionCountTestData
        {
            get
            {
                yield return new TestCaseData(new List<Question> { MultiSelectQuestionWithWrongAnswer, SingleSelectQuestionWithCorrectAnswer }, 1);
                yield return new TestCaseData(null, 0);
                yield return new TestCaseData(new List<Question>(), 0);
            }
        }

        public static IEnumerable<TestCaseData> ValidationTestData
        {
            get
            {
                yield return new TestCaseData(new List<Question> { OptionalQuestion }, true);
                yield return new TestCaseData(new List<Question> { TextInputQuestion }, false);
                yield return new TestCaseData(new List<Question> { MultiSelectQuestion }, false);
                yield return new TestCaseData(new List<Question> { SingleSelectQuestion }, false);
                yield return new TestCaseData(new List<Question> { SingleSelectQuestionWithWrongAnswer }, true);
                yield return new TestCaseData(new List<Question> { MultiSelectQuestionWithCorrectAnswer }, true);
                yield return new TestCaseData(new List<Question> { TextInputQuestionWithWrongAnswer, MultiSelectQuestion }, false);
                yield return new TestCaseData(new List<Question> { TextInputQuestionWithWrongAnswer, MultiSelectQuestionWithCorrectAnswer }, true);

            }
        }
    }
}
