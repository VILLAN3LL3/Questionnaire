using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Questionnaire.Data;

namespace Questionnaire.Tests.Data
{
    [TestFixture]
    public class QuestionInteractorTests
    {
        private QuestionInteractor CreateQuestionInteractor() => new(
                new FileProvider(),
                new QuestionnaireCreator(),
                new QuestionnaireEvaluator()
            );

        [Test]
        public void Should_GetQuestions()
        {
            QuestionInteractor questionInteractor = CreateQuestionInteractor();
            const string filePath = "testquestionnaire.txt";

            IList<Question> result = questionInteractor.GetQuestions(
                filePath);

            result.Should().BeEquivalentTo(TestData.Questions);
        }

        private static IEnumerable<TestCaseData> EvaluateQuestionTestData
        {
            get
            {
                yield return new TestCaseData(TestData.FirstQuestionWithWrongAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Cat",
                    SelectedAnswer = "Ant",
                    RightAnswerSelected = false
                });
                yield return new TestCaseData(TestData.LastQuestionWithCorrectAnswer, new EvaluatedQuestion
                {
                    CorrectAnswer = "Gryffindor, Hufflepuff, Ravenclaw, Slytherin",
                    SelectedAnswer = "Gryffindor",
                    RightAnswerSelected = true
                });
            }
        }

        [Test]
        [TestCaseSource(nameof(EvaluateQuestionTestData))]
        public void Should_EvaluateQuestion(Question question, EvaluatedQuestion expectedResult)
        {
            QuestionInteractor questionInteractor = CreateQuestionInteractor();

            EvaluatedQuestion result = questionInteractor.EvaluateQuestion(
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
            QuestionInteractor questionInteractor = CreateQuestionInteractor();

            int result = questionInteractor.GetCorrectQuestionCount(questions);

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
            QuestionInteractor questionInteractor = CreateQuestionInteractor();

            int result = questionInteractor.GetCorrectQuestionPercentage(
                correctQuestionCount,
                questionCount);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Should_UpdateQuestion()
        {
            QuestionInteractor questionInteractor = CreateQuestionInteractor();
            Question question = TestData.Questions[0];

            questionInteractor.UpdateQuestion(question, "Ant");

            question.Should().BeEquivalentTo(TestData.FirstQuestionWithWrongAnswer);
        }
    }
}
