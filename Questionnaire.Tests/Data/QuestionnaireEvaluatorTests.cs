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

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.EvaluatedQuestionTestData))]
        public void Should_EvaluateQuestion(Question question, EvaluatedQuestion expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            EvaluatedQuestion result = questionnaireEvaluator.EvaluateQuestion(
                question);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.QuestionCountTestData))]
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

            question.Should().BeEquivalentTo(TestData.MultiSelectQuestionWithWrongAnswer);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.ValidationTestData))]
        public void Should_Check_If_Questionnaire_Is_Valid(IList<Question> questions, bool expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            var result = questionnaireEvaluator.AreAllRequiredQuestionsAnswered(questions);

            result.Should().Be(expectedResult);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.QuestionCompletionTestData))]
        public void Should_Calculate_Completed_Question_Count(IList<Question> questions, bool includeOptional, int expectedResult)
        {
            QuestionnaireEvaluator questionnaireEvaluator = CreateQuestionnaireEvaluator();

            var result = questionnaireEvaluator.GetCompletedQuestionPercentage(questions, includeOptional);

            result.Should().Be(expectedResult);
        }
    }
}
