using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Questionnaire.Data;

namespace Questionnaire.Tests.Data
{
    [TestFixture]
    public class QuestionnaireCreatorTests
    {
        private QuestionnaireCreator CreateQuestionnaireCreator() => new();

        [Test]
        public void CreateQuestionnaire_StateUnderTest_ExpectedBehavior()
        {
            QuestionnaireCreator questionnaireCreator = CreateQuestionnaireCreator();
            string[] textLines = new string[]
            {
                "?Which of these animals is a mammal",
                "Ant",
                "Bee",
                "*Cat",
                "?What is the sum of 2+3",
                "2",
                "*5",
                "6"
            };
            IList<Question> expectedresult = TestData.Questions.Take(2).ToList();

            IList<Question> result = questionnaireCreator.CreateQuestionnaire(
                textLines);

            result.Should().BeEquivalentTo(expectedresult, opt => opt.WithStrictOrdering());
        }
    }
}
