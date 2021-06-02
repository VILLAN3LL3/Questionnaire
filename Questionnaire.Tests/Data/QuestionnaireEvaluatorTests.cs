using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Questionnaire.Data;

namespace Questionnaire.Tests.Data
{
    [TestFixture]
    public class QuestionnaireEvaluatorTests
    {
        private QuestionnaireEvaluator CreateQuestionnaireEvaluator() => new();

        private static IEnumerable<TestCaseData> EvaluateQuestionTestData
        {
            get
            {
                yield return new TestCaseData(TestData.FirstQuestionWithWrongAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Ant",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.LastQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Slytherin",
                    SelectedAnswers = "Slytherin",
                    RightAnswersSelected = true
                });
                yield return new TestCaseData(TestData.FirstQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Cat, Dog",
                    RightAnswersSelected = true
                });
                yield return new TestCaseData(TestData.FirstQuestionWithOneWrongAndOneCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Ant, Dog",
                    RightAnswersSelected = false
                });
                yield return new TestCaseData(TestData.FirstQuestionWithOneCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat, Dog",
                    SelectedAnswers = "Dog",
                    RightAnswersSelected = false
                });
            }
        }

        [Test]
        [TestCaseSource(nameof(EvaluateQuestionTestData))]
        public void Should_EvaluateQuestion(Question question, EvaluatedQuestion expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            EvaluatedQuestion result = questionnaireEvaluator.EvaluateQuestion(
                question);

            result.Should().BeEquivalentTo(expectedResult);
        }

        private static IEnumerable<TestCaseData> QuestionCountTestData
        {
            get
            {
                yield return new TestCaseData(new List<Question> { TestData.FirstQuestionWithWrongAnswer, TestData.LastQuestionWithCorrectAnswer }, 1);
                yield return new TestCaseData(null, 0);
                yield return new TestCaseData(new List<Question>(), 0);
            }
        }

        [Test]
        [TestCaseSource(nameof(QuestionCountTestData))]
        public void Should_GetCorrectQuestionCount(IList<Question> questions, int expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            int result = questionnaireEvaluator.GetCorrectQuestionCount(questions);

            result.Should().Be(expectedResult);
        }

        [Test]
        [TestCase(0, 4, 0)]
        [TestCase(1, 4, 25)]
        [TestCase(2, 4, 50)]
        [TestCase(3, 4, 75)]
        [TestCase(4, 4, 100)]
        [TestCase(0, 0, 0)]
        [TestCase(5, 4, 100)]
        public void Should_GetCorrectQuestionPercentage(int correctQuestionCount, int questionCount, int expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            int result = questionnaireEvaluator.GetCorrectQuestionPercentage(
                correctQuestionCount,
                questionCount);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Should_UpdateQuestion()
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();
            Question question = TestData.Questions[0];

            questionnaireEvaluator.UpdateQuestion(question, "Ant");

            question.Should().BeEquivalentTo(TestData.FirstQuestionWithWrongAnswer);
        }
    }
}
